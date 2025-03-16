using ProductManager.BL.DTOS.Category;
using ProductManager.BL.Interfaces.Category;
using ProductManager.DAL.Base;
using ProductManager.DAL.Interfaces.Repositories;

namespace ProductManager.BL.Services.Category
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repository;

        public CategoryService(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> GeAll()
        {
            OperationResult result = new OperationResult();
            try
            {
                var categories = await _repository.GetAllAsync();
                categories.Select(x => new CategoryDTO
                {
                    Id = x.Id,
                    Name = x.Name
                }).ToList();
                result.Success = true;
                result.Data = categories;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<OperationResult> GeById(int id)
        {
            OperationResult result = new OperationResult();
            try
            {
                var category = await _repository.GetEntityByIdAsync(id);
                result.Data = new CategoryDTO
                {
                    Id = category.Id,
                    Name = category.Name
                };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public Task<OperationResult> Remove(RemoveCategoryDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> Save(CategoryCreateOrUpdateDTO dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var category = new DAL.Entities.Category
                {
                    Name = dto.Name
                };
                await _repository.SaveEntityAsync(category);
                result.Success = true;
                result.Message = "Categoria Creada Correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
        }

        public async Task<OperationResult> Update(UpdateCategoryDTO dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var category = new DAL.Entities.Category
                {
                    Id = dto.Id,
                    Name = dto.Name
                };
                await _repository.UpdateEntityAsync(category);
                result.Success = true;
                result.Message = "Categoria Actualizada Correctamente";
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }
    }
}
