using Proxy_shopping.Models;
using Proxy_shopping.service;
using Microsoft.EntityFrameworkCore;

namespace Proxy_shopping.service
{
    public class OrderService : IOrderService
    {
        private readonly ProxyContext _db;

        public OrderService(ProxyContext db)
        {
            _db = db;
        }
        // 取得訂單清單
        public async Task<List<OrderDto>> GetOrdersAsync()
        {
            var orders = await _db.Orders
                .Include(o => o.Product)
                 .Where(o => o.OrderStatus == OrderStatus.pending)
                .ToListAsync();

            
            return orders
                .Where(o => o.Product != null)
                .Select(order => new OrderDto
            {
                    OrderId = order.OrderId,
                    Img = order.Product!.ImageUrl ?? "/images/iphone13.jpg",
                    ProductType = order.Product.ProductType,
                Price = order.Product.Price,
                Category = order.Product.Category,
                CreatedAt = order.Product.CreatedAt,
                Title = order.Product.Title,
                Location = order.Product.Location,
                ShoppingAddress = order.Product.ShippingAddress
            }).ToList();
        }

        // 取得單筆的清單  單筆顯示用的
        public async Task<OrderDto?> GetOrderByIdAsync(int orderId)
        {
            var order = await _db.Orders
                .Include(o => o.Product )
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
                return null;

            return new OrderDto
            {
                OrderId = order.OrderId,
                Img = order.Product!.ImageUrl,
                ProductType = order.Product.ProductType,
                Price = order.Product.Price,
                Category = order.Product.Category,
                CreatedAt = order.Product.CreatedAt,
                Title = order.Product.Title,
                Location = order.Product.Location,
                ShoppingAddress = order.Product.ShippingAddress
            };
        }
        // 賣方 按下取單之後
        public async Task TakeOrderAsync(int orderId, int userId)
        {
            var order = await _db.Orders
                .Include(o => o.Product)
                .FirstOrDefaultAsync(o => o.OrderId == orderId);

            if (order == null)
                throw new Exception("訂單不存在");

            if (order.OrderStatus != OrderStatus.pending)
                throw new Exception("此訂單已被取走");

            var user = await _db.Users.FirstOrDefaultAsync(u => u.Uid == order.SellerId);

            if (user == null)
                throw new Exception("使用者不存在");

            var amount = order.Product!.Price;

            if (user.Balance < amount)
                throw new Exception("餘額不足");

            // 1️ 扣使用者錢包
            user.Balance -= amount;

            // 2️ 金額進 order扣住
            order.EscrowAmount = amount;

            // 3️ 訂單狀態改為 Taken
            order.OrderStatus = OrderStatus.Taken;

            await _db.SaveChangesAsync();
        }
    }
}
