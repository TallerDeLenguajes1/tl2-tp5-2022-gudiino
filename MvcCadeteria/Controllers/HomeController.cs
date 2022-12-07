using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcCadeteria.Models;
using Microsoft.AspNetCore.Session;//paquete 1  para usar sesiones
using Microsoft.AspNetCore.Http;// paquete 2 para usar sesiones
using MvcCadeteria.Filters;

namespace MvcCadeteria.Controllers;

public class HomeController : Controller
{
    //public const string SessionKeyName = "_Name";
    //public var nuevo = HttpContext.Session.LoadAsync

    private readonly ILogger<HomeController> _logger;
    
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        //ViewBag.nombre = HttpContext.Session.GetString(SessionKeyName);
        //if(HttpContext.Session["Log_u"] == null)return RedirectToAction("Login", "Home");
        return View();
    }
    [AuthorizeUsers(Policy = "ADMINISTRADORES")]
    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
