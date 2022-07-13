using eTickets.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public interface IOrderService
    {
        //Create 2 methods - to add orders to db
        // - Get all orders from db
        Task StoreOrderAsync(List<ShoppingCartItem> items, int userId, string userEmail);
        Task<List<Order>> GetOrdersByUserIdAsync(int userId);
        Task<List<Order>> GetOrdersByAdminIdAsync(int userId);
        Task<List<Order>> OrderAsync(int id,string status);
    }
}
