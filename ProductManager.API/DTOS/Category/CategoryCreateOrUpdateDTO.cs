using System.ComponentModel.DataAnnotations;

namespace ProductManager.API.DTOS.Category
{
    public class CategoryCreateOrUpdateDTO
    {
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }
    }
}
