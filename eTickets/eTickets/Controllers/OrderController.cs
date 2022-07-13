using eTickets.Data.Cart;
using eTickets.Data.Services;
using eTickets.Data.ViewModels;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Threading.Tasks;

namespace eTickets.Controllers
{
    public class OrderController : Controller
    {
        private readonly IMovieService _movieService;
        private readonly ShoppingCart _shoppingCart;
        private readonly IOrderService _orderService;
        public OrderController(IMovieService movieService, ShoppingCart shoppingCart, IOrderService orderService)
        {
            _orderService = orderService;
            _movieService = movieService;
            _shoppingCart = shoppingCart;
        }   
        public async Task<IActionResult> Index()
        {
            string rn = (string)TempData["Role"];
            int userId = (int)HttpContext.Session.GetInt32("id");
           
            if (rn == "3" || rn == null)
            {
                var orders = await _orderService.GetOrdersByAdminIdAsync(userId);
                return View(orders);
            }
            else
            {
                var orders = await _orderService.GetOrdersByUserIdAsync(userId);
                return View(orders);
            }
        }

        public IActionResult ShoppingCart()
        {
            //Returns list of Shopping Cart Items
            var items = _shoppingCart.GetShoppingCartItem();
            _shoppingCart.ShoppingCartItem = items;

            var response = new ShoppingCartVM()
            {
                ShoppingCart = _shoppingCart,
                ShoppingCartTotal = _shoppingCart.GetShoppingCardTotal()
            };

            return View(response);
        }

        public async Task<IActionResult> AddItemToShoppingCart(int id)
        {
            var item = await _movieService.GetMovieByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.AddItemToCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));
        }

        public async Task<IActionResult> RemoveItemFromShoppingCart(int id)
        {
            var item = await _movieService.GetMovieByIdAsync(id);
            if (item != null)
            {
                _shoppingCart.RemoveItemFromCart(item);
            }
            return RedirectToAction(nameof(ShoppingCart));

        }

        public async Task<IActionResult> CompleteOrder()
        {
            var items = _shoppingCart.GetShoppingCartItem();
            int userId = (int)HttpContext.Session.GetInt32("id");
            string userEmailAddress = HttpContext.Session.GetString("Email");
            await _orderService.StoreOrderAsync(items, userId, userEmailAddress);
            await _shoppingCart.ClearShoppingCartAsync();
            return View("OrderCompleted");
        }

        public async Task<IActionResult> Order(int id,string status)
        {
            await _orderService.OrderAsync(id, status);
            return RedirectToAction("Index");
        }
    }
}
