using System.ComponentModel.DataAnnotations;

namespace AdminUI.ViewModels
{
    public class BrandCreateViewModel
    {
        [Required]
        [MaxLength(20)]
        public string Name { get; set; }
    }
}
