using System.ComponentModel.DataAnnotations;

namespace KadiovVehicleCare.ViewModels
{
    public class CreateClientViewModel
    {
        [Required]
        [StringLength(50)]
        [Display(Name = "Име")]
        public string FirstName { get; set; } 

        [Required]
        [StringLength(50)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; }

        
        [Required]
        [Display(Name = "Телефон")]
        public int PhoneNumber { get; set; }

        [EmailAddress]
        [Display(Name = "Имейл")]
        public string? Email { get; set; }
    }
}
