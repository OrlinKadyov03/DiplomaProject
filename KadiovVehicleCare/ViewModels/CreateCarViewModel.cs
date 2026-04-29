using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace KadiovVehicleCare.ViewModels
{
    public class CreateCarViewModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Марка")]
        public string Brand { get; set; } 

        [Required]
        [StringLength(50)]
        [Display(Name = "Модел")]
        public string Model { get; set; } 

        [Required]
        [StringLength(15)]
        [Display(Name = "Регистрационен номер")]
        public string PlateNumber { get; set; } 

        [Display(Name = "Година")]
        public int Year { get; set; }

        [StringLength(30)]
        [Display(Name = "Цвят")]
        public string? Color { get; set; }

        [Required]
        [Display(Name = "Клиент")]
        public int ClientId { get; set; }

        public IEnumerable<SelectListItem> Clients { get; set; } = new List<SelectListItem>();
    }
}
