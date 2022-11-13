using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCadeteria.Models;
using MvcCadeteria.ViewModels;

namespace MvcCadeteria.Controllers;

public class CadetesController : Controller
{
    private Cadeteria inicioDatos = new Cadeteria();

    // GET: Cadetes
    public IActionResult Index()
    {
            return inicioDatos.getCadetes != null ? 
                        View(inicioDatos.getCadetes()) :
                        Problem("La lista de cadete es null.");
    }
    
    // *********************************************************GET: Cadetes/Detalles
    public IActionResult Details(int? id)
    {
        if (id == null || inicioDatos.getCadetes() == null)
        {
            return NotFound();
        }

        Cadete? cadete = null;
        foreach (var item in inicioDatos.getCadetes())
        {
            if (item.getId()==id)
            {
                cadete=item;
            }
        }
        if (cadete == null)
        {
            return NotFound();
        }

        return View(cadete);
    }

    // GET: Cadetes/Create
    public IActionResult Create()
    {
        return View(new AltaCdtViewModel());
    }

    // POST: Cadetes/Crear
    [HttpPost]
    
    public IActionResult Create(AltaCdtViewModel nuevoCadete)
    {//Cadete(int iden, string nom, string dir,int num, string tel)
        int ultimoId=0;
        foreach (var item in inicioDatos.getCadetes())
        {
            if(item.getId()>ultimoId)ultimoId=item.getId();
        }
        int id=ultimoId+1;
        // Cadete cadete = new Cadete(id,nombr,call,numer,telefon);
        // if (cadete!=null)
        // {
        //     string archivo = "listaCadetes.csv";
        //     inicioDatos.getCadetes().Add(cadete);
        //     HelperDeArchivos.GuardarCSV(archivo,inicioDatos.getCadetes(),inicioDatos.getCadeteria());
        //     return RedirectToAction(nameof(Index));
        // }
        if (ModelState.IsValid)
        {
            Cadete cadete = new Cadete(id,nuevoCadete.NombreCadete!,nuevoCadete.Direccion!,nuevoCadete.Numero,nuevoCadete.Telefono!);
            string archivo = "listaCadetes.csv";
            inicioDatos.getCadetes().Add(cadete);
            HelperDeArchivos.GuardarCSV(archivo,inicioDatos.getCadetes(),inicioDatos.getCadeteria());
            return RedirectToAction(nameof(Index));
        }
        return View();
    }

    // GET: Cadetes/Editar **********************************************************************
    public IActionResult Editar(int? id)
    {
        if (id == null || inicioDatos.getCadetes() == null)
        {
            return NotFound();
        }

        EditarCdtViewModel? cadete = new EditarCdtViewModel();
        foreach (var item in inicioDatos.getCadetes())
        {
            if (item.getId()==id)
            {
                cadete!.id=item.getId();
                cadete!.NombreCadete=item.getNom();
                cadete!.Direccion=item.getCalle();
                cadete!.Numero=item.getNumero();
                cadete!.Telefono=item.getTelefono();
            }
        }
        if (cadete == null)
        {
            return NotFound();
        }
        return View(cadete);
    }

    // ***************************************************************POST: Cadetes/Editar/
    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Editar(int id, EditarCdtViewModel cadete)
    {
        if (id != cadete.id)
        {
            return NotFound();
        }

        // if (ModelState.IsValid)
        // {
        //     foreach (var item in inicioDatos.getCadetes())
        //     {
        //         if (item.getId()==id)
        //         {
        //             item.setNom(cadete.NombreCadete!);
        //             item.setCalle(cadete.Direccion!);
        //             item.setNumero(cadete.Numero);
        //             item.setTelefono(cadete.Telefono!);
        //         }
        //     }
        //     return RedirectToAction(nameof(Index));
        // }
        //++++++++++++++++++++++++++++++++++++++++++++
        if(ModelState.IsValid){
            var buscaCadete =   from cdt in inicioDatos.getCadetes()
                            where cdt.getId()==id
                            select cdt;
            if ((buscaCadete.Count())!=0){
                foreach (var item in buscaCadete)
                {
                    if(item.getId()==id){
                        item.setNom(cadete.NombreCadete!);
                        item.setCalle(cadete.Direccion!);
                        item.setNumero(cadete.Numero);
                        item.setTelefono(cadete.Telefono!);
                    }
                }
                return RedirectToAction(nameof(Index));
            }else{
                return NotFound();
            }
        }
        
        //+++++++++++++++++++++++++++++++++
        return View(cadete);
    }

    // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++GET: Cadetes/Delete/5
    public IActionResult Delete(int? id)
    {
        Cadete? cadete=null;
        if (id == null || inicioDatos.getCadetes() == null)
        {
            return NotFound();
        }
        var buscaCadete =   from cdt in inicioDatos.getCadetes()
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
        if (inicioDatos.getCadetes() == null)
        {
            return Problem("Entity set 'MvcCadeteriaContext.Cadete'  is null.");
        }
        var buscaCadete =   from cdt in inicioDatos.getCadetes()
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
            if(inicioDatos.getCadetes().Remove(eliminar!)){
                string archivo = "listaCadetes.csv";
                HelperDeArchivos.GuardarCSV(archivo,inicioDatos.getCadetes(),inicioDatos.getCadeteria());
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
