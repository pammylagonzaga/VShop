using System.Text.Json;
using VShop.Web.Models;

namespace VShop.Web.Service.Contracts
{
    public class CategoryService : ICategoryService
    {
        private readonly IHttpClientFactory _clienteFactory;
        private readonly JsonSerializerOptions _options;
        private const string apiEndpoint = "/api/categories/";

        public CategoryService(IHttpClientFactory clienteFactory)
        {
            _clienteFactory = clienteFactory;
            _options = new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            };
        }
        public async Task<IEnumerable<CategoryViewModel>> GetAllCategories()
        {
            var client = _clienteFactory.CreateClient("ProductApi");

            IEnumerable<CategoryViewModel> categories;

            var response = await client.GetAsync(apiEndpoint);

            if (response.IsSuccessStatusCode)
            {
                var apiResponse = await response.Content.ReadAsStreamAsync();
                categories = await JsonSerializer
                    .DeserializeAsync<IEnumerable<CategoryViewModel>>(apiResponse, _options);
            }
            else
            {
                return null;
            }
            return categories;
        }
    }
}
