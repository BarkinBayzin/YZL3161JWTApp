using JwtAppUI.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
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

            //post url'in nereye gideceğini ve contentini ona veriyorum
            var response = await client.PostAsync("http://localhost:5099/api/Auth/SignIn", requestContent);

            if (response.IsSuccessStatusCode)
            {
                //başarılı olan response'un contenti okunur.
                var jsonData = await response.Content.ReadAsStringAsync();
                JWTResponseModel tokenModel = JsonSerializer.Deserialize<JWTResponseModel>(jsonData, new JsonSerializerOptions
                {
                    PropertyNamingPolicy = JsonNamingPolicy.CamelCase,//null gelmemesi için namingPolicy düzenlemesini gerçekleştiriyorum.
                });

                JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();
                var token = handler.ReadJwtToken(tokenModel.Token);//tokenın direkt kendisini yakaladım

                if (token != null)
                {
                    var roles = token.Claims.Where(x => x.Type == ClaimTypes.Role).Select(x => x.Value);//rol bilgilerini alırım

                    //if(roles.Contains("Admin"))
                    //{
                    //    return Redirect("...");
                    //}

                    var claims = token.Claims.ToList();//Claimslerimi liste haline getiriyorum ki, token'ı frontendden giden istekler içerisine ekleyebileyim, daha sonra ekleme işlemini gerçekleştiriyorum.
                    claims.Add(new Claim("accessToken", tokenModel.Token == null ? "" : tokenModel.Token));

                    ClaimsIdentity identity = new ClaimsIdentity(claims, JwtBearerDefaults.AuthenticationScheme);

                    var authProps = new AuthenticationProperties
                    {
                        AllowRefresh = true, //refreshlenmelere token bitiş süresini yenilemez.
                        ExpiresUtc = tokenModel.ExpireDate,
                        IsPersistent = true,//token süresi bitmediği sürece hatırlansın
                    };

                    await HttpContext.SignInAsync(JwtBearerDefaults.AuthenticationScheme, new ClaimsPrincipal(identity), authProps);
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ModelState.AddModelError("", "Kullancı Adı veya Şifre Yanlış");
                    return View(model);
                }
            }
            else
            {
                //burada else if ler ile diğer status codelar kontrol edilebilir, ben şimdilik kısa tutuyorum.
                ModelState.AddModelError("", "Kullanıcı Adı veya Şifre Yanlış");
                return View(response);
            }
        }

        // API Url = http://localhost:5099/api/Auth/SignIn
        // UI Url = http://localhost:5100/
    }
}
