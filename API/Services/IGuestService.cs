using API.Contracts;

namespace API.Services
{
    public interface IGuestService
    {
        Task ProcessGuestInfo(Guest guest);
    }
}
