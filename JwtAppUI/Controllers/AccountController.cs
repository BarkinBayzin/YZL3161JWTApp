using JwtAppUI.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Text.Json;

namespace JwtAppUI.Controllers
{
    public class AccountController : Controller
    {
        //Api ile iletişim kurabilmek için IHttpClientFactory arayüzünü kullanıyorum,
        //bunun sayesinde bir client oluşturup, auth controller içerisindeki login işlemlerini gerçekleştirebiliyorum.
        private readonly IHttpClientFactory _httpClientFactory;
        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }

        public IActionResult SignIn()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> SignIn(UserLoginModel model)
        {
            //İsteği atacak bir client oluşturulur.
            HttpClient client = _httpClientFactory.CreateClient();

            //İçerik json formatına dönüştürülür, bunu eskilerde kullandığımız gibi Newtonsoft.Json gibi düşenebilirsiniz, artık microsoftun kendi kütüphanesini de kullanabilirsiniz.
            StringContent requestContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");

            var response = await client.PostAsync("http://localhost:5099/api/Auth/SignIn", requestContent);

            return View(response);

        }

        // API Url = http://localhost:5099/api/Auth/SignIn
        // UI Url = http://localhost:5100/
    }
}
