using ProductManager.DAL.Base.Entities;
using System.ComponentModel.DataAnnotations.Schema;

namespace ProductManager.DAL.Entities
{
    [Table("Category")]
    public class Category : AuditoryEntity
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Product> Products { get; set; }
    }
}
