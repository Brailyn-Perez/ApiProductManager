using Microsoft.Extensions.Logging;
using ProductManager.DAL.Base;
using ProductManager.DAL.Entities;
using ProductManager.DAL.Interfaces.Repositories;

namespace ProductManager.DAL.Repositories
{
    public class SupplierRepository : BaseRepository<Supplier>, ISupplierRepository
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<SupplierRepository> _logger;
        public SupplierRepository(ApplicationDbContext context, ILogger<SupplierRepository> logger) : base(context)
        {
            _context = context;
            _logger = logger;

        }
    }
}
