using KadiovVehicleCare.Interfaces;
using KadiovVehicleCare.Models;
using KadiovVehicleCare.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace KadiovVehicleCare.Controllers
{

    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IActionResult> Index()
        {
            var clients = await _clientRepository.GetAllAsync();

            var viewModels = clients.Select(c => new ClientViewModel
            {
                Id = c.Id,
                FirstName = c.FirstName,
                LastName = c.LastName,
                PhoneNumber = c.PhoneNumber,
                Email = c.Email,
                CreatedAt = c.CreatedAt
            });

            return View(viewModels);
        }

        public async Task<IActionResult> Details(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null) return NotFound();

            var viewModel = new ClientViewModel
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                CreatedAt = client.CreatedAt
            };

            return View(viewModel);
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClientViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            var client = new Client
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                PhoneNumber = viewModel.PhoneNumber,
                Email = viewModel.Email,
                CreatedAt = DateTime.Now
            };

            var result = await _clientRepository.AddAsync(client);
            if (!result)
            {
                ModelState.AddModelError("", "Неуспешно добавяне на клиент.");
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Edit(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null) return NotFound();

            var viewModel = new EditClientViewModel
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                CreatedAt = client.CreatedAt
            };

            return View(viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, EditClientViewModel viewModel)
        {
            if (id != viewModel.Id) return NotFound();

            if (!ModelState.IsValid)
                return View(viewModel);

            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null) return NotFound();

            client.FirstName = viewModel.FirstName;
            client.LastName = viewModel.LastName;
            client.PhoneNumber = viewModel.PhoneNumber;
            client.Email = viewModel.Email;
            client.CreatedAt = viewModel.CreatedAt;

            var result = await _clientRepository.UpdateAsync(client);
            if (!result)
            {
                ModelState.AddModelError("", "Неуспешно редактиране на клиент.");
                return View(viewModel);
            }

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Delete(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null) return NotFound();

            var viewModel = new ClientViewModel
            {
                Id = client.Id,
                FirstName = client.FirstName,
                LastName = client.LastName,
                PhoneNumber = client.PhoneNumber,
                Email = client.Email,
                CreatedAt = client.CreatedAt
            };

            return View(viewModel);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var client = await _clientRepository.GetByIdAsync(id);
            if (client == null) return NotFound();

            var result = await _clientRepository.DeleteAsync(client);
            if (!result)
            {
                ModelState.AddModelError("", "Неуспешно изтриване на клиент.");
                return RedirectToAction(nameof(Index));
            }

            return RedirectToAction(nameof(Index));
        }
    } 
}
