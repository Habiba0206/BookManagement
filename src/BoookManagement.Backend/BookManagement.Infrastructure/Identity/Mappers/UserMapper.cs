using AutoMapper;
using BookManagement.Application.Identity.Models;
using BookManagement.Domain.Entities;

namespace BookManagement.Infrastructure.Identity.Mappers;

public class UserMapper : Profile
{
    public UserMapper()
    {
        CreateMap<User, UserDto>().ReverseMap();
        CreateMap<SignInDetails, User>().ReverseMap();
        CreateMap<SignUpDetails, User>().ReverseMap();
    }
}
