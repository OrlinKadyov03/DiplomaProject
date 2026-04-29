using KadiovVehicleCare.Data;
using KadiovVehicleCare.Interfaces;
using KadiovVehicleCare.Models;
using Microsoft.EntityFrameworkCore;

namespace KadiovVehicleCare.Repository
{
    public class CarRepository :ICarRepository
    {
        private readonly ApplicationDbContext _context;

        public CarRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Car>> GetAllAsync()
        {
            return await _context.Cars.Include(c => c.Client).ToListAsync();
        }

        public async Task<Car?> GetByIdAsync(int id)
        {
            return await _context.Cars
                .Include(c => c.Client)
                .FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> AddAsync(Car car)
        {
            await _context.Cars.AddAsync(car);
            return await SaveAsync();
        }

        public async Task<bool> UpdateAsync(Car car)
        {
            _context.Cars.Update(car);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Car car)
        {
            _context.Cars.Remove(car);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
