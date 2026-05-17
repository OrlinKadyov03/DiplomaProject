using KadiovVehicleCare.Interfaces;
using KadiovVehicleCare.Models.Enum;
using KadiovVehicleCare.Models;
using KadiovVehicleCare.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Authorization;

namespace KadiovVehicleCare.Controllers
{

    [Authorize(Roles = "Admin,User")]
    public class AppointmentController : Controller
    {
        private readonly IAppointmentRepository _appointmentRepository;
        private readonly IClientRepository _clientRepository;
        private readonly ICarRepository _carRepository;
        private readonly IServiceRepository _serviceRepository;

        public AppointmentController(
            IAppointmentRepository appointmentRepository,
            IClientRepository clientRepository,
            ICarRepository carRepository,
            IServiceRepository serviceRepository)
        {
            _appointmentRepository = appointmentRepository;
            _clientRepository = clientRepository;
            _carRepository = carRepository;
            _serviceRepository = serviceRepository;
        }

        public async Task<IActionResult> Index()
        {
            var appointments = await _appointmentRepository.GetAllAsync();

            var viewModels = appointments.Select(a => new AppointmentViewModel
            {
                Id = a.Id,
                ClientFullName = a.Client != null ? $"{a.Client.FirstName} {a.Client.LastName}" : "",
                CarInfo = a.Car != null ? $"{a.Car.Brand} {a.Car.Model} ({a.Car.PlateNumber})" : "",
                ServiceName = a.Service != null ? a.Service.Name : "",
                EmployeeId = a.EmployeeId,
                AppointmentDate = a.AppointmentDate,
                Status = a.Status.ToString(),
                Notes = a.Notes
            });

            return View(viewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null) return NotFound();

            var viewModel = new AppointmentViewModel
            {
                Id = appointment.Id,
                ClientFullName = appointment.Client != null ? $"{appointment.Client.FirstName} {appointment.Client.LastName}" : "",
                CarInfo = appointment.Car != null ? $"{appointment.Car.Brand} {appointment.Car.Model} ({appointment.Car.PlateNumber})" : "",
                ServiceName = appointment.Service != null ? appointment.Service.Name : "",
                EmployeeId = appointment.EmployeeId,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status.ToString(),
                Notes = appointment.Notes
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var viewModel = new CreateAppointmentViewModel
            {
                AppointmentDate = DateTime.Now,
                Clients = await GetClientsSelectList(),
                Cars = await GetCarsSelectList(),
                Services = await GetServicesSelectList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateAppointmentViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                viewModel.Clients = await GetClientsSelectList();
                viewModel.Cars = await GetCarsSelectList();
                viewModel.Services = await GetServicesSelectList();
                return View(viewModel);
            }

            var appointment = new Appointment
            {
                ClientId = viewModel.ClientId,
                CarId = viewModel.CarId,
                ServiceId = viewModel.ServiceId,
                EmployeeId = viewModel.EmployeeId,
                AppointmentDate = viewModel.AppointmentDate,
                Notes = viewModel.Notes
            };

            var result = await _appointmentRepository.AddAsync(appointment);
            if (!result)
            {
                ModelState.AddModelError("", "Неуспешно добавяне на записване.");
                viewModel.Clients = await GetClientsSelectList();
                viewModel.Cars = await GetCarsSelectList();
                viewModel.Services = await GetServicesSelectList();
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null) return NotFound();

            var viewModel = new EditAppointmentViewModel
            {
                Id = appointment.Id,
                ClientId = appointment.ClientId,
                CarId = appointment.CarId,
                ServiceId = appointment.ServiceId,
                EmployeeId = appointment.EmployeeId,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status.ToString(),
                Notes = appointment.Notes,
                Clients = await GetClientsSelectList(),
                Cars = await GetCarsSelectList(),
                Services = await GetServicesSelectList(),
                Statuses = GetStatusesSelectList()
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditAppointmentViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                viewModel.Clients = await GetClientsSelectList();
                viewModel.Cars = await GetCarsSelectList();
                viewModel.Services = await GetServicesSelectList();
                viewModel.Statuses = GetStatusesSelectList();
                return View(viewModel);
            }

            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null) return NotFound();

            appointment.ClientId = viewModel.ClientId;
            appointment.CarId = viewModel.CarId;
            appointment.ServiceId = viewModel.ServiceId;
            appointment.EmployeeId = viewModel.EmployeeId;
            appointment.AppointmentDate = viewModel.AppointmentDate;
            appointment.Notes = viewModel.Notes;

            if (Enum.TryParse<AppointmentStatus>(viewModel.Status, out var status))
            {
                appointment.Status = status;
            }

            var result = await _appointmentRepository.UpdateAsync(appointment);
            if (!result)
            {
                ModelState.AddModelError("", "Неуспешно редактиране на записване.");
                viewModel.Clients = await GetClientsSelectList();
                viewModel.Cars = await GetCarsSelectList();
                viewModel.Services = await GetServicesSelectList();
                viewModel.Statuses = GetStatusesSelectList();
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null) return NotFound();

            var viewModel = new AppointmentViewModel
            {
                Id = appointment.Id,
                ClientFullName = appointment.Client != null ? $"{appointment.Client.FirstName} {appointment.Client.LastName}" : "",
                CarInfo = appointment.Car != null ? $"{appointment.Car.Brand} {appointment.Car.Model} ({appointment.Car.PlateNumber})" : "",
                ServiceName = appointment.Service != null ? appointment.Service.Name : "",
                EmployeeId = appointment.EmployeeId,
                AppointmentDate = appointment.AppointmentDate,
                Status = appointment.Status.ToString(),
                Notes = appointment.Notes
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var appointment = await _appointmentRepository.GetByIdAsync(id);
            if (appointment == null) return NotFound();

            var result = await _appointmentRepository.DeleteAsync(appointment);
            if (!result)
            {
                ModelState.AddModelError("", "Неуспешно изтриване на записване.");
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }

        private async Task<IEnumerable<SelectListItem>> GetClientsSelectList()
        {
            var clients = await _clientRepository.GetAllAsync();
            return clients.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.FirstName} {c.LastName}"
            });
        }

        private async Task<IEnumerable<SelectListItem>> GetCarsSelectList()
        {
            var cars = await _carRepository.GetAllAsync();
            return cars.Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = $"{c.Brand} {c.Model} ({c.PlateNumber})"
            });
        }

        private async Task<IEnumerable<SelectListItem>> GetServicesSelectList()
        {
            var services = await _serviceRepository.GetAllAsync();
            return services.Select(s => new SelectListItem
            {
                Value = s.Id.ToString(),
                Text = s.Name
            });
        }

        private IEnumerable<SelectListItem> GetStatusesSelectList()
        {
            return new List<SelectListItem>
            {
                new SelectListItem { Value = "Pending", Text = "Pending" },
                new SelectListItem { Value = "Completed", Text = "Completed" },
                new SelectListItem { Value = "Cancelled", Text = "Cancelled" }
            };

        }
    
    }
}