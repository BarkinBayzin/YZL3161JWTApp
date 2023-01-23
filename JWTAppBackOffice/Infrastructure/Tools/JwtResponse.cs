namespace JWTAppBackOffice.Infrastructure.Tools
{
    public class JwtResponse
    {
        public JwtResponse(string token, DateTime expireDate)
        {
            Token = token;
            ExpireDate = expireDate;
        }

        public string Token { get; set; }
        public DateTime ExpireDate { get; set; }
    }
}
