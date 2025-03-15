using Microsoft.EntityFrameworkCore;

namespace ProductManager.API.Base.Validator
{
    public static class Validator
    {
        public static bool isValidId(int id)
        {
            return id > 0;
        }
    }
}
