using ProductManager.BL.Base;
using ProductManager.BL.DTOS.Category;

namespace ProductManager.BL.Interfaces.Category
{
    public interface ICategoryService : IBaseService<CategoryDTO, CategoryCreateOrUpdateDTO, RemoveCategoryDTO>
    {

    }
}
