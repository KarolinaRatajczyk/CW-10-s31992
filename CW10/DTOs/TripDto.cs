namespace CW10.DTOs;

public class TripDto
{
    public int IdTrip { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public DateTime DateFrom { get; set; }
    public DateTime DateTo { get; set; }
    public int MaxPeople { get; set; }
    public List<CountryDto> Countries { get; set; } = new();
    public List<ClientShortDto> Clients { get; set; } = new();
}