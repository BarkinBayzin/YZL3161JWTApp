using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using JwtAppUI.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Text;
using System.Net;
using Microsoft.AspNetCore.Authorization;

namespace JwtAppUI.Controllers
{
    [Authorize(Roles = "Admin, Member")]
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        private HttpClient CreateClient()
        {
            var client = _httpClientFactory.CreateClient();

            var token = User.Claims.SingleOrDefault(x => x.Type == "accessToken").Value;

            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);

            //Artık token bilgisini aldım, isteklerimi şekillendirebilirim.

            return client;
        }

        public async Task<IActionResult> List()
        {
            HttpClient client = CreateClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:5099/api/Products");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                List<ProductListResponseModel> list = JsonSerializer.Deserialize<List<ProductListResponseModel>>(jsonString, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                foreach (var item in list)
                {
                    var responseCategory = await client.GetAsync($"http://localhost:5099/api/Categories/{item.CategoryId}");
                    var categoryJsonString = await responseCategory.Content.ReadAsStringAsync();
                    var category = JsonSerializer.Deserialize<CategoryListResponseModel>(categoryJsonString, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                    item.Category = category;
                }
                return View(list);
            }
            else if (response.StatusCode == HttpStatusCode.Forbidden) return RedirectToAction("AccessDenied", "Account");
            else return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Create()
        {
            HttpClient client = CreateClient();
            HttpResponseMessage response = await client.GetAsync("http://localhost:5099/api/Categories");
            if (response.IsSuccessStatusCode)
            {
                string jsonString = await response.Content.ReadAsStringAsync();
                List<CategoryListResponseModel> catList = JsonSerializer.Deserialize<List<CategoryListResponseModel>>(jsonString, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                ViewBag.Categories = new SelectList(catList, "Id", "Definition");
                return View();
            }
            else return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Create(ProductCreateRequestModel model)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = CreateClient();

                StringContent requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync("http://localhost:5099/api/Products", requestContent);

                if (response.IsSuccessStatusCode) return RedirectToAction("List");

                else return View(model);
            }
            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var client = this.CreateClient();
            var response = await client.GetAsync("http://localhost:5099/api/Products/" + id);
            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var productModel = JsonSerializer.Deserialize<ProductUpdateRequestModel>(jsonString, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                var responseCat = await client.GetAsync("http://localhost:5099/api/Categories");
                if (responseCat.IsSuccessStatusCode)
                {
                    var catJsonString = await responseCat.Content.ReadAsStringAsync();
                    var list = JsonSerializer.Deserialize<List<CategoryListResponseModel>>(catJsonString, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                    ViewBag.Categories = new SelectList(list, "Id", "Definition");
                    return View(productModel);
                }
                else return RedirectToAction("Index", "Home");
            }
            else return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public async Task<IActionResult> Update(ProductUpdateRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var client = this.CreateClient();

                var requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                var response = await client.PutAsync("http://localhost:5099/api/Products", requestContent);

                return RedirectToAction("List");
            }

            return View();
        }

        public async Task<IActionResult> Remove(int id)
        {
            var client = this.CreateClient();
            await client.DeleteAsync($"http://localhost:5099/api/Products/{id}");
            return RedirectToAction("List");
        }
    }
}
