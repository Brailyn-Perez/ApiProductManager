
using ProductManager.API.Entities;

namespace ProductManager.API.Entities
{
    public class Supplier
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
