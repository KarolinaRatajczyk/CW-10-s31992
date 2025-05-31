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

    // GET /api/trips?page=1&pageSize=10
    [HttpGet]
    public async Task<IActionResult> GetTrips([FromQuery] int page = 1, [FromQuery] int pageSize = 10)
    {
        var result = await _tripService.GetTripsAsync(page, pageSize);
        return Ok(result);
    }

    // POST /api/trips/{idTrip}/clients
    [HttpPost("{idTrip}/clients")]
    public async Task<IActionResult> AssignClientToTrip(int idTrip, [FromBody] NewClientDto dto)
    {
        var result = await _tripService.AssignClientToTripAsync(idTrip, dto);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return Ok(new { message = result.Message });
    }
}
