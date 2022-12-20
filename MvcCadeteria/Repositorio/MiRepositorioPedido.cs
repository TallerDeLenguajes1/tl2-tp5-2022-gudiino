using System;
using MvcCadeteria.Models;

namespace MvcCadeteria.Repositorio
{
    public interface MiRepositorioPedido
    {
        public List<Pedido> getPedidos();
        public List<Pedido> GetAllPd2CliCdt();
        public Pedido getPedidoId(int id);
        public Pedido GetPedidoId(int id);
        public Pedido getPedidoObsCli(string obs,int id);
        public bool altaPedido(Pedido pd2);
        public bool updatePedido(Pedido pd2);
        public bool updatePedidoEsta2(int id_pd2, int estado);
        public bool deletePedido(int id_pd2);
    }
}