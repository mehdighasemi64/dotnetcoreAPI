using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using dotnetcoreAPI.Models;

namespace dotnetcoreAPI.Controllers
{
    [ApiController]
    [Route("api/[controller]")] // https://localhost:5001/api/Country/AllCountry
    public class CountryController : ControllerBase
    {
        [HttpGet("AllCountry")]
        public List<Country> AllCountry()
        {
            var db = new soamanaDB();
            var countries = db.Countries.ToList();
            return (countries);
        }
    }
}
