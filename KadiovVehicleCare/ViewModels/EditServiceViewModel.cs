using System.ComponentModel.DataAnnotations;

namespace KadiovVehicleCare.ViewModels
{
    public class EditServiceViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Полето Име е задължително!")]
        [StringLength(100)]
        [Display(Name = "Име")]
        public string Name { get; set; } = string.Empty;

        [StringLength(500)]
        [Display(Name = "Описание")]
        public string? Description { get; set; }
        [Required(ErrorMessage = "Полето Цена е задължително!")]
        [Range(0, 10000)]
        [Display(Name = "Цена")]
        public decimal Price { get; set; }
        [Required(ErrorMessage = "Полето Продължителност (минути) е задължително!")]
        [Range(1, 1000)]
        [Display(Name = "Продължителност (минути)")]
        public int DurationInMinutes { get; set; }
    }
}
