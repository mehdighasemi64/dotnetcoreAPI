using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetcoreAPI.Models;

namespace dotnetcoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class DeliveryOrderController : ControllerBase
    {      
       [HttpPost("RegisterDeliveryOrder")]
        public ActionResult RegisterDeliveryOrder([FromBody] DeliveryOrder deliveryOrder)
        {
            using (var db = new soamanaDB())
            {
                db.DeliveryOrders.Add(deliveryOrder);
                db.SaveChanges();
            }
            return Ok(deliveryOrder);
        }

        [HttpPut("DeliveryDone")]
        public ActionResult DeliveryDone([FromBody] int orderId)
        {
            var db = new soamanaDB();
            Order order = new Order();
            order = db.Orders.Where(x => x.OrderId == orderId).FirstOrDefault();

            order.OrderStatusId = 4;

            db.SaveChanges();
            return Ok("OK");
        }
    }
}