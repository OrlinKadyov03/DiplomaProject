using System.ComponentModel.DataAnnotations;

namespace KadiovVehicleCare.ViewModels
{
    public class EditClientViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето Име е задължително!")]
        [StringLength(50)]
        [Display(Name = "Име")]
        public string FirstName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето Фамилия е задължително!")]
        [StringLength(50)]
        [Display(Name = "Фамилия")]
        public string LastName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Полето Телефонен номер е задължително!")]
        [Display(Name = "Телефонен номер")]
        public int PhoneNumber { get; set; } 

        [EmailAddress]
        [Display(Name = "Имейл")]
        public string? Email { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
