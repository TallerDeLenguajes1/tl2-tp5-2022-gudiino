using System;
using MvcCadeteria.Models;

namespace MvcCadeteria.Repositorio
{
    public interface MiRepositorioCliente
    {
        public List<Cliente> getClientes();
        public Cliente getClienteId(int id);
        public Cliente getClienteNomDir(string nom,string dir);
        public bool altaCliente(Cliente cdt);
        public bool updateCliente(Cliente cdt);
        public bool deleteCliente(int id_cdt);
    }
}