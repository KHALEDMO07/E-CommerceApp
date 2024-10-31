namespace E_Commerce.Models
{
    public class Wishlist
    {
        public int Id { get; set; } 

        public int UserId { get; set; }

        public User user { get; set; }

        public List<WishlistProduct>? WishlistProducts { get; set; }

        public decimal TotalPrice { get; set; }

    }
}
