using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using dotnetcoreAPI.Models;
using Newtonsoft.Json.Linq;

namespace dotnetcoreAPI.Controllers
{
    [ApiController]
    
    [Route("api/[controller]")]  // https://localhost:5001/api/Category/AllProductCategory
    public class CategoryController : ControllerBase
    {
        [HttpGet("AllProductCategory")]
        public List<ProductCategory> AllProductCategory()
        {
            var db = new soamanaDB();
            var categories = db.ProductCategories.ToList();
            return (categories);
        }

        [HttpGet("AllCategory")]
        public List<Category> AllCategory()
        {
            var db = new soamanaDB();
            var categories = db.Categories.ToList();
            return (categories);
        }
    }
}
