namespace E_Commerce.Interfaces
{
    public interface IUnitOfWork:IDisposable
    {
        IUserService _userService { get; }
        ICategoryService _categoryService { get; }
        IProductService _productService { get; }
        IWishlistService _wishlistService { get; }
        IWishlistProductService _wishlistProductService { get; }
        ICartService _cartService { get; }
        ICartProductService _cartProductService { get; }
        IOrderService _orderService { get; }
        IOrderItemService _orderItemService { get; }    
        IShipmentService _shipmentService { get; }

        IPaymentService _paymentService { get; }
        IShipmentPriceService _shipmentPriceService { get; }
        int Complete(); 
    }
}
