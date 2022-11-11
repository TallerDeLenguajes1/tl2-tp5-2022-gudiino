using MvcCadeteria.Models;
namespace MvcCadeteria.ViewModels;
public class MapperCdtViewModel
{
    public List<CdtViewModel> GetCadeteViewModel(List<Cadete> cdts)
    {
        List<CdtViewModel> viewModels = new List<CdtViewModel>();
        foreach (var cdt in cdts)
        {
            var cdtViewModel = new CdtViewModel();
            cdtViewModel.id = cdt.getId().ToString();
            cdtViewModel.nombre = cdt.getNom();
            cdtViewModel.calle = cdt.getCalle();
            cdtViewModel.numero = cdt.getNumero().ToString();
            cdtViewModel.telefono = cdt.getTelefono();
            viewModels.Add(cdtViewModel);
        }
        return viewModels;
    }
}