using ProductManager.API.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManager.API.Entities
{
    [Table("Category")]
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
