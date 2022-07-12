using System;
using System.Collections.Generic;

namespace Core.Data.Paging
{
    public static class PaginateExtensions
    {
        public static IPaginate<T> ToPaginate<T>(this IEnumerable<T> source, int index, int size, int from = 0)
        {
            return new Paginate<T>(source, index, size, from);
        }

        public static IPaginate<TResult> ToPaginate<TSource, TResult>(this IEnumerable<TSource> source,
            Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int index, int size, int from = 0)
        {
            return new Paginate<TSource, TResult>(source, converter, index, size, from);
        }

        public static IPaginate<T> ToSimplePaginate<T>(this IEnumerable<T> source, int index, int size, int from = 0)
        {
            return new SimplePaginate<T>(source, index, size, from);
        }

        public static IPaginate<TResult> ToSimplePaginate<TSource, TResult>(this IEnumerable<TSource> source,
            Func<IEnumerable<TSource>, IEnumerable<TResult>> converter, int index, int size, int from = 0)
        {
            return new SimplePaginate<TSource, TResult>(source, converter, index, size, from);
        }
    }

}
