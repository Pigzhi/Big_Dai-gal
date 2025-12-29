using Proxy_shopping.Models;

namespace Proxy_shopping.service
{
    public interface IOrderService
    {
        //  取得訂單清單（給列表頁）
        Task<List<OrderDto>> GetOrdersAsync();

        //  取得單筆訂單（詳情頁）
        Task<OrderDto?> GetOrderByIdAsync(int orderId);

        // 取單（扣錢 → Escrow）
        Task TakeOrderAsync(int orderId, int userId);


    }


}
