namespace JWTAppBackOffice.Infrastructure.Tools
{
    public class JwtSettings
    {
        /*
          ValidAudience = "http://localhost",
        ValidIssuer = "http://localhost",
        ClockSkew = TimeSpan.Zero,
        ValidateLifetime = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("barkinbayzin.net6")), //16 karakterden fazla bir şey vermemi bekler !!
        ValidateIssuerSigningKey = true
         */
        public const string Issuer = "http://localhost";
        public const string Audience = "http://localhost";
        public const string Key = "barkinbayzin.net6"; //16 karakterden fazla bir şey vermemi bekler !!
        public const int Expire = 30;
    }
}
