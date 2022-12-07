using System;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MvcCadeteria.Models;
using MvcCadeteria.Repositorio;
using MvcCadeteria.ViewsModels;
//using System.Net.Security;

namespace MvcCadeteria.Controllers;

public class UsuariosController : Controller
{
    private readonly ILogger<UsuariosController> _logger;
    private readonly MiRepositorioUsuario _repoUsuario;
    // public const string SessionKeyName = "_nom";
    // public const string SessionKeyUser = "_user";
    // public const string SessionKeyRol = "_rol";
    public UsuariosController(ILogger<UsuariosController> logger, MiRepositorioUsuario repoUsuario)
    {
        _logger = logger;
        _repoUsuario = repoUsuario;
    }
    //****************************************************************inicio la vista de inicio de sesion
    public IActionResult Index()
    {
        InUsuario IngresoU = new InUsuario();
        return View(IngresoU);
    }
    //******************************************************************** logueo del usuario
    [HttpPost]
    [ValidateAntiForgeryToken]
    //public IActionResult Login(InUsuario in_u)//recepcion de los datos de logueo
    public async Task<IActionResult> Login(InUsuario in_u)
    {
        if (ModelState.IsValid)
        {
            try
            {
                Usuario usuario = _repoUsuario.getUsuario(in_u.nom_u!,in_u.pass_u!);
                if (usuario == null)
                {
                    ViewBag.msg = "No tienes credenciales correctas";
                    return View(in_u);
                }
                else
                {
                    //DEBEMOS CREAR UNA IDENTIDAD (name y role)
                    //Y UN PRINCIPAL
                    //DICHA IDENTIDAD DEBEMOS COMBINARLA CON LA COOKIE DE 
                    //AUTENTIFICACION
                    ClaimsIdentity identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme, ClaimTypes.Name, ClaimTypes.Role);
                    //TODO USUARIO PUEDE CONTENER UNA SERIE DE CARACTERISTICAS
                    //LLAMADA CLAIMS.  DICHAS CARACTERISTICAS PODEMOS ALMACENARLAS
                    //DENTRO DE USER PARA UTILIZARLAS A LO LARGO DE LA APP
                    Claim claimUserName = new Claim(ClaimTypes.Name, usuario.nom_u);
                    Claim claimRole = new Claim(ClaimTypes.Role, usuario.rol_u);
                    Claim claimIdUsuario = new Claim("IdUsuario", usuario.id_u.ToString());
                    Claim claimLog = new Claim("LogUsuario", usuario.log_u);

                    identity.AddClaim(claimUserName);
                    identity.AddClaim(claimRole);
                    identity.AddClaim(claimIdUsuario);
                    identity.AddClaim(claimLog);

                    ClaimsPrincipal userPrincipal = new ClaimsPrincipal(identity);
                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, userPrincipal, new AuthenticationProperties
                    {
                        ExpiresUtc = DateTime.Now.AddMinutes(45)
                    });

                    return RedirectToAction("Index", "Home");
                }
                //HttpContext.User;
                //Session["User"]=nuevo;
                // HttpContext.Session.SetString(SessionKeyName, usuario.nom_u);
                // HttpContext.Session.SetString(SessionKeyUser, usuario.log_u);
                // HttpContext.Session.SetString(SessionKeyRol, usuario.rol_u);
                // return RedirectToAction("Index", "Home");
            }
            catch (Exception ex)
            {
                ViewBag.Error= ex.Message;
                return View(in_u);
            }
        }
        ViewBag.msg = "Modelo no valido";
        return View(in_u);
    }
    //************************************************ deslogueo del usuario
    // public IActionResult Logout()
    // {
    //     HttpContext.Session.Clear();
    //     return RedirectToAction("Index", "Home");
    // }
    public async Task<IActionResult> Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Index", "Usuarios");
    }
    //***********************************************

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View("Error!");
    }
}