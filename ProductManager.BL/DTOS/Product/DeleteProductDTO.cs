using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ProductManager.BL.DTOS.Product
{
    public class DeleteProductDTO
    {
        [Required]
        [NotNull]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}
