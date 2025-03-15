using ProductManager.DAL.Base;

namespace ProductManager.BL.Base
{
    public interface IBaseService<DToSave, DToUpdate, DToRemove>
    {
        Task<OperationResult> GeAll();
        Task<OperationResult> GeById(int id);
        Task<OperationResult> Remove(DToRemove dto);
        Task<OperationResult> Update(DToUpdate dto);
        Task<OperationResult> Save(DToSave dto);
    }
}
