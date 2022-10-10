using API.Contracts;

namespace API.Repositories
{
    public interface IDataManager
    {
        Task<List<Guest>> GetGuests();
        Task SaveGuest(Guest guest);
    }
}
