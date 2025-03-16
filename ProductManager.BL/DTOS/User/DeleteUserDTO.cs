
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;

namespace ProductManager.BL.DTOS.User
{
    public class DeleteUserDTO
    {
        [Required]
        [NotNull]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
    }
}
