using API大專.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API大專.Controllers
{
    [ApiController]
    [Route("api/Commissions/MyCompleted")]
    public class COMPLETEDCommission : Controller
    {
        private readonly ProxyContext _proxyContext;
        public COMPLETEDCommission(ProxyContext proxyContext)
        {
            _proxyContext = proxyContext;
        }

        [HttpGet]
        public async Task<ActionResult> GetCompletedOrder(string userid)
        {
            //var userid = "101";
            var userExists = await _proxyContext.Users
                     .AnyAsync(u => u.Uid == userid);
            if (!userExists)
            {
                return Unauthorized(new
                {
                    success = false,
                    message = "尚未登入，或是權限不足"
                });
            }
            var Orders = await (
                        from o in _proxyContext.CommissionOrders
                        join c in _proxyContext.Commissions
                        on o.CommissionId equals c.CommissionId
                        join buyerUser in _proxyContext.Users
                        on o.BuyerId equals buyerUser.Uid
                        join sellerUser in _proxyContext.Users
                        on o.SellerId equals sellerUser.Uid
                        where o.Status == "COMPLETED"
                        && (o.BuyerId == userid || o.SellerId ==userid)
                        select new
                        {
                            title = c.Title,
                            imageurl = c.ImageUrl,
                            description = c.Description,
                            location = c.Location,
                            status = o.Status,
                            amount = o.Amount,
                            finishedAt = o.FinishedAt,
                            buyer = new
                            {
                                id = buyerUser.Uid,
                                name = buyerUser.Name
                            },
                            seller = new
                            {
                                id = sellerUser.Uid,
                                name = sellerUser.Name
                            }
                        }
                            ).ToListAsync();
            return Ok(new
            {
                success = true,
                data = Orders
            });

        }
    }
}
