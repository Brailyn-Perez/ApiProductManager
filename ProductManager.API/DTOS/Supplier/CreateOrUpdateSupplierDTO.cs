using System.ComponentModel.DataAnnotations;

namespace ProductManager.API.DTOS.Supplier
{
    public class CreateOrUpdateSupplierDTO
    {
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
