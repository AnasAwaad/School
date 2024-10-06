using Microsoft.EntityFrameworkCore;

namespace School.Core.Wrappers;
public static class QueryableExtensions
{
    public static async Task<PaginatedResult<T>> ToPaginatedListAsync<T>(this IQueryable<T> source, int pageNumber, int pageSize, List<string>? messages = null)
        where T : class
    {
        if (source == null)
        {
            throw new Exception("Source is Empty");
        }

        pageNumber = pageNumber == 0 ? 1 : pageNumber;
        pageSize = pageSize == 0 ? 10 : pageSize;
        int count = await source.AsNoTracking().CountAsync();

        if (count == 0)
            return PaginatedResult<T>.Success(new List<T>(), count, pageNumber, pageSize, messages);

        var items = await source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToListAsync();

        return PaginatedResult<T>.Success(items, count, pageNumber, pageSize, messages);
    }
}
