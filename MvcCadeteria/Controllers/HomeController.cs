using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using MvcCadeteria.Models;
using Microsoft.AspNetCore.Session;//paquete 1  para usar sesiones
using Microsoft.AspNetCore.Http;// paquete 2 para usar sesiones

namespace MvcCadeteria.Controllers;

public class HomeController : Controller
{
    //HttpContext.Session.SetString(Nombre,"Jorge"); 
    //string Nombre = HttpContext.Session.GetString(Nombre); 
    public const string SessionKeyName = "_Name";

    private readonly ILogger<HomeController> _logger;
    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        HttpContext.Session.SetString(SessionKeyName, "JORGE");
        ViewBag.nombre = HttpContext.Session.GetString(SessionKeyName);
        return View();
    }

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
