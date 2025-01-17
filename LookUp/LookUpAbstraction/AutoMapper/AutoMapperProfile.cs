using AutoMapper;
using LookUpAbstraction.DTO.CascadingLookUp.Request;
using LookUpAbstraction.DTO.CascadingLookUp.Response;
using LookUpAbstraction.DTO.LookUp.Request;
using LookUpAbstraction.DTO.LookUp.Response;
using LookUpAbstraction.DTO.LookUpType.Request;
using LookUpAbstraction.DTO.LookUpType.Response;
using LookUpData.Models;

namespace LookUpAbstraction.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateLookUpMaps();

            CreateLookUpTypesMaps();

            CreateCascadingLookUpMaps();
        }

        //This function creates all the maps for the LookUp Entity
        private void CreateLookUpMaps()
        {
            //CREATE
            CreateMap<CreateLookUpDTO, LookUp>()
                .ForMember(des => des.LookUpTypeId, opt => opt.MapFrom(src => src.LookUpTypeId))
                .ForMember(des => des.NameEn, opt => opt.MapFrom(src => src.NameEn))
                .ForMember(des => des.NameAr, opt => opt.MapFrom(src => src.NameAr));

            //UPDATE
            CreateMap<UpdateLookUpDTO, LookUp>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.LookUpTypeId, opt => opt.MapFrom(src => src.LookUpTypeId))
                .ForMember(des => des.NameEn, opt => opt.MapFrom(src => src.NameEn))
                .ForMember(des => des.NameAr, opt => opt.MapFrom(src => src.NameAr));

            //GET
            CreateMap<LookUp, LookUpDTO>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.LookUpTypeId, opt => opt.MapFrom(src => src.LookUpTypeId))
                .ForMember(des => des.NameEn, opt => opt.MapFrom(src => src.NameEn))
                .ForMember(des => des.NameAr, opt => opt.MapFrom(src => src.NameAr));
        }

        //This function creates all the maps for the LookUpType Entity
        private void CreateLookUpTypesMaps()
        {
            //CREATE
            CreateMap<CreateLookUpTypeDTO, LookUpType>()
                .ForMember(des => des.Type, opt => opt.MapFrom(src => src.Type));

            //UPDATE
            CreateMap<UpdateLookUpTypeDTO, LookUpType>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.Type, opt => opt.MapFrom(src => src.Type));

            //GET
            CreateMap<LookUpType, LookUpTypeDTO>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.Type, opt => opt.MapFrom(src => src.Type));
        }

        //This function creates all the maps for the CascadingLookUp Entity
        private void CreateCascadingLookUpMaps()
        {
            //CREATE
            CreateMap<CreateCascadingLookUpDTO, CascadingLookUp>()
                .ForMember(des => des.ParentId, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(des => des.ChildId, opt => opt.MapFrom(src => src.ChildId));

            //UPDATE
            CreateMap<UpdateCascadingLookUpDTO, CascadingLookUp>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.ParentId, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(des => des.ChildId, opt => opt.MapFrom(src => src.ChildId));

            //GET
            CreateMap<CascadingLookUp, CascadingLookUpDTO>()
                .ForMember(des => des.Id, opt => opt.MapFrom(src => src.Id))
                .ForMember(des => des.ParentId, opt => opt.MapFrom(src => src.ParentId))
                .ForMember(des => des.ChildId, opt => opt.MapFrom(src => src.ChildId));
        }
    }
}
