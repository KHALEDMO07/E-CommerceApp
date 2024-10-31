namespace E_Commerce.DTOs
{
    public class AddCartProduct
    {
        public int ProductId { get; set; }

        public int CartId { get; set; }

        public int quantity { get; set; }
    }
}
