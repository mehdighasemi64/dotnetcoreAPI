using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json.Linq;
using dotnetcoreAPI.Models;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace dotnetcoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductController : ControllerBase
    {
        [HttpGet("AllProduct")]
        public List<VwProductImage> AllProduct()
        {
            var db = new soamanaDB();
            List<VwProductImage> list = db.VwProductImages.ToList();
            return (list);
        }

        [HttpPost("RegisterProduct")]
        public ActionResult RegisterProduct(VMproductImage productImage)
        {
            byte[] buffer = Convert.FromBase64String(productImage.image.Content.ToString().Replace("data:image/jpeg;base64,", string.Empty));

            using (soamanaDB db = new soamanaDB())
            {
                productImage.image.ImageType = 1;

                productImage.image.ImageContent = buffer;

                db.Images.Add(productImage.image);
                db.SaveChanges();

                productImage.product.ImageId = productImage.image.ImageId;
                db.Products.Add(productImage.product);
                db.SaveChanges();
            }
            return Ok("OK");
        }

        [HttpDelete("DeleteProduct")]
        public ActionResult DeleteProduct(int ProductId)
        {
            using (var db = new soamanaDB())
            {
                Product product = new Product();
                product = db.Products.FirstOrDefault(x => x.ProductId == ProductId);
                db.Products.Remove(product);
                db.SaveChanges();
            }
            return Ok("OK");
        }

        [HttpPut("UpdateProduct")]
        public ActionResult UpdateProduct(VMproductImage productImage)
        {
            using (soamanaDB db = new soamanaDB())
            {
                if (productImage.image.Content != null)
                {
                    byte[] buffer = Convert.FromBase64String(productImage.image.Content.ToString().Replace("data:image/jpeg;base64,", string.Empty));
                    productImage.image.ImageType = 1;

                    productImage.image.ImageContent = buffer;

                    db.Images.Add(productImage.image);
                    db.SaveChanges();
                    productImage.product.ImageId = productImage.image.ImageId;
                }
                Product product = new Product();

                product = db.Products.First(x => x.ProductId == productImage.product.ProductId);
                product.Price = productImage.product.Price;
                product.Quantity = productImage.product.Quantity;
                product.ProductNameEN = productImage.product.ProductName;
                product.DescriptionEN = productImage.product.Description;
                product.CountryId = productImage.product.CountryId;
                product.ProductCategoryId = productImage.product.ProductCategoryId;

                if (productImage.image.Content != null)
                    product.ImageId = productImage.product.ImageId;
                db.SaveChanges();
            }
            return Ok("OK");
        }
    }
}