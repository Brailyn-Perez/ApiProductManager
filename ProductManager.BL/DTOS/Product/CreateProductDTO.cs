using System.ComponentModel.DataAnnotations;

namespace ProductManager.BL.DTOS.Product
{
    public class CreateProductDTO
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [Range(1, 10)]
        public decimal Price { get; set; }
        [Required]
        [Range(1, 10)]
        public int Stock { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int SupplierId { get; set; }
        [Required]
        [Range(1, int.MaxValue)]
        public int CategoryId { get; set; }
    }
}
