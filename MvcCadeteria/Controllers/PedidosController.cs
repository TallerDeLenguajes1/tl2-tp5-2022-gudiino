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
        private readonly MiRepositorioCliente _repoCliente;
        private readonly MiRepositorioCadete _repoCadete;

        public PedidosController(IMapper mappeo, MiRepositorioPedido repoPedido, MiRepositorioCliente repoCliente,MiRepositorioCadete repoCadete)
        {
            _mappeo = mappeo;
            _repoPedido = repoPedido;
            _repoCliente = repoCliente;
            _repoCadete = repoCadete;
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
                Pedido? newPd2= _mappeo.Map<Pedido>(pedido);
                Cliente verif_cli = _repoCliente.getClienteNomDir(pedido.nombre_cliente!,pedido.Direccion!);
                if(verif_cli==null){
                    verif_cli=new Cliente{cli_nombre=pedido.nombre_cliente!,cli_domicilio=pedido.Direccion!,cli_telefono=pedido.Telefono!,cli_detalle_direccion=pedido.detalle_direccion!};
                    _repoCliente.altaCliente(verif_cli);
                    verif_cli = _repoCliente.getClienteNomDir(pedido.nombre_cliente!,pedido.Direccion!);
                }
                newPd2.id_cli=verif_cli.cli_id;
                if(_repoPedido.altaPedido(newPd2!)){
                    return RedirectToAction("AsignarCdt",newPd2.id_pd2);
                    //return AsignarCdt(newPd2);
                } 
            }
        return View(pedido);
        }
        //******************************************************************* ASIGNAR CADETE A PEDIDO
        [HttpGet]
        public IActionResult AsignarCdt(int id)
        {
            Pedido nuevo = _repoPedido.getPedido(id);
            Pd2ViewModel pedido = _mappeo.Map<Pd2ViewModel>(nuevo);
            ViewBag.estado="Pedido Ingresado: Seleccionar Cadete";
            return View(pedido);
        }
        //******************************************************************* buscar CADETE 
        public IActionResult BuscarCdt(int id_pd2, string nombre, int id_cdt)
        {
            Pedido nuevo = _repoPedido.getPedido(id_pd2);
            Pd2ViewModel pedido = _mappeo.Map<Pd2ViewModel>(nuevo);
            List<CdtViewModel> listaCdt = _mappeo.Map<List<CdtViewModel>>(_repoCadete.getCadetes());
            var listaCdts = from m in listaCdt
                            select m;
            if (!String.IsNullOrEmpty(nombre))
            {
                listaCdts = listaCdts.Where(s => s.nombre!.Contains(nombre));
            }
            if (id_cdt!=0)
            {
                listaCdts = listaCdts.Where(s => s.id!.Contains(id_cdt.ToString()));
            }
            ViewBag.listCdt=listaCdts;
            ViewBag.estado="Pedido Ingresado: Seleccionar Cadete";
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
