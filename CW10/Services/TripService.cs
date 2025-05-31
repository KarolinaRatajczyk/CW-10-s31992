using CW10.DTOs;
using CW10.Models;

namespace CW10.Services;
public interface ITripService
{
    Task<TripListDto> GetTripsAsync(int page, int pageSize);
    Task<bool> DeleteClientAsync(int idClient);
    Task<string> AssignClientToTripAsync(int idTrip, NewClientDto dto);
}

public class TripService : ITripService
{
    private readonly ApbdContext _context;

    public TripService(ApbdContext context)
    {
        _context = context;
    }
    
    public async Task<TripListDto> GetTripsAsync(int page, int pageSize)
    {
        var query = _context.Trips
            .Include(t => t.CountryTrips).ThenInclude(ct => ct.Country)
            .Include(t => t.ClientTrips).ThenInclude(ct => ct.Client)
            .OrderByDescending(t => t.DateFrom);

        int totalTrips = await query.CountAsync();
        int allPages = (int)Math.Ceiling(totalTrips / (double)pageSize);

        var trips = await query
            .Skip((page - 1) * pageSize)
            .Take(pageSize)
            .ToListAsync();

        return new TripListDto
        {
            PageNum = page,
            PageSize = pageSize,
            AllPages = allPages,
            Trips = trips.Select(t => new TripDto
            {
                Name = t.Name,
                Description = t.Description,
                DateFrom = t.DateFrom,
                DateTo = t.DateTo,
                MaxPeople = t.MaxPeople,
                Countries = t.CountryTrips.Select(ct => new CountryDto
                {
                    Name = ct.Country.Name
                }).ToList(),
                Clients = t.ClientTrips.Select(ct => new ClientShortDto
                {
                    FirstName = ct.Client.FirstName,
                    LastName = ct.Client.LastName
                }).ToList()
            }).ToList()
        };
    }
    
    public async Task<bool> DeleteClientAsync(int idClient)
    {
        var client = await _context.Clients
            .Include(c => c.ClientTrips)
            .FirstOrDefaultAsync(c => c.IdClient == idClient);

        if (client == null)
            return false;

        if (client.ClientTrips.Any())
            return false;

        _context.Clients.Remove(client);
        await _context.SaveChangesAsync();

        return true;
    }
    
    public async Task<string> AssignClientToTripAsync(int idTrip, NewClientDto dto)
        {
            var trip = await _context.Trips
                .Include(t => t.ClientTrips)
                .ThenInclude(ct => ct.Client)
                .FirstOrDefaultAsync(t => t.IdTrip == idTrip);

            if (trip == null)
                return "Trip not found.";

            if (trip.DateFrom <= DateTime.UtcNow)
                return "Cannot register for a trip that already started or ended.";
            
            var existingClient = await _context.Clients.FirstOrDefaultAsync(c => c.Pesel == dto.Pesel);
            if (existingClient != null)
            {
                bool alreadyAssigned = await _context.ClientTrips.AnyAsync(ct =>
                    ct.IdTrip == idTrip && ct.IdClient == existingClient.IdClient);

                if (alreadyAssigned)
                    return "Client with this PESEL is already registered for this trip.";
            }
            
            var client = existingClient ?? new Client
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                Email = dto.Email,
                Telephone = dto.Telephone,
                Pesel = dto.Pesel
            };

            if (existingClient == null)
                await _context.Clients.AddAsync(client);

            var clientTrip = new ClientTrip
            {
                Client = client,
                IdTrip = idTrip,
                RegisteredAt = DateTime.UtcNow,
                PaymentDate = dto.PaymentDate
            };

            await _context.ClientTrips.AddAsync(clientTrip);
            await _context.SaveChangesAsync();

            return null;
        }
}