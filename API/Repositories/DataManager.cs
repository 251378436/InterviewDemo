using API.Contracts;

namespace API.Repositories
{
    public class DataManager : IDataManager
    {
        public async Task<IEnumerable<Guest>> GetGuests()
        {
            await Task.Delay(1);
            return new List<Guest>();
        }

        public async Task SaveGuest(Guest guest)
        {
            await Task.Delay(1);
        }
    }
}
