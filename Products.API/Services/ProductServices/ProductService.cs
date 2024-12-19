﻿using MetaCortex.Products.API.Dtos;
using MetaCortex.Products.DataAccess.Interface;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MetaCortex.Products.API.Services.ProductServices
{
    public class ProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService()
        {
          
        }

        public async Task UpdateProductOrderStock(string product)
        {
            try
            {
             
                var products = JsonSerializer.Deserialize<List<ProductDto>>(product);
                Console.WriteLine(products);

                if (products == null)
                {
                    throw new Exception("Error deserializing product");
                }
                
                foreach (var p in products)
                {
                    await _productRepository.UpdateProductOrderStock(p.Name, p.Quantity);
                    Console.WriteLine("Product {0} updated with quantity {1}", p.Name, p.Quantity);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
