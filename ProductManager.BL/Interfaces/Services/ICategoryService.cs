using ProductManager.BL.Base;
using ProductManager.BL.DTOS.Category;

namespace ProductManager.BL.Interfaces.Services
{
    public interface ICategoryService : IBaseService<CategoryDTO, CategoryCreateOrUpdateDTO, RemoveCategoryDTO>
    {

    }
}
