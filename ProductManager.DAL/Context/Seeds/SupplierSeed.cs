using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using ProductManager.DAL.Entities;

namespace ProductManager.DAL.Context.Seeds
{
    public class SupplierSeed : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasQueryFilter(x => !x.Deleted);
        }
    }
}
