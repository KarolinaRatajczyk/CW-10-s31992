using Microsoft.AspNetCore.Mvc;
using CW10.Services;

namespace CW10.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientsController : ControllerBase
{
    private readonly ITripService _tripService;

    public ClientsController(ITripService tripService)
    {
        _tripService = tripService;
    }

    // DELETE /api/clients/{idClient}
    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var result = await _tripService.DeleteClientAsync(idClient);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return NoContent();
    }
}