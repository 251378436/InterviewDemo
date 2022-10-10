using API.Contracts;

namespace API.Repositories
{
    public interface IDataManager
    {
        Task<IEnumerable<Guest>> GetGuests();
        Task SaveGuest(Guest guest);
    }
}
