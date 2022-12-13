using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using MvcCadeteria.Models;
using MvcCadeteria.ViewsModels;

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
            .ForMember(d=> d.Nombre, o=> o.MapFrom(s=> s.cdt_nombre))
            .ForMember(d=> d.Direccion, o=> o.MapFrom(s=> s.cdt_domicilio))
            .ForMember(d=> d.Telefono, o=> o.MapFrom(s=> s.cdt_telefono))
            .ForMember(d=> d.Sucursal, o=> o.MapFrom(s=> s.cdt_id_sucursal))
            .ReverseMap();
            // CreateMap<Pedido, Pd2ViewModel>()
            // .ForMember(d=> d.id_pd2, o=> o.MapFrom(s=> s.id_pd2))
            // .ForMember(d=> d.obs, o=> o.MapFrom(s=> s.detalle_pedido))
            // .ForMember(d=> d.id_cli, o=> o.MapFrom(s=> s.id_cli))
            // .ForMember(d=> d.id_cdt, o=> o.MapFrom(s=> s.id_cdt))
            // .ForMember(d=> d.estado, o=> o.MapFrom(s=> s.estado_pedido))
            // .ReverseMap();
            CreateMap<Pedido, AltaPd2ViewModel>()
            .ForMember(d=> d.detalle_pedido, o=> o.MapFrom(s=> s.detalle_pedido))
            .ForMember(d=> d.estado_pedido, o=> o.MapFrom(s=> s.estado_pedido))
            .ReverseMap();
            //****************detalles
            CreateMap<Pedido, Pd2ViewModel>().IncludeMembers(d => d.cli_pd2, d => d.cdt_pd2)
            .ForMember(d=> d.id_pd2, o=> o.MapFrom(s=> s.id_pd2))
            .ForMember(d=> d.obs, o=> o.MapFrom(s=> s.detalle_pedido))
            .ForMember(d=> d.estado, o=> o.MapFrom(s=> s.estado_pedido))
            .ForMember(d=> d.id_cli, o=> o.MapFrom(s=> s.id_cli))
            .ForMember(d=> d.cli_nom, o=> o.MapFrom(s=> s.cli_pd2.cli_nombre))
            .ForMember(d=> d.Direccion, o=> o.MapFrom(s=> s.cli_pd2.cli_domicilio))
            .ForMember(d=> d.Telefono, o=> o.MapFrom(s=> s.cli_pd2.cli_telefono))
            .ForMember(d=> d.detalle_direccion, o=> o.MapFrom(s=> s.cli_pd2.cli_detalle_direccion))
            .ForMember(d=> d.id_cdt, o=> o.MapFrom(s=> s.id_cdt))
            .ForMember(d=> d.cdt_nom, o=> o.MapFrom(s=> s.cdt_pd2.cdt_nombre))
            .ForMember(d=> d.cdt_tel, o=> o.MapFrom(s=> s.cdt_pd2.cdt_telefono))
            .ReverseMap();

            CreateMap<Cliente, Pd2ViewModel>()
            .ForMember(d=> d.cli_nom, o=> o.MapFrom(s=> s.cli_nombre))
            .ForMember(d=> d.Direccion, o=> o.MapFrom(s=> s.cli_domicilio))
            .ForMember(d=> d.Telefono, o=> o.MapFrom(s=> s.cli_telefono))
            .ForMember(d=> d.detalle_direccion, o=> o.MapFrom(s=> s.cli_detalle_direccion))
            .ReverseMap();

            CreateMap<Cadete, Pd2ViewModel>()
            .ForMember(d=> d.cdt_nom, o=> o.MapFrom(s=> s.cdt_nombre))
            .ForMember(d=> d.cdt_tel, o=> o.MapFrom(s=> s.cdt_telefono))
            .ReverseMap();
            //editar pedido
            CreateMap<Pedido, EditarPd2ViewModel>().IncludeMembers(d => d.cli_pd2, d => d.cdt_pd2)
            .ForMember(d=> d.id_pd2, o=> o.MapFrom(s=> s.id_pd2))
            .ForMember(d=> d.obs, o=> o.MapFrom(s=> s.detalle_pedido))
            .ForMember(d=> d.estado, o=> o.MapFrom(s=> s.estado_pedido))
            .ForMember(d=> d.id_cli, o=> o.MapFrom(s=> s.id_cli))
            .ForMember(d=> d.cli_nom, o=> o.MapFrom(s=> s.cli_pd2.cli_nombre))
            .ForMember(d=> d.Direccion, o=> o.MapFrom(s=> s.cli_pd2.cli_domicilio))
            .ForMember(d=> d.Telefono, o=> o.MapFrom(s=> s.cli_pd2.cli_telefono))
            .ForMember(d=> d.detalle_direccion, o=> o.MapFrom(s=> s.cli_pd2.cli_detalle_direccion))
            .ForMember(d=> d.id_cdt, o=> o.MapFrom(s=> s.id_cdt))
            .ForMember(d=> d.cdt_nom, o=> o.MapFrom(s=> s.cdt_pd2.cdt_nombre))
            .ReverseMap();

            CreateMap<Cliente, EditarPd2ViewModel>()
            .ForMember(d=> d.cli_nom, o=> o.MapFrom(s=> s.cli_nombre))
            .ForMember(d=> d.Direccion, o=> o.MapFrom(s=> s.cli_domicilio))
            .ForMember(d=> d.Telefono, o=> o.MapFrom(s=> s.cli_telefono))
            .ForMember(d=> d.detalle_direccion, o=> o.MapFrom(s=> s.cli_detalle_direccion))
            .ReverseMap();

            CreateMap<Cadete, EditarPd2ViewModel>()
            .ForMember(d=> d.cdt_nom, o=> o.MapFrom(s=> s.cdt_nombre))
            .ReverseMap();
        }
    }
}