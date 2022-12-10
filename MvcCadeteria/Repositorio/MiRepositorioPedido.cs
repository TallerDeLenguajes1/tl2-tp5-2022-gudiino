using System;
using MvcCadeteria.Models;

namespace MvcCadeteria.Repositorio
{
    public interface MiRepositorioPedido
    {
        public List<Pedido> getPedidos();
        public Pedido getPedido(int id);
        public bool altaPedido(Pedido pd2);
        public bool updatePedido(Pedido pd2);
        public bool deletePedido(int id_pd2);
        public int ultimoIdPedido();
    }
}