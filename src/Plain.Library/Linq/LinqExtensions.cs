using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System.Linq
{
    public static class LinqExtensions
    {
        public static IQueryable<T> ToPage<T>(this IQueryable<T> items, int page, int pageSize) where T : class
        {
            if (page <= 0)
                page = 1;

            if (pageSize > 0)
            {
                items = items.Skip((page - 1) * pageSize).Take(pageSize);
            }
            return items;
        }
    }
}
