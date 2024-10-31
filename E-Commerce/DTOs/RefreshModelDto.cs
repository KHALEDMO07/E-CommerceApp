namespace E_Commerce.DTOs
{
    public class RefreshModelDto
    {
        public required string accessToken {  get; set; }   
        public required string refreshToken { get; set; }
    }
}
