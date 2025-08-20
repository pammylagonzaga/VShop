using System.Text;
using System.Text.Json;
using VShop.Web.Models;


namespace VShop.Web.Service.Contracts;

public class ProductService : IProductService
{
    private readonly IHttpClientFactory _clientFactory;
    private const string apiEndpoint = "/api/products/";
    private readonly JsonSerializerOptions _option;
    private ProductViewModel productVM;
    private IEnumerable<ProductViewModel> productsVM;

    public ProductService(IHttpClientFactory clientFactory)
    {
        _clientFactory = clientFactory;
        _option = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };
    }
    public async Task<IEnumerable<ProductViewModel>> GetAllProducts()
    {
        var client = _clientFactory.CreateClient("ProductApi");

        using (var response = await client.GetAsync(apiEndpoint))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productsVM = await JsonSerializer
                    .DeserializeAsync<IEnumerable<ProductViewModel>>(apiResponse, _option);
            }
            else
            {
                return null;
            }
        }
        return productsVM;
    }
    public async Task<ProductViewModel> FindProductById(int id)
    {
        var client = _clientFactory.CreateClient("ProductApi");

        using (var response = await client.GetAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productVM = await JsonSerializer
                    .DeserializeAsync<ProductViewModel>(apiResponse, _option);
            }
            else
            {
                return null;
            }
        }
        return productVM;
    }
    public async Task<ProductViewModel> CreateProduct(ProductViewModel productVM)
    {
        var client = _clientFactory.CreateClient("ProductApi");

        StringContent content = new StringContent(
            JsonSerializer.Serialize(productVM), Encoding.UTF8, "application/json");

        using (var response = await client.PostAsync(apiEndpoint, content))
        {
                if (response.IsSuccessStatusCode)
                {
                    var apiResponse = await response.Content.ReadAsStreamAsync();
                    productVM = await JsonSerializer
                        .DeserializeAsync<ProductViewModel>(apiResponse, _option);
                }
                else
                {
                    return null;
                }
        }
        return productVM;
    }
    public async Task<ProductViewModel> UpdateProduct(ProductViewModel productVM)
    {
        var client = _clientFactory.CreateClient("ProductApi");
        ProductViewModel productUpdate = new ProductViewModel();

        using (var response = await client.PutAsJsonAsync(apiEndpoint, productVM))
        {
            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                productUpdate = await JsonSerializer
                    .DeserializeAsync<ProductViewModel>(apiResponse, _option);
            }
            else
            {
                return null;
            }
        }
        return productUpdate;
    }

    public async Task<bool> DeleteProductById(int id)
    {
        var client = _clientFactory.CreateClient("ProductApi");

        using (var response = await client.DeleteAsync(apiEndpoint + id))
        {
            if (response.IsSuccessStatusCode)
            {
                return true;
            }
        }
        return false;
    }

}
