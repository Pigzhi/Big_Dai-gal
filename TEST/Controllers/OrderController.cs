using Proxy_shopping.Models;
using Proxy_shopping.service;
using Azure.Core;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Proxy_shopping.Controllers
{
   
    [Route("api/orders")] //這個 Controller 底下的所有 API，都會以 /api/orders 開頭
    public class OrderController : ControllerBase
    {
        private readonly IOrderService _orderService;
        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;
        }

        // 前端要顯示的訂單
        [HttpGet("list")]
        public async Task<IActionResult> GetOrders()
        {
            var orders = await _orderService.GetOrdersAsync();

            if (orders.Count == 0)
                return NotFound(new { message = "找不到訂單" });

            return Ok(orders);
        }

        // 取一個訂單 顯示用的
        [HttpGet("{id}")]
        public async Task<IActionResult> GetOrder(int id)
        {
            var order = await _orderService.GetOrderByIdAsync(id);
            return  (order == null) ? NotFound()  : Ok(order) ;
        }

        //賣方 按下取得訂單
        [HttpPost("{id}/take")]
        public async Task<IActionResult> TakeOrder(int id)
        {
            try
            {

                int userId =1;   

                await _orderService.TakeOrderAsync(id, userId);

                return Ok(new { message = "取單成功" });
            }
            catch (Exception ex)
            {
                return BadRequest(new { message = ex.Message });
            }
        }
    }
}
