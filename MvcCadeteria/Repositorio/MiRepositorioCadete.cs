using System;
using MvcCadeteria.Models;
using MvcCadeteria.ViewModels;

namespace MvcCadeteria.Repositorio
{
    public interface MiRepositorioCadete
    {
        public List<Cadete> getCadetes();
        public Cadete getCadete(int id);
        public bool updateCadete(Cadete cdt);
        public bool deleteCadete(int id_cdt);
        public bool altaCadete(Cadete cdt);
        public int ultimoIdPersona();
    }
}