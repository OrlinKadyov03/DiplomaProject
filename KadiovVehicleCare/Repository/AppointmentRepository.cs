using KadiovVehicleCare.Data;
using KadiovVehicleCare.Interfaces;
using KadiovVehicleCare.Models;
using Microsoft.EntityFrameworkCore;

namespace KadiovVehicleCare.Repository
{
    public class AppointmentRepository : IAppointmentRepository
    {
        private readonly ApplicationDbContext _context;

        public AppointmentRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IEnumerable<Appointment>> GetAllAsync()
        {
            return await _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.Car)
                .Include(a => a.Service)
                .ToListAsync();
        }

        public async Task<Appointment?> GetByIdAsync(int id)
        {
            return await _context.Appointments
                .Include(a => a.Client)
                .Include(a => a.Car)
                .Include(a => a.Service)
                .FirstOrDefaultAsync(a => a.Id == id);
        }

        public async Task<bool> AddAsync(Appointment appointment)
        {
            await _context.Appointments.AddAsync(appointment);
            return await SaveAsync();
        }

        public async Task<bool> UpdateAsync(Appointment appointment)
        {
            _context.Appointments.Update(appointment);
            return await SaveAsync();
        }

        public async Task<bool> DeleteAsync(Appointment appointment)
        {
            _context.Appointments.Remove(appointment);
            return await SaveAsync();
        }

        public async Task<bool> SaveAsync()
        {
            var saved = await _context.SaveChangesAsync();
            return saved > 0;
        }
    }
}
