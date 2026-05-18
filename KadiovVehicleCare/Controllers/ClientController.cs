using KadiovVehicleCare.Interfaces;
using KadiovVehicleCare.Models;
using KadiovVehicleCare.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace KadiovVehicleCare.Controllers
{
    [Authorize(Roles = "Admin,User")]
    public class ClientController : Controller
    {
        private readonly IClientRepository _clientRepository;

        public ClientController(IClientRepository clientRepository)
        {
            _clientRepository = clientRepository;
        }

        public async Task<IActionResult> Index(string searchString)
        {
            var clients = await _clientRepository.GetAllAsync();

            if (User.IsInRole("User"))
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                clients = clients.Where(c => c.UserId == currentUserId);
            }

            if (!string.IsNullOrWhiteSpace(searchString))
            {
                searchString = searchString.ToLower();

                clients = clients.Where(c =>
                    c.FirstName.ToLower().Contains(searchString) ||
                    c.LastName.ToLower().Contains(searchString) ||
                    c.PhoneNumber.ToString().Contains(searchString) ||
                    (c.Email != null && c.Email.ToLower().Contains(searchString)));
            }

            if (User.IsInRole("User"))
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var existingClient = await _clientRepository.GetByUserIdAsync(currentUserId!);
                ViewBag.CanCreateClient = existingClient == null;
            }
            else
            {
                ViewBag.CanCreateClient = true;
            }

            ViewBag.SearchString = searchString;

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

            if (User.IsInRole("User"))
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (client.UserId != currentUserId)
                {
                    return Forbid();
                }
            }

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

        public async Task<IActionResult> Create()
        {
            if (User.IsInRole("User"))
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                var existingClient = await _clientRepository.GetByUserIdAsync(currentUserId!);

                if (existingClient != null)
                {
                    TempData["Error"] = "От този профил вече е създаден клиент.";
                    return RedirectToAction(nameof(Index));
                }
            }

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateClientViewModel viewModel)
        {
            if (!ModelState.IsValid)
                return View(viewModel);

            string? currentUserId = null;

            if (User.IsInRole("User"))
            {
                currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                var existingClient = await _clientRepository.GetByUserIdAsync(currentUserId!);
                if (existingClient != null)
                {
                    ModelState.AddModelError("", "От този профил вече е създаден клиент.");
                    return View(viewModel);
                }
            }

            var client = new Client
            {
                FirstName = viewModel.FirstName,
                LastName = viewModel.LastName,
                PhoneNumber = viewModel.PhoneNumber,
                Email = viewModel.Email,
                CreatedAt = DateTime.Now,
                UserId = currentUserId
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

            if (User.IsInRole("User"))
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (client.UserId != currentUserId)
                {
                    return Forbid();
                }
            }

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

            if (User.IsInRole("User"))
            {
                var currentUserId = User.FindFirstValue(ClaimTypes.NameIdentifier);
                if (client.UserId != currentUserId)
                {
                    return Forbid();
                }
            }

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
        [Authorize(Roles = "Admin")]
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
        [Authorize(Roles = "Admin")]
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
