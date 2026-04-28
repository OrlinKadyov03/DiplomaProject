using System.ComponentModel.DataAnnotations;

namespace KadiovVehicleCare.ViewModels
{
    public class EditClientViewModel
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Име")]
        public string FirstName { get; set; } = string.Empty;

        [Required]
        [StringLength(50)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Телефон")]
        public int PhoneNumber { get; set; } 

        [EmailAddress]
        [Display(Name = "Имейл")]
        public string? Email { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
