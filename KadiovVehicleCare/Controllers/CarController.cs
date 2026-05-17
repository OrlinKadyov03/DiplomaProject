using KadiovVehicleCare.Data;
using KadiovVehicleCare.Interfaces;
using KadiovVehicleCare.Models;
using KadiovVehicleCare.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace KadiovVehicleCare.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class CarController : Controller
    {
        private readonly ICarRepository _carRepository;
        private readonly IClientRepository _clientRepository;

        public CarController(ICarRepository carRepository, IClientRepository clientRepository)
        {
            _carRepository = carRepository;
            _clientRepository = clientRepository;
        }

        public async Task<IActionResult> Index()
        {
            var cars = await _carRepository.GetAllAsync();

            var viewModels = cars.Select(c => new CarViewModel
            {
                Id = c.Id,
                Brand = c.Brand,
                Model = c.Model,
                PlateNumber = c.PlateNumber,
                Year = c.Year,
                Color = c.Color,
                ClientFullName = c.Client != null ? $"{c.Client.FirstName} {c.Client.LastName}" : string.Empty
            });

            return View(viewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null) return NotFound();

            var viewModel = new CarViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                PlateNumber = car.PlateNumber,
                Year = car.Year,
                Color = car.Color,
                ClientFullName = car.Client != null ? $"{car.Client.FirstName} {car.Client.LastName}" : string.Empty
            };

            return View(viewModel);
        }

        public async Task<IActionResult> Create()
        {
            var clients = await _clientRepository.GetAllAsync();

            var viewModel = new CreateCarViewModel
            {
                Clients = clients.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.FirstName} {c.LastName}"
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateCarViewModel viewModel)
        {
            if (!ModelState.IsValid)
            {
                var clients = await _clientRepository.GetAllAsync();
                viewModel.Clients = clients.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.FirstName} {c.LastName}"
                });

                return View(viewModel);
            }

            var car = new Car
            {
                Brand = viewModel.Brand,
                Model = viewModel.Model,
                PlateNumber = viewModel.PlateNumber,
                Year = viewModel.Year,
                Color = viewModel.Color,
                ClientId = viewModel.ClientId
            };

            var result = await _carRepository.AddAsync(car);
            if (!result)
            {
                ModelState.AddModelError("", "Неуспешно добавяне на автомобил.");

                var clients = await _clientRepository.GetAllAsync();
                viewModel.Clients = clients.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.FirstName} {c.LastName}"
                });

                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null) return NotFound();

            var clients = await _clientRepository.GetAllAsync();

            var viewModel = new EditCarViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                PlateNumber = car.PlateNumber,
                Year = car.Year,
                Color = car.Color,
                ClientId = car.ClientId,
                Clients = clients.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.FirstName} {c.LastName}"
                })
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditCarViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            if (!ModelState.IsValid)
            {
                var clients = await _clientRepository.GetAllAsync();
                viewModel.Clients = clients.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.FirstName} {c.LastName}"
                });

                return View(viewModel);
            }

            var car = await _carRepository.GetByIdAsync(id);
            if (car == null) return NotFound();

            car.Brand = viewModel.Brand;
            car.Model = viewModel.Model;
            car.PlateNumber = viewModel.PlateNumber;
            car.Year = viewModel.Year;
            car.Color = viewModel.Color;
            car.ClientId = viewModel.ClientId;

            var result = await _carRepository.UpdateAsync(car);
            if (!result)
            {
                ModelState.AddModelError("", "Неуспешно редактиране на автомобил.");

                var clients = await _clientRepository.GetAllAsync();
                viewModel.Clients = clients.Select(c => new SelectListItem
                {
                    Value = c.Id.ToString(),
                    Text = $"{c.FirstName} {c.LastName}"
                });

                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }


        public async Task<IActionResult> Delete(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null) return NotFound();

            var viewModel = new CarViewModel
            {
                Id = car.Id,
                Brand = car.Brand,
                Model = car.Model,
                PlateNumber = car.PlateNumber,
                Year = car.Year,
                Color = car.Color,
                ClientFullName = car.Client != null ? $"{car.Client.FirstName} {car.Client.LastName}" : string.Empty
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var car = await _carRepository.GetByIdAsync(id);
            if (car == null) return NotFound();

            var result = await _carRepository.DeleteAsync(car);
            if (!result)
            {
                ModelState.AddModelError("", "Неуспешно изтриване на автомобил.");
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    }
}