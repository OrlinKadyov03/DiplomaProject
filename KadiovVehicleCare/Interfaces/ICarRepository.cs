using KadiovVehicleCare.Models;

namespace KadiovVehicleCare.Interfaces
{
    public interface ICarRepository
    {
        Task<IEnumerable<Car>> GetAllAsync();
        Task<Car?> GetByIdAsync(int id);
        Task<bool> AddAsync(Car car);
        Task<bool> UpdateAsync(Car car);
        Task<bool> DeleteAsync(Car car);
        Task<bool> SaveAsync();
    }
}
