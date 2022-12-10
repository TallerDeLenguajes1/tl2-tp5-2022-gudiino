using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcCadeteria.Models;
using MvcCadeteria.Repositorio;
using MvcCadeteria.ViewsModels;

namespace MvcCadeteria.Controllers
{
    //[Authorize]
    public class PedidosController : Controller
    {
        private readonly IMapper _mappeo;
        private readonly MiRepositorioPedido _repoPedido;

        public PedidosController(IMapper mappeo, MiRepositorioPedido repoPedido)
        {
            _mappeo = mappeo;
            _repoPedido = repoPedido;
        }
        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++GET: listar pedidos
        // GET: Pedidos
        [Authorize(Roles = "Administrador, Cadete")]
        public IActionResult Index()
        {
            List<Pd2ViewModel> listaPd2 = _mappeo.Map<List<Pd2ViewModel>>(_repoPedido.getPedidos());
            return View(listaPd2);
        }
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++GET: Pedidos/Detalles
        [Authorize(Roles = "Administrador, Cadete")]
        public IActionResult Detalle(int? id)
        {
            if (id == null)return NotFound();
            Pedido? pedido = null;
            var buscaPedido =  from pd2 in _repoPedido.getPedidos()
                            where pd2.id_pd2==id
                            select pd2;
            if ((buscaPedido.Count())!=0){
                foreach (var item in buscaPedido)
                {
                    if(item.id_pd2==id)pedido=item;
                }
            }
            if (pedido == null)return NotFound();
            return View(pedido);
        }

        // *****************************************************************GET: nuevo pedido
        [Authorize(Roles = "Administrador")]
        public IActionResult Create()
        {
            return View(new AltaPd2ViewModel());
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++POST: alta pedido
        [HttpPost]
        [Authorize(Roles = "Administrador")]
        public IActionResult Create(AltaPd2ViewModel pedido)
        {
            if (ModelState.IsValid)
            {
                //Cliente(int iden, string nom, string dir, string tel, string dirREf)
                Pedido? newPd2=null;
                if(_repoPedido.altaPedido(newPd2!)){
                    return RedirectToAction(nameof(Index));
                } 
            }
        return View(pedido);
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++GET: Pedidos/Editar
        // public IActionResult Edit(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     Pedido? pedido = null;
        //     var buscaPedido =  from pd2 in inicioDatos.getPedidos()
        //                     where pd2.id_pedido==id
        //                     select pd2;
        //     if ((buscaPedido.Count())!=0){
        //         foreach (var item in inicioDatos.getPedidos())
        //         {
        //             if(item.id_pedido==id)pedido=item;
        //         }
        //     }
        //     if (pedido == null)
        //     {
        //         return NotFound();
        //     }
        //     return View(pedido);
        // }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++POST: Pedidos/Editar
        // [HttpPost]
        // [ValidateAntiForgeryToken]
        // public IActionResult Edit(int id, Pedido pedido)
        // {
        //     if (id != pedido.id_pedido)
        //     {
        //         return NotFound();
        //     }

        //     if (ModelState.IsValid)
        //     {
        //         var buscaPedido =   from pd2 in inicioDatos.getPedidos()
        //                              where pd2.id_pedido==id
        //                              select pd2;
        //         if ((buscaPedido.Count())!=0){
        //             foreach (var item in buscaPedido)
        //             {
        //                 if(item.id_pedido==id){
        //                     item.cliente_pedido=pedido.cliente_pedido;
        //                     item.detalle_pedido=pedido.detalle_pedido;
        //                     item.estado_pedido=pedido.estado_pedido;
        //                 }
        //             }
        //             return RedirectToAction(nameof(Index));
        //         }
        //     }
        //     return View(pedido);
        // }

        // *******************************************************************GET: Pedidos/eliminar
        // public IActionResult Delete(int? id)
        // {
        //     if (id == null)
        //     {
        //         return NotFound();
        //     }

        //     Pedido? pedido = null;
        //     var buscaPedido =  from pd2 in inicioDatos.getPedidos()
        //                     where pd2.id_pedido==id
        //                     select pd2;
        //     if ((buscaPedido.Count())!=0){
        //         foreach (var item in inicioDatos.getPedidos())
        //         {
        //             if(item.id_pedido==id)pedido=item;
        //         }
        //     }
        //     if (pedido == null)
        //     {
        //         return NotFound();
        //     }

        //     return View(pedido);
        // }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++POST: Pedido/Eliminar
        // [HttpPost, ActionName("Delete")]
        // [ValidateAntiForgeryToken]
        // public IActionResult DeleteConfirmed(int id)
        // {
        //     Pedido? eliminar=null;
        //     if(inicioDatos.getPedidos()==null){
        //         return Problem("Entity set 'MvcCadeteriaContext.Cadete'  is null.");
        //     }
        //     var buscaPedido =  from pd2 in inicioDatos.getPedidos()
        //                     where pd2.id_pedido==id
        //                     select pd2;
        //     if ((buscaPedido.Count())!=0){
        //         foreach (var item in inicioDatos.getPedidos())
        //         {
        //             if(item.id_pedido==id)eliminar=item;
        //         }
        //     }
        //     if (inicioDatos.getPedidos().Remove(eliminar!))
        //     {
        //         return RedirectToAction(nameof(Index));
        //     }else{
        //         return View(id);
        //     }
        // }

        // private bool PedidoExists(int id)
        // {
        //     return _context.Pedido.Any(e => e.id == id);
        // }
    }
}
