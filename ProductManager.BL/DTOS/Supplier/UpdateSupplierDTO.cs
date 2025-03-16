
using System.ComponentModel.DataAnnotations;

namespace ProductManager.BL.DTOS.Supplier
{
    public class UpdateSupplierDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Name { get; set; }
        [Required]
        [MinLength(3)]
        [MaxLength(50)]
        public string Contact { get; set; }
    }
}
