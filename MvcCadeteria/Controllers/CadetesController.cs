using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCadeteria.Models;
using MvcCadeteria.ViewModels;
using MvcCadeteria.Repositorio;
using AutoMapper;
using Microsoft.AspNetCore.Session;//paquete 1  para usar sesiones
using Microsoft.AspNetCore.Http;// paquete 2 para usar sesiones
using Microsoft.AspNetCore.Authorization;
namespace MvcCadeteria.Controllers;

//[Authorize]
[Authorize(Roles = "Administrador")]
public class CadetesController : Controller
{
    //public const string SessionKeyName = "_Name";
    private readonly IMapper _mappeo;
    private readonly MiRepositorioCadete _repoCadete;

    public CadetesController(IMapper mappeo, MiRepositorioCadete repoCadete)
    {
        _mappeo = mappeo;
        _repoCadete = repoCadete;
    }
    
    //+++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ GET: Listado Cadetes
    public IActionResult Index()
    {
        List<CdtViewModel> nuevo = _mappeo.Map<List<CdtViewModel>>(_repoCadete.getCadetes());
        return nuevo != null ? 
                        View(nuevo) :
                        Problem("La lista de cadete es null.");
    }
    // ************************************************************ GET: Detalles Cadete
    public IActionResult Details(int id)
    {
        if(_repoCadete.getCadete(id) == null) return NotFound();
        CdtViewModel? nuevo = _mappeo.Map<CdtViewModel>(_repoCadete.getCadete(id));
        if (nuevo == null) return NotFound();
        return View(nuevo);
    }
    //**************************************************************  GET: Crear Cadete
    //[Authorize(Roles = "Administrador")]
    public IActionResult Create()
    {
        return View(new AltaCdtViewModel());
    }
    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ POST: Crear Cadete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(AltaCdtViewModel newCdt)
    {
        if (ModelState.IsValid)
        {
            if(_repoCadete.altaCadete(newCdt)){
                return RedirectToAction(nameof(Index));
            } 
        }
        return View();
    }
    //***************************************************************** GET: Editar Cadete
    public IActionResult Editar(int id)
    {
        Cadete nuevo= _repoCadete.getCadete(id);
        if (nuevo == null)
        {
            return NotFound();
        }
        EditarCdtViewModel? cadete = _mappeo.Map<EditarCdtViewModel>(nuevo);
        if (cadete == null)
        {
            return NotFound();
        }
        return View(cadete);
    }
    // ***************************************************************** POST: Editar Cadete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(int id, EditarCdtViewModel cadete)
    {
        if (id != cadete.id)
        {
            return NotFound();
        }
        if(ModelState.IsValid){
            //Cadete cdtUpdate = _mappeo.Map<Cadete>(cadete);
            if ( _repoCadete.updateCadete(cadete)){
               
                return RedirectToAction(nameof(Index));
            }else{
                return NotFound();
            }
        }
        return View(cadete);
    }
    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ GET: Eliminar Cadete
    public IActionResult Delete(int? id)
    {
        Cadete? cadete=null;
        if (id == null || _repoCadete.getCadetes() == null)
        {
            return NotFound();
        }
        var buscaCadete =   from cdt in _repoCadete.getCadetes()
                            where cdt.getId()==id
                            select cdt;
        if ((buscaCadete.Count())!=0){
            foreach (var item in buscaCadete)
            {
                if(item.getId()==id)cadete=item;
            }
        }else{
            return NotFound();
        }
        return View(cadete);
    }

    // POST: Cadetes/Delete/5
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        Cadete? eliminar=null;
        if (_repoCadete.getCadetes() == null)
        {
            return Problem("Entity set 'MvcCadeteriaContext.Cadete'  is null.");
        }
        var buscaCadete =   from cdt in _repoCadete.getCadetes()
                            where cdt.getId()==id
                            select cdt;
        if ((buscaCadete.Count())!=0){
            foreach (var item in buscaCadete)
            {
                if (item.getId()==id)
                {
                    eliminar=item;
                }
            }
            if(_repoCadete.getCadetes().Remove(eliminar!)){
                //string archivo = "listaCadetes.csv";
                //HelperDeArchivos.GuardarCSV(archivo,_repoCadete.getCadetes(),_repoCadete.getCadeteria());
                Console.WriteLine(" eliminaDO");
            }else{
                Console.WriteLine("fallo eliminacion");
            }
            return RedirectToAction(nameof(Index));
        }else{
            return NotFound();
        }
        
    }
}
