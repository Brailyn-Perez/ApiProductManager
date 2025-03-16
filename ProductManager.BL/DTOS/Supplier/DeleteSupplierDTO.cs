
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ProductManager.BL.DTOS.Supplier
{
    public class DeleteSupplierDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        [NotNull]
        public int Id { get; set; }
    }
}
