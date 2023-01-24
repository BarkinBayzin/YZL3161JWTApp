using Microsoft.AspNetCore.Authentication.JwtBearer;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

//İlgili API controller endpointlerini yakalamak için HttpClient ekliyorum.
builder.Services.AddHttpClient();

//Token ayarlamaları gerçekleştirilir.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddCookie(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    //hangi path ile login olur, sisteme tanıtıyorum
    opt.LoginPath = "/Account/SignIn";
    opt.LogoutPath = "/Account/LogOut"; //logout url'im
    opt.AccessDeniedPath = "/Account/AccessDenied";//eğer kullanıcının yetkiis yoksa
    opt.Cookie.SameSite = SameSiteMode.Strict; //sadece o domainde kullansın
    opt.Cookie.HttpOnly = true; // diyerek js saldırılarından koruyorum.
    opt.Cookie.SecurePolicy = CookieSecurePolicy.SameAsRequest; //ssl ayarlaması cookie için
    opt.Cookie.Name = "JwtCookie";
}); //cookie ayarlamaları artık tamam, sıra okumada.


var app = builder.Build();

app.UseRouting();

//middlewareların çağırılma sırasına dikkat ediyorum
app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapDefaultControllerRoute();
});

app.Run();
