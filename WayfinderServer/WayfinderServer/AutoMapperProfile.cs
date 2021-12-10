using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WayfinderServer.DTOs;
using WayfinderServer.Entities;

namespace WayfinderServer
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Place, PlaceNewDTO>();
            CreateMap<PlaceNewDTO, Place>();
            CreateMap<Place, PlaceDTO>();
            CreateMap<PlaceDTO, Place>();

            CreateMap<Floor, FloorNewDTO>();
            CreateMap<FloorNewDTO, Floor>();
            CreateMap<Floor, FloorDTO>();
            CreateMap<FloorDTO, Floor>();

            CreateMap<Category, CategoryNewDTO>();
            CreateMap<CategoryNewDTO, Category>();
            CreateMap<Category, CategoryDTO>();
            CreateMap<CategoryDTO, Category>();

            CreateMap<Target, TargetNewDTO>();
            CreateMap<TargetNewDTO, Target>();
            CreateMap<Target, TargetDTO>();
            CreateMap<TargetDTO, Target>();

            CreateMap<FloorPlan3D, FloorPlan3DDTO>();
            CreateMap<FloorPlan3DDTO, FloorPlan3D>();

            CreateMap<FloorPlan2D, FloorPlan2DDTO>();
            CreateMap<FloorPlan2DDTO, FloorPlan2D>();

            CreateMap<VirtualObject, VirtualObjectNewDTO>();
            CreateMap<VirtualObjectNewDTO, VirtualObject>();

            CreateMap<VirtualObject, VirtualObjectCoordinatesDTO>();
            CreateMap<VirtualObjectCoordinatesDTO, VirtualObject>();

            CreateMap<VirtualObjectType, VirtualObjectTypeDTO>();
            CreateMap<VirtualObjectTypeDTO, VirtualObjectType>();

            CreateMap<VirtualObject, VirtualObjectDTO>();
            CreateMap<VirtualObjectDTO, VirtualObject>();

            CreateMap<FloorSwitcher, FloorSwitcherNewDTO>();
            CreateMap<FloorSwitcherNewDTO, FloorSwitcher>();

            CreateMap<FloorSwitcher, FloorSwitcherDTO>();
            CreateMap<FloorSwitcherDTO, FloorSwitcher>();

            CreateMap<FloorSwitcherPointNewDTO, FloorSwitcherPoint>();
            CreateMap<FloorSwitcherPoint, FloorSwitcherPointNewDTO>();

            CreateMap<FloorSwitcherPoint, FloorSwitcherPointDTO>();
            CreateMap<FloorSwitcherPointDTO, FloorSwitcherPoint>();

            CreateMap<TargetDistanceNewDTO, TargetDistance>();
            CreateMap<TargetDistance, TargetDistanceNewDTO>();

            CreateMap<FloorSwitcherPointDistanceNewDTO, FloorSwitcherPointDistance>();
            CreateMap<FloorSwitcherPointDistance, FloorSwitcherPointDistanceNewDTO>();

            CreateMap<FloorBaseDTO, Floor>();
            CreateMap<Floor, FloorBaseDTO>();

            CreateMap<TargetBaseDTO, Target>();
            CreateMap<Target, TargetBaseDTO>();

            CreateMap<MarkerNewDTO, Marker>();
            CreateMap<Marker, MarkerNewDTO>();

            CreateMap<MarkerDTO, Marker>();
            CreateMap<Marker, MarkerDTO>();

            CreateMap<TargetPathStepsDTO, Target>();
            CreateMap<Target, TargetPathStepsDTO>();

            CreateMap<QRCodePointDTO, Target>();
            CreateMap<Target, QRCodePointDTO>();

            CreateMap<QRCodePointDTO, Marker>();
            CreateMap<Marker, QRCodePointDTO>();

            CreateMap<QRCodePointDTO, IQRCodePoint>();
            CreateMap<IQRCodePoint, QRCodePointDTO>();

        }

    }
}
