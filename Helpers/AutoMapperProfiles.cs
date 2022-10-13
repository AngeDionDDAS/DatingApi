using AutoMapper;
using DatingApi.Dtos;
using DatingApi.Models;

namespace DatingApi.Helpers
{
    public class AutoMapperProfiles: Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User, UserForListDto>()
                .ForMember(dest => dest.PhotoUrl, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);

                })
                .ForMember(dest => dest.Age, opt =>
                {
                    opt.ResolveUsing(d => d.DateOfBirth.CalculeAge());
                });


            CreateMap<User, UserForDetailedDto>()
                .ForMember(dest => dest.Urlphoto, opt =>
                {
                    opt.MapFrom(src => src.Photos.FirstOrDefault(p => p.IsMain).Url);
                })
                .ForMember(dest => dest.Age, opt =>
                {
                    opt.ResolveUsing(d => d.DateOfBirth.CalculeAge());
                });

            CreateMap<Photo, PhotosForDetailedDto>();
        }

        
    } 
}
