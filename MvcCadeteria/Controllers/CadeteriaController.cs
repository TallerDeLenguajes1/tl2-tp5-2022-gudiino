using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MvcCadeteria.Models;
using MvcCadeteria.Repositorio;
using MvcCadeteria.ViewsModels;

namespace MvcCadeteria.Controllers
{
    [Authorize(Roles = "Administrador")]
    public class CadeteriaController : Controller
    {
        private readonly ILogger<CadeteriaController> _logger;
        private readonly IMapper _mapeo;
        private readonly MiRepositorioCadeteria _repoCadeteria;
        private readonly MiRepositorioCadete _repoCadete;

        public CadeteriaController(ILogger<CadeteriaController> logger, IMapper mapeo, MiRepositorioCadeteria repoCadeteria,MiRepositorioCadete repoCadete)
        {
            _logger = logger;
            _mapeo = mapeo;
            _repoCadeteria = repoCadeteria;
            _repoCadete = repoCadete;
        }

        public IActionResult Index()
        {
            Cadeteria dato_suc= _repoCadeteria.GetDatosCadeteria();
            CdtriaViewModel vmCdtria = _mapeo.Map<CdtriaViewModel>(dato_suc);
            List<CdtViewModel> listaCdt = _mapeo.Map<List<CdtViewModel>>(_repoCadete.GetCdtsCantPd2s());
            ViewBag.datoCdtria=vmCdtria;
            return View(listaCdt);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}