using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetcoreAPI.Models;

namespace dotnetcoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class OrderController : ControllerBase
    {
        [HttpPost("RegisterOrder")]
        public ActionResult RegisterOrder([FromBody] Order order)
        {
            order.OrderDate = DateTime.Now;
            using (var db = new soamanaDB())
            {
                db.Orders.Add(order);
                db.SaveChanges();
            }
            return Ok(order);
        }

        [HttpPut("RegisterOrderDetails")]
        public IActionResult RegisterOrderDetails(VMproduct product)
        {
            // ((System.Text.Json.JsonElement)products).GetRawText
            var db = new soamanaDB();
            int OrderId = product.OrderId;
            List<OrderDetail> OrderDetails = new List<OrderDetail>();


            for (int i = 0; i < product.products.Length; i++)
            {
                db.OrderDetails.Add(new OrderDetail
                {
                    OrderId = OrderId,
                    ProductId = product.products[i].ProductId,
                    Count = product.products[i].Quantity,
                    TotalCost =Convert.ToInt32 (product.products[i].Quantity * product.products[i].Price)
                });
            }
            db.SaveChanges();
            return Ok(OrderId);
        }


        [HttpPatch("ShoppingHistory")]
        public List<VwShoppingHistory> ShoppingHistory([FromBody] int UserId)
        {
            var db = new soamanaDB();
            // var OrderId = Convert.ToInt32(((JContainer)products)[0]["OrderId"].ToString());

            List<VwShoppingHistory> shoppingHistory = new List<VwShoppingHistory>();

            shoppingHistory = db.VwShoppingHistories.Where(x => x.UserId == UserId).ToList();

            for (int i = 0; i < shoppingHistory.Count; i++)
            {
                shoppingHistory[i].Num = i + 1;
            }

            return (shoppingHistory);
        }

        [HttpGet("ManageShopping")]
        public List<VwShoppingHistory> ManageShopping()
        {
            var db = new soamanaDB();

            List<VwShoppingHistory> shoppingHistory = new List<VwShoppingHistory>();

            shoppingHistory = db.VwShoppingHistories.ToList();

            for (int i = 0; i < shoppingHistory.Count; i++)
            {
                shoppingHistory[i].Num = i + 1;
            }

            return (shoppingHistory);
        }
    }
}