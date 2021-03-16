using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetcoreAPI.Models;
using Newtonsoft.Json;
using System.Net.Http;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace dotnetcoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ZarinShopController : ControllerBase
    {
        [HttpPost("RegisterShop")]
        public async Task<ActionResult> RegisterShop(Shop shop)
        {
            HttpClient client = new HttpClient();

            var url = "https://api.zarinpal.com/pg/v4/payment/request.json";
            var parameters = new Dictionary<string, string> { { "merchant_id", "50657202-f9b6-11e7-b1b5-000c295eb8fc" }, { "amount", shop.Price.ToString() }, { "description", shop.Description }, { "callback_url", "http://localhost:4012/PaymentFeedback" }, { "mobile", shop.Mobile }, { "email", shop.Email } };
            var encodedContent = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync(url, new FormUrlEncodedContent(parameters));
            var contents = await response.Content.ReadAsStringAsync();
            Response RZ = JsonConvert.DeserializeObject<Response>(contents);
            var db = new soamanaDB();
            Shop Shop = new Shop();
            if (RZ.data.code == 100)
            {
                try
                {
                    Shop.Mobile = shop.Mobile;
                    Shop.Price = shop.Price;
                    Shop.Email = shop.Email;
                    Shop.DateShop = DateTime.Now;
                    Shop.Description = shop.Price + shop.Email;
                    Shop.OrderId = Convert.ToInt32(shop.OrderId);
                    Shop.Authority = RZ.data.authority;
                    Shop.Status = RZ.data.code;
                    db.Shops.Add(Shop);
                    db.SaveChanges();
                }
                catch
                {
                }
                return Ok(RZ.data.authority);
            }
            else
                return Ok("NOk");
        }

        [HttpPatch("HandleZarinFeedback")]
        public async Task<ActionResult> HandleZarinFeedback(Shop shop)
        {
            var db = new soamanaDB();
            Shop Shop = new Shop();
            Shop = db.Shops.Where(x => x.Authority == shop.Authority).FirstOrDefault();
            int Amount = Convert.ToInt32(Shop.Price);
            HttpClient client = new HttpClient();
            var url = "https://api.zarinpal.com/pg/v4/payment/verify.json";

            var parameters = new Dictionary<string, string> { { "merchant_id", "50657202-f9b6-11e7-b1b5-000c295eb8fc" }, { "amount", Shop.Price.ToString() }, { "authority", Shop.Authority } };
            var encodedContent = new FormUrlEncodedContent(parameters);
            var response = await client.PostAsync(url, new FormUrlEncodedContent(parameters));
            var contents = await response.Content.ReadAsStringAsync();
            ResponseZarinFeedback RZ = JsonConvert.DeserializeObject<ResponseZarinFeedback>(contents);

            // int Id = Convert.ToInt32(Shop.OrderId);
            var Order = db.Orders.SingleOrDefault(x => x.OrderId == Shop.OrderId);

            if (RZ.data.code == "101" || RZ.data.code == "100")
            {
                Shop.RefId = RZ.data.ref_id.ToString();

                Order.OrderStatusId = 3;

                /////////////////////////////////////////////////////////////////////////////// to reduce from quantity of product

                var OrderDetail = db.OrderDetails.Where(x => x.OrderId == Order.OrderId).ToList();

                for (int i = 0; i < OrderDetail.Count; i++)
                {
                    var productId = OrderDetail[i].ProductId;
                    var products = db.Products.Where(x => x.ProductId == productId).FirstOrDefault();
                    products.Quantity = products.Quantity - OrderDetail[i].Count;
                }

                db.SaveChanges();
                return Ok(Shop.RefId);
            }
            else
            {
                Order.OrderStatusId = 2;
                db.SaveChanges();
                return Ok("NOK");
            }
        }

    }
}
