using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MvcCadeteria.Models;
using MvcCadeteria.ViewModels;

namespace MvcCadeteria.Repositorio
{
    public interface MiRepositorioCadete
    {
        public List<Cadete> getCadetes();
        public Cadete getCadete(int id);
        public bool updateCadete(EditarCdtViewModel cdt);
        public bool altaCadete(AltaCdtViewModel cdt);
        public int altaPersona(string nom, string calle, int num, string tel);
        public int ultimoIdPersona();
    }
}