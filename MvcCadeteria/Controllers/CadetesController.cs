using System;
using Microsoft.AspNetCore.Mvc;
using MvcCadeteria.Models;
using MvcCadeteria.Repositorio;
using AutoMapper;
using Microsoft.AspNetCore.Session;//paquete 1  para usar sesiones
using Microsoft.AspNetCore.Http;// paquete 2 para usar sesiones
using Microsoft.AspNetCore.Authorization;
using MvcCadeteria.ViewsModels;

namespace MvcCadeteria.Controllers;

//[Authorize(Roles = "Administrador")]
[Authorize(Policy = "ADMIN")]
public class CadetesController : Controller
{
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
        List<CdtViewModel> listaCdt = _mappeo.Map<List<CdtViewModel>>(_repoCadete.getCadetes());
        return listaCdt != null ? 
                        View(listaCdt) :
                        Problem("La lista de cadete es null.");
    }
    // ************************************************************ GET: Informacion Cadete
    public IActionResult Info(int id)
    {
        if(_repoCadete.getCadeteId(id) == null) return NotFound();
        CdtViewModel? cdt = _mappeo.Map<CdtViewModel>(_repoCadete.getCadeteId(id));
        if (cdt == null) return NotFound();
        return View(cdt);
    }
    //**************************************************************  GET: Crear Cadete
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
            Cadete nuevoCdt = _mappeo.Map<Cadete>(newCdt);
            if(_repoCadete.altaCadete(nuevoCdt)){
                return RedirectToAction(nameof(Index));
            } 
        }
        return View(newCdt);
    }
    //***************************************************************** GET: Editar Cadete
    public IActionResult Editar(int id)
    {
        EditarCdtViewModel cdt = _mappeo.Map<EditarCdtViewModel>(_repoCadete.getCadeteId(id));
        if (cdt == null)return NotFound();
        return View(cdt);
    }
    // ***************************************************************** POST: Editar Cadete
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(int id, EditarCdtViewModel cadete)
    {
        if(ModelState.IsValid){
            Cadete cdt = _mappeo.Map<Cadete>(cadete);
            if ( _repoCadete.updateCadete(cdt)){
               
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
        EditarCdtViewModel? cadete=null;
        if (id == null || _repoCadete.getCadetes() == null)return NotFound();
        var buscaCadete =   from cdt in _repoCadete.getCadetes()
                            where cdt.cdt_id==id
                            select cdt;
        if ((buscaCadete.Count())!=0){
            foreach (var item in buscaCadete)
            {
                if(item.cdt_id==id){
                    cadete=_mappeo.Map<EditarCdtViewModel>(item);
                }
            }
        }else{
            return NotFound();
        }
        return View(cadete);
    }
    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ POST: Eliminar Cadete
    [HttpPost, ActionName("Delete")]
    public IActionResult DeleteConfirmed(int id)
    {
        Cadete? eliminar_cdt=null;
        if (_repoCadete.getCadetes() == null)
        {
            return Problem("La lista de cadetes es nula.");
        }
        var buscaCadete =   from cdt in _repoCadete.getCadetes()
                            where cdt.cdt_id==id
                            select cdt;
        if ((buscaCadete.Count())!=0){
            foreach (var item in buscaCadete)
            {
                if (item.cdt_id==id)
                {
                    eliminar_cdt=item;
                }
            }
            if(_repoCadete.deleteCadete(eliminar_cdt!.cdt_id)){
                return RedirectToAction(nameof(Index));
            }else{
                return View(_mappeo.Map<EditarCdtViewModel>(eliminar_cdt));
            }
        }else{
            return NotFound();
        }
    }
}
