using KadiovVehicleCare.Models;

namespace KadiovVehicleCare.Interfaces
{
    public interface IServiceRepository
    {
        Task<IEnumerable<Service>> GetAllAsync();
        Task<Service?> GetByIdAsync(int id);
        Task<bool> AddAsync(Service service);
        Task<bool> UpdateAsync(Service service);
        Task<bool> DeleteAsync(Service service);
        Task<bool> SaveAsync();
    }
}
