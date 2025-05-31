using Microsoft.AspNetCore.Mvc;
using CW10.Services;

namespace CW10.Controllers;

[ApiController]
[Route("api/clients")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _clientService;

    public ClientsController(IClientService clientService)
    {
        _clientService = clientService;
    }

    [HttpDelete("{idClient}")]
    public async Task<IActionResult> DeleteClient(int idClient)
    {
        var result = await _clientService.DeleteClientAsync(idClient);

        if (!result.IsSuccess)
            return BadRequest(result.Message);

        return NoContent();
    }
}