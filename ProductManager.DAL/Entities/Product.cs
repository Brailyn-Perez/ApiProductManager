using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManager.DAL.Entities
{
    [Table("Products")]
    public class Product
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }

        public Supplier Supplier { get; set; }
        public Category Category { get; set; }
    }
}
