using ProductManager.BL.Base;
using ProductManager.BL.DTOS.Supplier;

namespace ProductManager.BL.Interfaces.Supplier
{
    public interface ISupplierService : IBaseService<CreateOrUpdateSupplierDTO, CreateOrUpdateSupplierDTO, DeleteSupplierDTO>
    {

    }
}
