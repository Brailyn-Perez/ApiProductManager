﻿using ProductManager.BL.DTOS.Supplier;
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

        public async Task<OperationResult> Remove(DeleteSupplierDTO dto)
        {
            OperationResult result = new OperationResult();
            try
            {
                var existSupplier = await _repository.ExistsAsync(x => x.Id == dto.Id && x.Deleted == false);

                var supplierHasProduct = await _repository.SupplierHasProduct(dto.Id);

                if (!supplierHasProduct)
                {
                    result.Success = false;
                    result.Message = "El Suplidor tiene productos asociados";
                    return result;
                }

                if (!existSupplier)
                {
                    result.Success = false;
                    result.Message = "El Suplidor no existe";
                    return result;
                }

                var supplier = await _repository.GetEntityByIdAsync(dto.Id);
                supplier.Deleted = true;

                await _repository.UpdateEntityAsync(supplier);

            }
            catch (Exception ex)
            {
                result.Success = false;
                result.Message = ex.Message;
            }

            return result;
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
