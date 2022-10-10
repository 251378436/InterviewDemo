using API.Contracts;
using API.Services;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestsController : ControllerBase
    {
        private readonly ILogger<GuestsController> _logger;
        private readonly IGuestService _guestService;

        public GuestsController(ILogger<GuestsController> logger, IGuestService guestService)
        {
            _logger = logger;
            _guestService = guestService;
        }

        [HttpPost]
        public async Task Post([FromBody] Guest guest)
        {
            await _guestService.ProcessGuestInfo(guest);
        }
    }
}