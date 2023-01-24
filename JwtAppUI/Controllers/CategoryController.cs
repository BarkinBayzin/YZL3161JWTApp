using JwtAppUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace JwtAppUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class CategoryController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CategoryController(IHttpClientFactory httpClientFactory)
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
            var client = CreateClient();

            var response = await client.GetAsync("http://localhost:5099/api/Categories");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();

                List<CategoryListResponseModel> list = JsonSerializer.Deserialize<List<CategoryListResponseModel>>(jsonString, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                return View(list);
            }
            else if (response.StatusCode == HttpStatusCode.Unauthorized) return RedirectToAction("Index", "Home");
            else return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> Remove(int id)
        {
            var client = CreateClient();

            await client.DeleteAsync($"http://localhost:5099/api/Categories/{id}");

            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            return View(new CategoryCreateRequestModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CategoryCreateRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var client = CreateClient();

                var requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://localhost:5099/api/Categories", requestContent);

                return RedirectToAction("List");
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var client = CreateClient();

            var response = await client.GetAsync($"http://localhost:5099/api/Categories/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                CategoryUpdateRequestModel catModel = JsonSerializer.Deserialize<CategoryUpdateRequestModel>(jsonString, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                });
                return View(catModel);
            }

            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Update(CategoryUpdateRequestModel model)
        {
            if (ModelState.IsValid)
            {
                HttpClient client = CreateClient();

                StringContent requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PutAsync("http://localhost:5099/api/Categories", requestContent);

                return RedirectToAction("List");
            }

            return View(model);
        }
    }
}
