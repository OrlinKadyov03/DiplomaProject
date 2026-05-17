using KadiovVehicleCare.Models;

namespace KadiovVehicleCare.Interfaces
{
    public interface IClientRepository
    {
        Task<Client?> GetByUserIdAsync(string userId);
        Task<IEnumerable<Client>> GetAllAsync();
        Task<Client?> GetByIdAsync(int id);
        Task<bool> AddAsync(Client client);
        Task<bool> UpdateAsync(Client client);
        Task<bool> DeleteAsync(Client client);
        Task<bool> SaveAsync();
    }
}
