using KadiovVehicleCare.Interfaces;
using KadiovVehicleCare.Models;
using KadiovVehicleCare.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KadiovVehicleCare.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class ServiceController : Controller
    {
        private readonly IServiceRepository _serviceRepository;

        public ServiceController(IServiceRepository serviceRepository)
        {
            _serviceRepository = serviceRepository;
        }

        public async Task<IActionResult> Index()
        {
            var service = await _serviceRepository.GetAllAsync();

            var serviceViewModels = service.Select(s => new ServiceViewModel
            {
                Id = s.Id,
                Name = s.Name,
                Description = s.Description,
                Price = s.Price,
                DurationInMinutes = s.DurationInMinutes
            });

            return View(serviceViewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null) return NotFound();

            var viewModel = new ServiceViewModel
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                DurationInMinutes = service.DurationInMinutes
            };

            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]
        public IActionResult Create()
        {
            return View();
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateServiceViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var service = new Service
            {
                Name = viewModel.Name,
                Description = viewModel.Description,
                Price = viewModel.Price,
                DurationInMinutes = viewModel.DurationInMinutes
            };

            var result = await _serviceRepository.AddAsync(service);
            if (!result)
            {
                ModelState.AddModelError("", "Неуспешно добавяне на услуга.");
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null) return NotFound();

            var viewModel = new EditServiceViewModel
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                DurationInMinutes = service.DurationInMinutes
            };

            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditServiceViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(viewModel);

            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null) return NotFound();

            service.Name = viewModel.Name;
            service.Description = viewModel.Description;
            service.Price = viewModel.Price;
            service.DurationInMinutes = viewModel.DurationInMinutes;

            var result = await _serviceRepository.UpdateAsync(service);
            if (!result)
            {
                ModelState.AddModelError("", "Неуспешно редактиране на услуга.");
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Delete(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null) return NotFound();

            var viewModel = new ServiceViewModel
            {
                Id = service.Id,
                Name = service.Name,
                Description = service.Description,
                Price = service.Price,
                DurationInMinutes = service.DurationInMinutes
            };

            return View(viewModel);
        }
        [Authorize(Roles = "Admin")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var service = await _serviceRepository.GetByIdAsync(id);
            if (service == null) return NotFound();

            var result = await _serviceRepository.DeleteAsync(service);
            if (!result)
            {
                ModelState.AddModelError("", "Неуспешно изтриване на услуга.");
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}