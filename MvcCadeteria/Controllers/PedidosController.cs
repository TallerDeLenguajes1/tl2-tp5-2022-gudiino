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
            List<Pd2ViewModel> listaPd2 = _mappeo.Map<List<Pd2ViewModel>>(_repoPedido.GetAllPd2CliCdt());
            return View(listaPd2);
        }
        
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++GET: Pedidos/Detalles
        [Authorize(Roles = "Administrador, Cadete")]
        public IActionResult Detalle(int id)
        {
            if(_repoPedido.GetPedidoId(id) == null) return NotFound();
            Pd2ViewModel? pd2 = _mappeo.Map<Pd2ViewModel>(_repoPedido.GetPedidoId(id));
            if (pd2 == null) return NotFound();
            return View(pd2);
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
                pedido.estado_pedido=0;//en curso
                Pedido? newPd2= _mappeo.Map<Pedido>(pedido);
                Cliente verif_cli = _repoCliente.getClienteNomDir(pedido.nombre_cliente!,pedido.Direccion!);
                if(verif_cli==null){
                    verif_cli=new Cliente{cli_nombre=pedido.nombre_cliente!,cli_domicilio=pedido.Direccion!,cli_telefono=pedido.Telefono!,cli_detalle_direccion=pedido.detalle_direccion!};
                    _repoCliente.altaCliente(verif_cli);
                    verif_cli = _repoCliente.getClienteNomDir(pedido.nombre_cliente!,pedido.Direccion!);
                }
                newPd2.id_cli=verif_cli.cli_id;
                if(_repoPedido.altaPedido(newPd2!)){
                    Pedido newPd2_1 = _repoPedido.getPedidoObsCli(newPd2.detalle_pedido,newPd2.id_cli);
                    //int id=Convert.ToInt32(newPd2_1.id_pd2);
                    return RedirectToAction("AsignarCdt", new {id=newPd2_1.id_pd2});//crear un nuevo objeto al pasar a otro controlador
                    //return AsignarCdt(newPd2_1.id_pd2);
                } 
            }
        return View(pedido);
        }
        //******************************************************************* ASIGNAR CADETE A PEDIDO
        public IActionResult AsignarCdt(int id)
        {
            Pedido nuevo = _repoPedido.GetPedidoId(id);
            Pd2ViewModel pedido = _mappeo.Map<Pd2ViewModel>(nuevo);
            ViewBag.estado="Pedido Ingresado: Seleccionar Cadete";
            return View(pedido);
        }
        //******************************************************************* buscar CADETE 
        public IActionResult BuscarCdt(int id_pd2, string nombre, int id_cdt)
        {
            Pedido nuevo = _repoPedido.GetPedidoId(id_pd2);
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
            ViewBag.estado="Pedido Ingresado: Buscar Cadete";
            return View(pedido);
        }
        //******************************************************************* buscar CADETE 
        public IActionResult CargarCdt(int id_pd2, string id_cdt)
        {
            Pedido nuevo = _repoPedido.getPedidoId(id_pd2);
            int id=Convert.ToInt32(id_cdt);
            //bool cambioID=int.TryParse(valorID,out id);
            //int id =Convert.ToInt32(Console.ReadLine());
            Cadete cadete = _repoCadete.getCadeteId(id);
            CdtViewModel cdt_elegido = _mappeo.Map<CdtViewModel>(cadete);
            nuevo.id_cdt=cadete.cdt_id;
            if (_repoPedido.updatePedido(nuevo))
            {
                nuevo.estado_pedido=1;
                if (_repoPedido.updatePedido(nuevo))
                {
                    Pd2ViewModel pedido = _mappeo.Map<Pd2ViewModel>(nuevo);
                    ViewBag.cdtElegido=cdt_elegido;
                    ViewBag.estado="Pedido Completo";
                    return View(pedido);
                }
            }
            ViewBag.estado="Reintento de carga de Cadete";
            return RedirectToAction("AsignarCdt", new {id=id_pd2});
            
        }
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++GET: Pedidos/Editar
        public IActionResult Edit(int id)
        {
            EditarPd2ViewModel pd2 = _mappeo.Map<EditarPd2ViewModel>(_repoPedido.GetPedidoId(id));
            if (pd2 == null)return NotFound();
            return View(pd2);
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++POST: Pedidos/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult Edit(int id_pd2, EditarPd2ViewModel pedido)
        {
            if (ModelState.IsValid)
            {
                Pedido? newPd2= _mappeo.Map<Pedido>(pedido);
                if(_repoCliente.updateCliente(newPd2.cli_pd2)){
                    if(_repoPedido.updatePedido(newPd2)){
                        ViewBag.mensaje="Datos del pedido actualizado exitosamente.";
                        return RedirectToAction(nameof(Index));
                    }
                }
            }
            return View(pedido);
        }

        // *******************************************************************GET: Pedidos/eliminar
        public IActionResult Delete(int id)
        {
            EditarPd2ViewModel? pd2=null;
            if (id == 0 || _repoPedido.GetPedidoId(id) == null)return NotFound();
            pd2 = _mappeo.Map<EditarPd2ViewModel>(_repoPedido.GetPedidoId(id));
            if (pd2 == null)return NotFound();
            return View(pd2);
        }

        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++POST: Pedido/Eliminar
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteConfirmed(int id)
        {
            Pedido? eliminar=_repoPedido.GetPedidoId(id);
            if(eliminar==null){
                return Problem("El Pedido ya fue eliminado o falla en la conexion.");
            }
            if (_repoPedido.deletePedido(eliminar.id_pd2))
            {
                return RedirectToAction(nameof(Index));
            }else{
                return View(id);
            }
        }
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ pedidos por cliente
        public IActionResult Pd2XCdt(int id_cdt, string nombre)
        {
            Pedido nuevo = _repoPedido.GetPedidoId(id_cdt);
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
            ViewBag.estado="Pedido Ingresado: Buscar Cadete";
            return View(pedido);
        }

    }
}
