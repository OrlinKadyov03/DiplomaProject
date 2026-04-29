using System.ComponentModel.DataAnnotations;

namespace KadiovVehicleCare.ViewModels
{
    public class CarViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Марка")]
        public string Brand { get; set; } 

        [Display(Name = "Модел")]
        public string Model { get; set; } 

        [Display(Name = "Регистрационен номер")]
        public string PlateNumber { get; set; } 

        [Display(Name = "Година")]
        public int Year { get; set; }

        [Display(Name = "Цвят")]
        public string? Color { get; set; }

        [Display(Name = "Клиент")]
        public string ClientFullName { get; set; } 
    }
}
