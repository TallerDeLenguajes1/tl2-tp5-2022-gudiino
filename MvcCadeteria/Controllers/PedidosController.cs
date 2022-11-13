using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using MvcCadeteria.Models;
using MvcCadeteria.ViewModels;

namespace MvcCadeteria.Controllers
{
    public class PedidosController : Controller
    {
        //private List<Pedido> listaPedidos;
        private Cadeteria inicioDatos = new Cadeteria();
        // public PedidosController()
        // {
        //     listaPedidos = new List<Pedido>();
        // }

        // GET: Pedidos
        public IActionResult Index()
        {
            return View(inicioDatos.getPedidos());
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++GET: Pedidos/Detalles
        public IActionResult Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pedido? pedido = null;
            var buscaPedido =  from pd2 in inicioDatos.getPedidos()
                            where pd2.id_pedido==id
                            select pd2;
            if ((buscaPedido.Count())!=0){
                foreach (var item in inicioDatos.getPedidos())
                {
                    if(item.id_pedido==id)pedido=item;
                }
            }
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // *****************************************************************GET: nuevo pedido
        public IActionResult Create()
        {
            return View(new AltaPd2ViewModel());
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++POST: alta pedido
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Create(AltaPd2ViewModel pedido)
        {
            if (ModelState.IsValid)
            {
                int mayor;
                if (inicioDatos.getClientes().Count==0)
                {
                    mayor=0;
                }else{
                    mayor = inicioDatos.getClientes().Max(x => x.getId());
                }
                Cliente nuevoCliente= new(mayor+1,pedido.nombre_cliente!,pedido.Direccion!,pedido.Numero,pedido.Telefono!,pedido.detalle_direccion!);
                inicioDatos.getClientes().Add(nuevoCliente);
                int mayor2=0;
                mayor2 = inicioDatos.getPedidos().Max(x => x.id_pedido);
                Pedido nuevoPedido=new Pedido(mayor2+1,pedido.detalle_pedido!,pedido.estado_pedido!,nuevoCliente);
                inicioDatos.getPedidos().Add(nuevoPedido);
                return RedirectToAction(nameof(Index));
            }
            return View(pedido);
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++GET: Pedidos/Editar
        public IActionResult Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pedido? pedido = null;
            var buscaPedido =  from pd2 in inicioDatos.getPedidos()
                            where pd2.id_pedido==id
                            select pd2;
            if ((buscaPedido.Count())!=0){
                foreach (var item in inicioDatos.getPedidos())
                {
                    if(item.id_pedido==id)pedido=item;
                }
            }
            if (pedido == null)
            {
                return NotFound();
            }
            return View(pedido);
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++POST: Pedidos/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id, Pedido pedido)
        {
            if (id != pedido.id_pedido)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                var buscaPedido =   from pd2 in inicioDatos.getPedidos()
                                     where pd2.id_pedido==id
                                     select pd2;
                if ((buscaPedido.Count())!=0){
                    foreach (var item in buscaPedido)
                    {
                        if(item.id_pedido==id){
                            item.cliente_pedido=pedido.cliente_pedido;
                            item.detalle_pedido=pedido.detalle_pedido;
                            item.estado_pedido=pedido.estado_pedido;
                        }
                    }
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(pedido);
        }

        // *******************************************************************GET: Pedidos/eliminar
        public IActionResult Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            Pedido? pedido = null;
            var buscaPedido =  from pd2 in inicioDatos.getPedidos()
                            where pd2.id_pedido==id
                            select pd2;
            if ((buscaPedido.Count())!=0){
                foreach (var item in inicioDatos.getPedidos())
                {
                    if(item.id_pedido==id)pedido=item;
                }
            }
            if (pedido == null)
            {
                return NotFound();
            }

            return View(pedido);
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++POST: Pedido/Eliminar
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Pedido? eliminar=null;
            if(inicioDatos.getPedidos()==null){
                return Problem("Entity set 'MvcCadeteriaContext.Cadete'  is null.");
            }
            var buscaPedido =  from pd2 in inicioDatos.getPedidos()
                            where pd2.id_pedido==id
                            select pd2;
            if ((buscaPedido.Count())!=0){
                foreach (var item in inicioDatos.getPedidos())
                {
                    if(item.id_pedido==id)eliminar=item;
                }
            }
            if (inicioDatos.getPedidos().Remove(eliminar!))
            {
                return RedirectToAction(nameof(Index));
            }else{
                return View(id);
            }
        }

        // private bool PedidoExists(int id)
        // {
        //     return _context.Pedido.Any(e => e.id == id);
        // }
    }
}
