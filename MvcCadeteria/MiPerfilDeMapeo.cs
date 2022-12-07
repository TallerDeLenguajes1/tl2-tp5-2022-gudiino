using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MvcCadeteria.Models;
using MvcCadeteria.ViewModels;

namespace MvcCadeteria
{
    public class MiPerfilDeMapeo : Profile
    {
        public MiPerfilDeMapeo()
        {
            CreateMap<Cadete, CdtViewModel>()
            .ForMember(d=> d.id, o=> o.MapFrom(s=> s.cdt_id))
            .ForMember(d=> d.nombre, o=> o.MapFrom(s=> s.cdt_nombre))
            .ForMember(d=> d.domicilio, o=> o.MapFrom(s=> s.cdt_domicilio))
            .ForMember(d=> d.telefono, o=> o.MapFrom(s=> s.cdt_telefono))
            .ReverseMap();
            
            CreateMap<Cadete, EditarCdtViewModel>()
            .ForMember(d=> d.id, o=> o.MapFrom(s=> s.cdt_id))
            .ForMember(d=> d.nombre, o=> o.MapFrom(s=> s.cdt_nombre))
            .ForMember(d=> d.domicilio, o=> o.MapFrom(s=> s.cdt_domicilio))
            .ForMember(d=> d.telefono, o=> o.MapFrom(s=> s.cdt_telefono))
            .ForMember(d=> d.sucursal, o=> o.MapFrom(s=> s.cdt_id_sucursal))
            .ReverseMap();

            CreateMap<Cadete, AltaCdtViewModel>()
            .ForMember(d=> d.Id, o=> o.MapFrom(s=> s.cdt_id))
            .ForMember(d=> d.Nombre, o=> o.MapFrom(s=> s.cdt_nombre))
            .ForMember(d=> d.Direccion, o=> o.MapFrom(s=> s.cdt_domicilio))
            .ForMember(d=> d.Telefono, o=> o.MapFrom(s=> s.cdt_telefono))
            .ForMember(d=> d.Sucursal, o=> o.MapFrom(s=> s.cdt_id_sucursal))
            .ReverseMap();
            // CreateMap<EditarCdtViewModel, Cadete>()
            // .ForMember(d=> d., o=> o.MapFrom(s=> s.id))
            // .ForMember(d=> d.nombre, o=> o.MapFrom(s=> s.getNom()))
            // .ForMember(d=> d.calle, o=> o.MapFrom(s=> s.getCalle()))
            // .ForMember(d=> d.numero, o=> o.MapFrom(s=> s.getNumero()))
            // .ForMember(d=> d.telefono, o=> o.MapFrom(s=> s.getTelefono()))
            // .ReverseMap();
        }
    }
}