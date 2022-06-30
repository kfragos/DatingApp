namespace API.DTOs
{
    // This is the object that we return when a user login or register
    public class UserDto
    {
        public string Username { get; set; }
        public string Token { get; set; }
        public string PhotoUrl { get; set; }
    }
}
