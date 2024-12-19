using MetaCortex.Products.API.Dtos;
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
                // Deserialisera produktlistan
                var products = JsonSerializer.Deserialize<List<ProductDto>>(product);

                if (products == null || !products.Any())
                {
                    throw new ArgumentException("Error deserializing product or no products provided.");
                }

                foreach (var p in products)
                {
                    // Kontrollera att namn och kvantitet är giltiga
                    if (string.IsNullOrEmpty(p.Name))
                    {
                        Console.WriteLine("Product name is missing for one item. Skipping...");
                        continue;
                    }

                    if (p.Quantity <= 0)
                    {
                        Console.WriteLine($"Invalid quantity ({p.Quantity}) for product {p.Name}. Skipping...");
                        continue;
                    }

                    // Uppdatera lagersaldo
                    await _productRepository.UpdateProductOrderStock(p.Name, p.Quantity);
                    Console.WriteLine("Product {0} updated with quantity {1}", p.Name, p.Quantity);
                }
            }
            catch (JsonException ex)
            {
                Console.WriteLine("JSON deserialization failed: " + ex.Message);
                throw new Exception("Failed to parse product JSON.", ex);
            }
            catch (Exception ex)
            {
                Console.WriteLine("An error occurred: " + ex.Message);
                throw; // Vidarebefordra undantaget
            }
        }

    }
}
