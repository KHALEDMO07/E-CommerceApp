
namespace E_Commerce.Models
{
    public class Jwt
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public int DurationTime { get; set; }
        public string Key { get; set; }
    }
}
