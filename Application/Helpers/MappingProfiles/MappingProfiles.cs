using Application.Dtos;
using AutoMapper;
using Doiman;

namespace Application.Helpers.MappingProfiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<Post, PostDto>()
            .ForMember(dto => dto.UserName, x => x.MapFrom(p => p.User.UserName));
        // CreateMap<Post, PostDto>().
        //     ForMember(dto => dto.UserName, 
        //     expression => expression.MapFrom(post => post.User.Username));

        CreateMap<User, UserDto>();
    }
}