using E_Commerce.Data;
using E_Commerce.Interfaces;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace E_Commerce.Services
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly AppDbContext _context;
        public IUserService _userService { get; private set; }

      //  ICategoryService IUnitOfWork._categoryService => throw new NotImplementedException();

        public IProductService _productService { get; private set; }

       
        public  ICategoryService _categoryService { get; private set; }

        public IWishlistService _wishlistService{ get; private set; }

        public IWishlistProductService _wishlistProductService { get; private set; }

        public ICartService _cartService { get; private set; }

        public ICartProductService _cartProductService { get; private set; }

        public IOrderService _orderService { get; private set; }

        public IOrderItemService _orderItemService { get; private set; }

        public IShipmentService _shipmentService { get; private set; }

        public IPaymentService _paymentService { get; private set; }

        public IShipmentPriceService _shipmentPriceService { get; private set; }

        public UnitOfWork(AppDbContext context , IUserService userService , ICategoryService categoryService
            ,IProductService productService ,IWishlistService wishlistService 
            , IWishlistProductService wishlistProductService , ICartService cartService
            , ICartProductService cartProductService , IOrderService orderService
            ,IOrderItemService orderItemService , IPaymentService paymentService 
            , IShipmentService shipmentService , IShipmentPriceService shipmentPriceService)
        {
            _context = context;
            _userService = userService;
            _categoryService = categoryService;
            _productService = productService;
            _wishlistService = wishlistService;
            _wishlistProductService = wishlistProductService;
            _cartService = cartService;
            _cartProductService = cartProductService;
            _orderService = orderService;
            _orderItemService = orderItemService;
            _paymentService = paymentService;
            _shipmentService = shipmentService;
            _shipmentPriceService = shipmentPriceService;
        }

        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
             _context.Dispose();
        }
    }
}
