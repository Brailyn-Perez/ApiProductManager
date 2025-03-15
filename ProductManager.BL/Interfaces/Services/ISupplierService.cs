using ProductManager.BL.Base;
using ProductManager.BL.DTOS.Supplier;

namespace ProductManager.BL.Interfaces.Services
{
    public interface ISupplierService : IBaseService<CreateOrUpdateSupplierDTO, CreateOrUpdateSupplierDTO, DeleteSupplierDTO>
    {

    }
}
