using ProductManager.DAL.Entities;
using ProductManager.DAL.Interfaces.Base;

namespace ProductManager.DAL.Interfaces.Repositories
{
    public interface ICategoryRepository : IBaseRepository<Category>
    {
        Task<bool> categoryHasProduct(int id);
    }
}
