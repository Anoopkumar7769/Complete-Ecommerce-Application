using eTickets.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace eTickets.Data.Services
{
    public class OrderService : IOrderService
    {
        private readonly AppDbContext _context;

        public OrderService(AppDbContext context)
        {
            _context = context;
        }
        public async Task<List<Order>> GetOrdersByUserIdAsync(int userId)
        {
            var orders = await _context.Order.Include(n => n.OrderItem).ThenInclude(n => n.Movie)
                .Where(n => n.UserId == userId).ToListAsync();
            return orders;

        }
        public async Task<List<Order>> GetOrdersByAdminIdAsync(int userId)
        {
            var orders = await _context.Order.Include(n => n.OrderItem).ThenInclude(n => n.Movie)
               .Include(n => n.Register).ToListAsync();
            return orders;
        }

        public async Task StoreOrderAsync(List<ShoppingCartItem> items, int userId, string userEmailAddress)
        {
            var order = new Order()
            {
                UserId = userId,
                Email = userEmailAddress
            };
            await _context.Order.AddAsync(order);
            await _context.SaveChangesAsync();

            foreach(var item in items)
            {
                var orderItem = new OrderItem()
                {
                    Amount = item.Amount,
                    MovieId = item.Movie.Id,
                    OrderId = order.Id,
                    Price = item.Movie.Price
                };
               await _context.OrderItem.AddAsync(orderItem);
            }
            await _context.SaveChangesAsync();
        }

        public async Task<List<Order>> OrderAsync(int id,string status)
        {
            var orders = await _context.Order.Include(n => n.OrderItem).ThenInclude(n => n.Movie)
                .Where(n => n.Id == id).ToListAsync();
            if(status == "confirm")
            {
                orders[0].Order_Status = "Confirm";
            }
            else if(status == "wait")
            {
                orders[0].Order_Status = "Pending";
            }
            else
            {
                orders[0].Order_Status = "Cancelled";
            }
            await _context.SaveChangesAsync();
            return orders;
        }
    }
}
