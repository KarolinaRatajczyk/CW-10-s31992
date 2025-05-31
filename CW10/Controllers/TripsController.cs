using Microsoft.AspNetCore.Mvc;
using CW10.DTOs;
using CW10.Services;

namespace CW10.Controllers;


[ApiController]
[Route("api/trips")]
public class TripsController : ControllerBase
{
    private readonly ITripService _tripService;

    public TripsController(ITripService tripService)
    {
        _tripService = tripService;
    }

    [HttpGet]
    public async Task<IActionResult> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var trips = await _tripService.GetTripsAsync(page, pageSize);
        return Ok(trips);
    }

    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AddClientToTrip(int idTrip, [FromBody] NewClientDto dto)
    {
        var result = await _tripService.AddClientToTripAsync(idTrip, dto);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok();
    }
}