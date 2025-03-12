using System.ComponentModel.DataAnnotations;

namespace ProductManager.API.DTOS.Product
{
    public class UpdateProductDTO
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
    }
}
