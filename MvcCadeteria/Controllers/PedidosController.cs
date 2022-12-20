using System;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using MvcCadeteria.Models;
using MvcCadeteria.Repositorio;
using MvcCadeteria.ViewsModels;

namespace MvcCadeteria.Controllers
{
    [Authorize]
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
        [Authorize(Roles = "Administrador")]
        public IActionResult AsignarCdt(int id)
        {
            Pedido nuevo = _repoPedido.GetPedidoId(id);
            Pd2ViewModel pedido = _mappeo.Map<Pd2ViewModel>(nuevo);
            ViewBag.estado="Pedido Ingresado: Seleccionar Cadete";
            return View(pedido);
        }
        //******************************************************************* buscar CADETE 
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
        public IActionResult Edit(int id)
        {
            EditarPd2ViewModel pd2 = _mappeo.Map<EditarPd2ViewModel>(_repoPedido.GetPedidoId(id));
            if (pd2 == null)return NotFound();
            return View(pd2);
        }

        // ++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++POST: Pedidos/Editar
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
        [Authorize(Roles = "Administrador")]
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
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ pedidos por cadetes
        [Authorize(Roles = "Administrador, Cadete")]
        public IActionResult BuscarPd2XCdt()
        {
            List<Pd2ViewModel> listaPd2Cdt = _mappeo.Map<List<Pd2ViewModel>>(_repoPedido.GetAllPd2CliCdt());
            return View(listaPd2Cdt);
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ busqueda de pedidos por cadete
        [Authorize(Roles = "Administrador, Cadete")]
        public IActionResult BuscarCdtPd2(string? nombre, int id_cdt)
        {
            List<Pd2ViewModel> listaCdtPd2 = _mappeo.Map<List<Pd2ViewModel>>(_repoPedido.GetAllPd2CliCdt());
            var listaPd2 = from m in listaCdtPd2
                            select m;
            if (!String.IsNullOrEmpty(nombre))
            {
                listaPd2 = from m in listaCdtPd2
                            where m.cdt_nom!= null && m.cdt_nom.Contains(nombre) 
                            select m;
                //listaPd2 = listaPd2.Where(s => s.cdt_nom.Contains(nombre.ToString()));
            }
            if (id_cdt!=0)
            {
                listaPd2 = listaPd2.Where(s => s.id_cdt.ToString().Contains(id_cdt.ToString()));
            }
            if(listaPd2.Count()==0)ViewBag.msjBuscCero= "No hay coincidencia, intente con nuevos valores.";
            ViewBag.lista=listaPd2;
            return View();
        }
        // +++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ PEDIDOS POR CLIENTES
        [Authorize(Roles = "Administrador")]
        public IActionResult BuscarPd2XCli()
        {
            List<Pd2ViewModel> listaPd2Cli = _mappeo.Map<List<Pd2ViewModel>>(_repoPedido.GetAllPd2CliCdt());
            List<Pd2ViewModel> listaPd2Cli2= listaPd2Cli.OrderBy(item => item.id_cli).ToList();
            return View(listaPd2Cli2);
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ BUSQUEDA DE PEDIDOS POR CLIENTE
        [Authorize(Roles = "Administrador")]
        public IActionResult BuscarCliPd2(string? nombre, int id_cli)
        {
            List<Pd2ViewModel> listaCliPd2 = _mappeo.Map<List<Pd2ViewModel>>(_repoPedido.GetAllPd2CliCdt());
            var listaPd2Cli2= listaCliPd2.OrderBy(item => item.id_cli).ToList();
            var listaCli = from m in listaPd2Cli2
                            select m;
            if (!String.IsNullOrEmpty(nombre))
            {
                listaCli = from m in listaPd2Cli2
                            where m.cli_nom.Contains(nombre) 
                            select m;
                //listaPd2 = listaPd2.Where(s => s.cdt_nom.Contains(nombre.ToString()));
            }
            if (id_cli!=0)
            {
                listaCli = listaCli.Where(s => s.id_cdt.ToString().Contains(id_cli.ToString()));
            }
            if(listaCli.Count()==0)ViewBag.msjBuscCero= "No hay coincidencia, intente con nuevos valores.";
            ViewBag.listaCli=listaCli;
            return View();
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ CAMBIO ESTADO PEDIDO
        [Authorize(Roles = "Administrador, Cadete")]
        public IActionResult CbioEstadoPd2()
        {
            List<Pd2ViewModel> listaPd2Cli = _mappeo.Map<List<Pd2ViewModel>>(_repoPedido.GetAllPd2CliCdt());
            return View(listaPd2Cli);
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ CAMBIO ESTADO PEDIDO BUSCADO
        [Authorize(Roles = "Administrador, Cadete")]
        public IActionResult CbioEstadoPd2Busca2(string nomCli, int id_cli, string nomCdt, int id_cdt)
        {
            bool bandera=true;
            List<Pd2ViewModel> listaPd2CliCdt = _mappeo.Map<List<Pd2ViewModel>>(_repoPedido.GetAllPd2CliCdt());
            var listaBusca = from m in listaPd2CliCdt
                            select m;
            if (!String.IsNullOrEmpty(nomCli)||!String.IsNullOrEmpty(nomCdt))
            {
                bandera=false;
                if(!String.IsNullOrEmpty(nomCli)){
                    listaBusca = listaBusca.Where(s => s.cli_nom!=null && s.cli_nom.Contains(nomCli));
                }else{
                    listaBusca = listaBusca.Where(s => s.cdt_nom!=null && s.cdt_nom.Contains(nomCdt));
                }
            }
            if (bandera && (id_cli!=0 || id_cdt!=0))
            {
                if (id_cli!=0)
                {
                    listaBusca = listaBusca.Where(s => s.id_cli.ToString().Contains(id_cli.ToString()));
                }else{
                    listaBusca = listaBusca.Where(s => s.id_cdt.ToString().Contains(id_cdt.ToString()));
                }
            }
            if(listaBusca.Count()==0)ViewBag.msjBuscCero= "No hay coincidencia, intente con nuevos valores.";
            ViewBag.listaPd2Busca2=listaBusca;
            return View();
        }
        //++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++++ SELECCION ESTADO PEDIDO
        [Authorize(Roles = "Administrador, Cadete")]
        public IActionResult CbioEsta2Pd2Selec(int id)
        {
            Pedido? pd2=_repoPedido.GetPedidoId(id);
            EditarPd2ViewModel pd2Vm = _mappeo.Map<EditarPd2ViewModel>(pd2);
            //if (pd2Vm == null)return NotFound();
            return View(pd2Vm);
        }
        //+++++++++++++++++++++++++++++++++++++++++++++++
        [Authorize(Roles = "Administrador, Cadete")]
        public IActionResult CbioEsta2Pd2SelecConfirm(int id_pd2, string estado)
        {
            int estado2=Convert.ToInt32(estado);
            if (_repoPedido.updatePedidoEsta2(id_pd2,estado2))
            {
                return RedirectToAction("Index");
            }
            return RedirectToAction("CbioEsta2Pd2Selec", new {id=id_pd2});
        }
    }
}
