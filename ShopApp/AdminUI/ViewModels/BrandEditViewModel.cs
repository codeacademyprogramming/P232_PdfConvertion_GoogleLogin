using System.ComponentModel.DataAnnotations;

namespace AdminUI.ViewModels
{
    public class BrandEditViewModel
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
