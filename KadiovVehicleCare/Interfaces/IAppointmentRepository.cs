using KadiovVehicleCare.Models;

namespace KadiovVehicleCare.Interfaces
{
    public interface IAppointmentRepository
    {
        Task<IEnumerable<Appointment>> GetAllAsync();
        Task<Appointment?> GetByIdAsync(int id);
        Task<bool> AddAsync(Appointment appointment);
        Task<bool> UpdateAsync(Appointment appointment);
        Task<bool> DeleteAsync(Appointment appointment);
        Task<bool> SaveAsync();
    }
}
