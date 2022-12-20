using System;
using MvcCadeteria.Models;

namespace MvcCadeteria.Repositorio
{
    public interface MiRepositorioCadete
    {
        public List<Cadete> getCadetes();
        public Cadete getCadeteId(int id);
        public bool updateCadete(Cadete cdt);
        public bool deleteCadete(int id_cdt);
        public bool altaCadete(Cadete cdt);
        public List<Cadete> GetCdtsCantPd2s();
    }
}