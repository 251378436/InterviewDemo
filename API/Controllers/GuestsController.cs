using API.Contracts;
using API.Contracts.Client;
using API.Services;
using AutoMapper;
using FluentValidation;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class GuestsController : ControllerBase
    {
        private readonly ILogger<GuestsController> _logger;
        private readonly IGuestService _guestService;
        private readonly IMapper _mapper;
        private readonly IValidator<Request> _validator;

        public GuestsController(ILogger<GuestsController> logger, 
            IGuestService guestService,
            IMapper mapper,
            IValidator<Request> validator)
        {
            _logger = logger;
            _guestService = guestService;
            _mapper = mapper;
            _validator = validator;
        }

        [HttpPost]
        public async Task<Response> Post([FromBody] Request request)
        {
            await _validator.ValidateAndThrowAsync(request);

            var guest = _mapper.Map<Guest>(request);
            await _guestService.ProcessGuestInfo(guest);

            _logger.LogInformation("saved");
            return new Response
            {
                Message = "Saved"
            };
        }
    }
}