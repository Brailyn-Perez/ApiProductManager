using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ProductManager.BL.DTOS.Category
{
    public class UpdateCategoryDTO
    {
        [Required]
        [NotNull]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        [NotNull]
        public string Name { get; set; }
    }
}
