using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Vega.Models;
using Vega.ApiViewModels;

namespace Vega.Common
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<Make, MakeViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name))
                .ForMember(dest => dest.Models, opt => opt.MapFrom(src => src.Models)).ReverseMap();

            CreateMap<Model, ModelViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();

            CreateMap<Feature, FeatureViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(dest => dest.Name, opt => opt.MapFrom(src => src.Name)).ReverseMap();

            CreateMap<VehicleViewModel, Vehicle>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.ContactName, opt => opt.MapFrom(src => src.ContactInfo.Name))
                .ForMember(dest => dest.ContactPhone, opt => opt.MapFrom(src => src.ContactInfo.Phone))
                .ForMember(dest => dest.ContactEmail, opt => opt.MapFrom(src => src.ContactInfo.Email))
                .ForMember(dest => dest.Features, opt => opt.Ignore())
                .AfterMap((vehicleViewModel, vehicle) => {
                    //Remove unselected features
                    var removedFeatures = vehicle.Features.Where(f => !vehicleViewModel.Features.Contains(f.FeatureId)).ToList();
                    foreach (var f in removedFeatures)
                        vehicle.Features.Remove(f);

                    //add new features
                    var addedFeatures = vehicleViewModel.Features.Where(id => !vehicle.Features.Any(f => f.FeatureId == id)).Select(id => new VehicleFeature() { FeatureId = id });
                    foreach (var f in addedFeatures)
                            vehicle.Features.Add(f);
                });

            CreateMap<Vehicle, VehicleViewModel>()
                .ForMember(dest => dest.ContactInfo, opt => opt.MapFrom(src => new ContactViewodel() { Name = src.ContactName, Email = src.ContactEmail, Phone = src.ContactPhone } ))
                .ForMember(dest => dest.Features, opt => opt.MapFrom(src => src.Features.Select(f => f.FeatureId)));

        }
    }
}
