using Microsoft.EntityFrameworkCore;
using Prism.Core.Domain.Models;
using Prism.Core.Domain.Models.Includes;

namespace Prism.Core.DataAccess.Extensions;

internal static class IncludeExtensions
{
    public static IQueryable<ThemeVersion> Include(this IQueryable<ThemeVersion> query, ThemeVersionInclude include)
    {
        if (include.Theme)
            query = query.Include(x => x.Theme);
        if (include.UserCreate)
            query = query.Include(x => x.UserCreate);
        if (include.UserAccept)
            query = query.Include(x => x.UserAccept);
        return query;
    }

    public static IQueryable<ThemeField> Include(this IQueryable<ThemeField> query, ThemeFieldInclude include)
    {
        if (include.Theme)
            query = query.Include(x => x.Theme);
        return query;
    }

    public static IQueryable<ThemeFieldVersion> Include(this IQueryable<ThemeFieldVersion> query, ThemeFieldVersionInclude include)
    {
        if (include.ThemeField)
            query = query.Include(x => x.ThemeField);
        if (include.UserCreate)
            query = query.Include(x => x.UserCreate);
        if (include.UserAccept)
            query = query.Include(x => x.UserAccept);
        return query;
    }
}
