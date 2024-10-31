namespace E_Commerce.Models
{
    public class UserAddress
    {
        public int Id { get; set; }

        public User user { get; set; }

        public int UserId { get; set; }

        public string Country {  get; set; }    

        public string City { get; set; }

        public string Region { get; set; }
    }
}
