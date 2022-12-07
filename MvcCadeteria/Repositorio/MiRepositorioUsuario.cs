using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcCadeteria.Models;

namespace MvcCadeteria.Repositorio
{
    public interface MiRepositorioUsuario
    {
        public Usuario getUsuario(string in_u, string pass_u);
    }
}