using KadiovVehicleCare.Data;
using KadiovVehicleCare.Interfaces;
using KadiovVehicleCare.Models;
using Microsoft.EntityFrameworkCore;

namespace KadiovVehicleCare.Repository
{
    public class ServiceRepository : IServiceRepository
    {
        private readonly ApplicationDbContext _context;

        public ServiceRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Service>> GetAllAsync()
        {
            return await _context.Services.ToListAsync();
        }

        public async Task<Service?> GetByIdAsync(int id)
        {
            return await _context.Services.FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<bool> AddAsync(Service service)
        {
            await _context.Services.AddAsync(service);
            return await SaveAsync();
        }

        public async Task<bool> UpdateAsync(Service service)
        {
            _context.Services.Update(service);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Service service)
        {
            _context.Services.Remove(service);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}