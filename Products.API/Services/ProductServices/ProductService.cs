using MetaCortex.Products.API.Dtos;
using MetaCortex.Products.API.Services.Interfaces;
using MetaCortex.Products.DataAccess.Interface;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace MetaCortex.Products.API.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _productRepository;
        public ProductService(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }
        public async Task UpdateProductOrderStock(string product)
        {
            try
            {
                if (string.IsNullOrEmpty(product))
                    throw new ArgumentException("Product input is null or empty.");

                var products = JsonSerializer.Deserialize<List<ProductDto>>(product);

                if (products == null || !products.Any())
                    throw new Exception("Deserialization failed or no products found.");

                if (_productRepository == null)
                    throw new InvalidOperationException("Product repository is not initialized.");

                foreach (var p in products)
                {
                    if (p == null || string.IsNullOrEmpty(p.Name) || p.Quantity <= 0)
                    {
                        Console.WriteLine($"Skipping invalid product: {p?.Name ?? "Unknown"} (Quantity: {p?.Quantity ?? 0})");
                        continue;
                    }

                    await _productRepository.UpdateProductOrderStock(p.Name, p.Quantity);
                    Console.WriteLine($"Product {p.Name} updated with quantity {p.Quantity}");
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine($"JSON deserialization failed: {ex.Message}");
                throw new Exception("Failed to parse product JSON.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"An error occurred: {ex.Message}");
                throw;
            }
        }
    }
}
