using JwtAppUI.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace JwtAppUI.Controllers
{
    [Authorize(Roles = "Admin")]
    public class SupplierController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public SupplierController(IHttpClientFactory httpClientFactory)
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

            var response = await client.GetAsync("http://localhost:5099/api/Suppliers");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var list = JsonSerializer.Deserialize<List<SupplierListResponseModel>>(jsonString, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                return View(list);
            }
            return RedirectToAction("Index","Home");
        }

        public async Task<IActionResult> Remove(int id)
        {
            var client = CreateClient();

            await client.DeleteAsync($"http://localhost:5099/api/Suppliers/{id}");

            return RedirectToAction("List");
        }

        public IActionResult Create()
        {
            return View(new SupplierCreateRequestModel());
        }

        [HttpPost]
        public async Task<IActionResult> Create(SupplierCreateRequestModel model)
        {
            if(ModelState.IsValid)
            {
                var client = CreateClient();

                var requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                var response = await client.PostAsync("http://localhost:5099/api/Suppliers", requestContent);

                return RedirectToAction("List");
            }
            return View(model);
        }

        public async Task<IActionResult> Update(int id)
        {
            var client = CreateClient();

            var response = await client.GetAsync($"http://localhost:5099/api/Suppliers/{id}");

            if (response.IsSuccessStatusCode)
            {
                var jsonString = await response.Content.ReadAsStringAsync();
                var supModel = JsonSerializer.Deserialize<SupplierUpdateRequestModel>(jsonString, new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

                return View(supModel);
            }
            return RedirectToAction("List");
        }

        [HttpPost]
        public async Task<IActionResult> Update(SupplierUpdateRequestModel model)
        {
            if (ModelState.IsValid)
            {
                var client = CreateClient();

                var requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

                var response = await client.PutAsync("http://localhost:5099/api/Suppliers", requestContent);

                return RedirectToAction("List");
            }
            return View(model);
        }
    }
}
