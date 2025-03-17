using ProductManager.DAL.Base.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManager.DAL.Entities
{
    [Table("Supplier")]
    public class Supplier : AuditoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Contact { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
