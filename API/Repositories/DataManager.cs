using API.Contracts;
using API.Support.FileProvider;
using System.Text.Json;

namespace API.Repositories
{
    /// <summary>
    /// Manage Guests data
    /// </summary>
    public class DataManager : IDataManager
    {
        private readonly FileProviderService _fileProviderService;
        private readonly string _filePath;
        private List<Guest> _guests = new List<Guest>();
        private readonly SemaphoreSlim _tokenCacheLock = new(1);

        public DataManager(FileProviderService fileProviderService)
        {
            _fileProviderService = fileProviderService;
            _filePath = $"{Directory.GetCurrentDirectory()}\\Content\\collections.json";
        }

        public async Task<List<Guest>> GetGuests()
        {
            if (_guests.Any())
                return _guests;

            var content = await _fileProviderService.Read(_filePath);

            try
            {
                _guests = JsonSerializer.Deserialize<List<Guest>>(content, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                }) ?? new List<Guest>();

            }
            catch (Exception) { }

            return _guests;
        }

        public async Task SaveGuest(Guest guest)
        {
            try
            {
                await _tokenCacheLock.WaitAsync();
                var guests = await GetGuests();
                if (!guests.Exists(g => g.FirstName == guest.FirstName 
                                    && g.LastName == guest.LastName)) 
                {
                    guests.Add(guest);
                    var json = JsonSerializer.Serialize(_guests, new JsonSerializerOptions
                    {
                        PropertyNamingPolicy = JsonNamingPolicy.CamelCase
                    });
                    await _fileProviderService.Write(_filePath, json);
                }
            }
            finally
            {
                _tokenCacheLock.Release();
            }
        }
    }
}