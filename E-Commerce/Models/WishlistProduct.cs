namespace E_Commerce.Models
{
    public class WishlistProduct
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public int WishlistId { get; set; }

        public Product product { get; set; }

        public Wishlist whishlist { get; set; }

       
        public decimal TotalPrice { get; set; }
    }
}
