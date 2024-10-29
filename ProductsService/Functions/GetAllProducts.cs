using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using ShoppingCart.Core.Interfaces;

namespace ProductsService.Functions;

public class GetAllProducts
{
    private readonly ILogger<GetAllProducts> _logger;
    private readonly IProductService _productService;

    public GetAllProducts(ILogger<GetAllProducts> logger, IProductService productService)
    {
        _logger = logger;
        _productService = productService;
    }

    [Function("GetAllProducts")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Function, "get", Route = "products")] HttpRequestData req)
    {
        _logger.LogInformation("Fetching all products...");

        // Obtener todos los productos desde el servicio
        var products = await _productService.GetAllProductsAsync();

        // Crear una respuesta con los productos en formato JSON
        var response = req.CreateResponse(System.Net.HttpStatusCode.OK);
        await response.WriteAsJsonAsync(products);

        return response;
    }
}