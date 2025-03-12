﻿namespace ProductManager.API.DTOS.Product
{
    public class CreateProductDTO
    {
        public string Name { get; set; }
        public decimal Price { get; set; }
        public int Stock { get; set; }
        public int SupplierId { get; set; }
        public int CategoryId { get; set; }
    }
}
