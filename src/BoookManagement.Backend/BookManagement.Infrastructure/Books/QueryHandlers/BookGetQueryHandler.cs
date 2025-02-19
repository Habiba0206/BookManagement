using AutoMapper;
using BookManagement.Application.Books.Models;
using BookManagement.Application.Books.Queries;
using BookManagement.Application.Books.Services;
using BookManagement.Domain.Common.Queries;
using Microsoft.EntityFrameworkCore;

namespace BookManagement.Infrastructure.Books.QueryHandlers;

public class BookGetQueryHandler(
    IMapper mapper,
    IBookService answerService)
    : IQueryHandler<BookGetQuery, ICollection<GetBookDto>>
{
    public async Task<ICollection<GetBookDto>> Handle(BookGetQuery request, CancellationToken cancellationToken)
    {
        var result = await answerService.Get(
            request.BookFilter,
            new QueryOptions()
            {
                QueryTrackingMode = QueryTrackingMode.AsNoTracking
            })
            .OrderByDescending(book => book.PopularityScore)
            .ToListAsync(cancellationToken);

        return mapper.Map<ICollection<GetBookDto>>(result);
    }
}
