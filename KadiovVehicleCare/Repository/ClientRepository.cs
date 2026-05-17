using KadiovVehicleCare.Data;
using KadiovVehicleCare.Interfaces;
using KadiovVehicleCare.Models;
using Microsoft.EntityFrameworkCore;

namespace KadiovVehicleCare.Repository
{
    public class ClientRepository : IClientRepository
    {
        private readonly ApplicationDbContext _context;

        public ClientRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Client?> GetByUserIdAsync(string userId)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.UserId == userId);
        }

        public async Task<IEnumerable<Client>> GetAllAsync()
        {
            return await _context.Clients.ToListAsync();
        }

        public async Task<Client?> GetByIdAsync(int id)
        {
            return await _context.Clients.FirstOrDefaultAsync(c => c.Id == id);
        }

        public async Task<bool> AddAsync(Client client)
        {
            await _context.Clients.AddAsync(client);
            return await SaveAsync();
        }

        public async Task<bool> UpdateAsync(Client client)
        {
            _context.Clients.Update(client);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Client client)
        {
            _context.Clients.Remove(client);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}