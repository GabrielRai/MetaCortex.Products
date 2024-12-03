
using MetaCortex.Products.DataAccess.Class;
using MetaCortex.Products.DataAccess.Interface;
using MongoDB.Bson;

namespace MetaCortex.Products.API.Endpoints
{
    public static class ProductEndpointExtensions
    {
        public static IEndpointRouteBuilder MapProductEndpoints(this IEndpointRouteBuilder app)
        {
            var repository = app.ServiceProvider.GetRequiredService<IProductRepository>();

            app.MapGet("/products", async (IProductRepository repository) =>
            {
                return await repository.GetProducts();
            });

            app.MapGet("/products/{id}", async (IProductRepository repository, string id) =>
            {
                return await repository.GetProduct(id);
            });

            app.MapPost("/products", async (IProductRepository repository, Product product) =>
            {
                return await repository.CreateProduct(product);
            });

            app.MapPut("/products/{id}", async (IProductRepository repository, string id, Product product) =>
            {
                await repository.UpdateProduct(id, product);
                return Results.NoContent();
            });

            app.MapDelete("/products/{id}", async (IProductRepository repository, string id) =>
            {
                await repository.DeleteProduct(id);
                return Results.NoContent();
            });

            return app;
        }
    }
}
