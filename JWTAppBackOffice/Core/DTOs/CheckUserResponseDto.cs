namespace JWTAppBackOffice.Core.DTOs
{
    public class CheckUserResponseDto
    {
        public string Username { get; set; }
        public string Role { get; set; }
        public int Id { get; set; }
        public bool IsExists { get; set; }
    }
}
