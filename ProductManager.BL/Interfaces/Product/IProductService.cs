using ProductManager.BL.Base;
using ProductManager.BL.DTOS.Product;

namespace ProductManager.BL.Interfaces.Product
{
    public interface IProductService : IBaseService<CreateProductDTO, UpdateProductDTO, DeleteProductDTO>
    {
    }
}
