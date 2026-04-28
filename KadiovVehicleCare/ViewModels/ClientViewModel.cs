using System.ComponentModel.DataAnnotations;

namespace KadiovVehicleCare.ViewModels
{
    public class ClientViewModel
    {
        public int Id { get; set; }

        [Display(Name = "Име")]
        public string FirstName { get; set; } 

        [Display(Name = "Фамилия")]
        public string LastName { get; set; } 

        [Display(Name = "Телефон")]
        public int PhoneNumber { get; set; } 

        [Display(Name = "Имейл")]
        public string? Email { get; set; }

        [Display(Name = "Дата на създаване")]
        public DateTime CreatedAt { get; set; }
    }
}
