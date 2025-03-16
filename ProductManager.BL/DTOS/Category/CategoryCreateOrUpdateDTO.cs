using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ProductManager.BL.DTOS.Category
{
    public class CategoryCreateOrUpdateDTO
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        [NotNull]
        public string Name { get; set; }
    }
}
