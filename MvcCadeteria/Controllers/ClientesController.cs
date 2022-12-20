using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace MvcCadeteria.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class ClientesController : Controller
    {
        private readonly ILogger<ClientesController> _logger;

        public ClientesController(ILogger<ClientesController> logger)
        {
            _logger = logger;
        }

        public IActionResult Index()
        {
            // if(!HttpContext.Request.Cookies.ContainsKey("first_request"))
            // {
            //     HttpContext.Response.Cookies.Append("first_request", DateTime.Now.ToString());
            //     return Content("Welcome, new visitor!");
            // }
            // else
            // {
            //     DateTime firstRequest = DateTime.Parse(HttpContext.Request.Cookies["first_request"]);
            //     return Content("Welcome back, user! You first visited us on: " + firstRequest.ToString());
            // }
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}