﻿using System.ComponentModel.DataAnnotations;

namespace ProductManager.BL.DTOS.Product
{
    public class UpdateProductDTO
    {
        [Required]
        [Range(1, int.MaxValue)]
        public int Id { get; set; }
        [Required]
        [MaxLength(50)]
        [MinLength(3)]
        public string Name { get; set; }
        [Required]
        [Range(1, 10)]
        public decimal Price { get; set; }
        [Required]
        [Range(1, 10)]
        public int Stock { get; set; }
    }
}
