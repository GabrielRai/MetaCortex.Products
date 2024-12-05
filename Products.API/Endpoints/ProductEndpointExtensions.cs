
using MetaCortex.Products.DataAccess.Class;
using MetaCortex.Products.DataAccess.Interface;
using MetaCortex.Products.DataAccess.RabbitMq;
using MongoDB.Bson;

namespace MetaCortex.Products.API.Endpoints
{
    public static class ProductEndpointExtensions
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            var group = app.MapGroup("api/products");

            group.MapPost("", CreateProduct);
            group.MapGet("{Id}", GetProductById);
            group.MapGet("", GetAllProducts);
            group.MapDelete("{Id}", DeleteProduct);
            group.MapPut("{Id}", UpdateProduct);

            return app;
        }
        public static async Task<IResult> CreateProduct(IProductRepository repo, Product product, IMessageProducerService message)
        {
            var createdProduct = await repo.CreateProduct(product);
            await message.SendProductAsync(product);
            return Results.Created($"/api/products/{createdProduct.Id}", createdProduct);
        }
        public static async Task<IResult> GetProductById(IProductRepository repo, string Id)
        {
            var product = await repo.GetProduct(Id);
            if (product == null)
            {
                return Results.NotFound();
            }
            return Results.Ok(product);
        }
        public static async Task<IResult> GetAllProducts(IProductRepository repo)
        {
            var products = await repo.GetProducts();
            return Results.Ok(products);
        }
        public static async Task<IResult> DeleteProduct(IProductRepository repo, string Id)
        {
            await repo.DeleteProduct(Id);
            return Results.NoContent();
        }
        public static async Task<IResult> UpdateProduct(IProductRepository repo, string Id, Product product)
        {
            await repo.UpdateProduct(Id, product);
            return Results.NoContent();
        }
    }
}
