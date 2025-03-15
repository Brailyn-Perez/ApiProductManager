using ProductManager.BL.DTOS.Category;
using ProductManager.BL.Interfaces.Category;
using ProductManager.DAL.Base;
using ProductManager.DAL.Repositories;

namespace ProductManager.BL.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly CategoryRepository _repository;

        public CategoryService(CategoryRepository repository)
        {
            _repository = repository;
        }

        public Task<OperationResult> GeAll()
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> GeById(int id)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Remove(RemoveCategoryDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Save(CategoryDTO dto)
        {
            throw new NotImplementedException();
        }

        public Task<OperationResult> Update(CategoryCreateOrUpdateDTO dto)
        {
            throw new NotImplementedException();
        }
    }
}
