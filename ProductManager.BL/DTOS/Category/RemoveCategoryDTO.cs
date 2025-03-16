using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ProductManager.BL.DTOS.Category
{
    public class RemoveCategoryDTO
    {
        [Required]
        [NotNull]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}
