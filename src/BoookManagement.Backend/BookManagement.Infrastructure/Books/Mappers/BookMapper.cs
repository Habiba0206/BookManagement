using AutoMapper;
using BookManagement.Application.Books.Models;
using BookManagement.Domain.Entities;

namespace BookManagement.Infrastructure.Books.Mappers;

public class BookMapper : Profile
{
    public BookMapper()
    {
        CreateMap<Book, GetBookDto>().ReverseMap();
        CreateMap<Book, CreateBookDto>().ReverseMap();
        CreateMap<GetBookDto, CreateBookDto>().ReverseMap();
    }
}
