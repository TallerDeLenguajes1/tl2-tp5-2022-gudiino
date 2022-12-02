using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Session;
namespace MvcCadeteria.Models
{
    public class sesiones : PageModel
    {
        public const string SessionKeyName = "_Name";
        public const string SessionKeyAge = "_Age";

        private readonly ILogger<sesiones> _logger;

        public sesiones(ILogger<sesiones> logger)
        {
            _logger = logger;
        }

        public void OnGet()
        {
            if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
            {
                HttpContext.Session.SetString(SessionKeyName, "Jorge");
                HttpContext.Session.SetInt32(SessionKeyAge, 73);
            }
            var name = HttpContext.Session.GetString(SessionKeyName);
            var age = HttpContext.Session.GetInt32(SessionKeyAge).ToString();

            _logger.LogInformation("Session Name: {Name}", name);
            _logger.LogInformation("Session Age: {Age}", age);
        }
    }
}