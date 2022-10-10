using API.Contracts;
using API.Repositories;

namespace API.Services
{
    public class GuestService : IGuestService
    {
        private readonly IDataManager _dataManager;
        private readonly ILogger<GuestService> _logger;
        public GuestService(IDataManager dataManager, ILogger<GuestService> logger)
        {
            _dataManager = dataManager;
            _logger = logger;
        }

        public async Task ProcessGuestInfo(Guest guest)
        {
            _logger.LogInformation("Start processing guest information ......");

            await _dataManager.SaveGuest(guest);

            _logger.LogInformation("Notify administrator......");
        }
    }
}
