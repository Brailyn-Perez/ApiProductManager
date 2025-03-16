using ProductManager.BL.DTOS.Supplier;
using ProductManager.BL.Interfaces.Supplier;
using ProductManager.DAL.Base;
using ProductManager.DAL.Interfaces.Repositories;

namespace ProductManager.BL.Services.Supplier
{
    public class SupplierService : ISupplierService
    {
        private readonly ISupplierRepository _repository;

        public SupplierService(ISupplierRepository repository)
        {
            _repository = repository;
        }

        public async Task<OperationResult> GeAll()
        {
            OperationResult result = new OperationResult();
            try
            {
                var suppliers = await _repository.GetAllAsync();
                suppliers.Select(x => new SupplierDTO
                {
                    Id = x.Id,
                    Name = x.Name,
                    Contact = x.Contact
                }).ToList();
                result.Success = true;
                result.Data = suppliers;
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
                var supplier = await _repository.GetEntityByIdAsync(id);
                result.Data = new SupplierDTO
                {
                    Id = supplier.Id,
                    Name = supplier.Name,
                    Contact = supplier.Contact
                };
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public Task<OperationResult> Remove(DeleteSupplierDTO dto)
        {
            throw new NotImplementedException();
        }

        public async Task<OperationResult> Save(CreateOrUpdateSupplierDTO dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var supplier = new DAL.Entities.Supplier
                {
                    Name = dto.Name,
                    Contact = dto.Contact
                };
                await _repository.SaveEntityAsync(supplier);
                result.Success = true;
            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }
            return result;
        }

        public async Task<OperationResult> Update(UpdateSupplierDTO dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var supplier = new DAL.Entities.Supplier
                {
                    Id = dto.Id,
                    Name = dto.Name,
                    Contact = dto.Contact
                };
                await _repository.UpdateEntityAsync(supplier);
                result.Success = true;
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
