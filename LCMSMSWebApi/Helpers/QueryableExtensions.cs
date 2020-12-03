using LCMSMSWebApi.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LCMSMSWebApi.Helpers
{
    public static class QueryableExtensions
    {
        public static IQueryable<T> Paginate<T>(this IQueryable<T> queryable, PaginationDTO paginationDTO)
        {
            return queryable
                .Skip((paginationDTO.Page - 1) * paginationDTO.RecordsPerPage)
                .Take(paginationDTO.RecordsPerPage);
        }

        /// <summary>
        /// Sort data dynamically by passing the property name as a string and sort direction.
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="query"></param>
        /// <param name="sortColumn"></param>
        /// <param name="descending"></param>
        /// <returns></returns>
        public static IQueryable<T> OrderByDynamic<T>(this IQueryable<T> query, string sortColumn, bool descending)
        {

            if (string.IsNullOrWhiteSpace(sortColumn)) return query;

            // Dynamically creates a call like this: query.OrderBy(x =&gt; x.SortColumn)
            var parameter = Expression.Parameter(typeof(T), "x");

            string command = "OrderBy";

            if (descending)
            {
                command = "OrderByDescending";
            }

            Expression resultExpression = null;

            var property = typeof(T).GetProperty(sortColumn);

            // this is the part x.SortColumn
            var propertyAccess = Expression.MakeMemberAccess(parameter, property);

            // this is the part p =&gt; x.SortColumn
            var orderByExpression = Expression.Lambda(propertyAccess, parameter);

            // finally, call the "OrderBy" / "OrderByDescending" method with the order by lamba expression
            resultExpression = Expression.Call(typeof(Queryable), command, new Type[] { typeof(T), property.PropertyType },
               query.Expression, Expression.Quote(orderByExpression));

            return query.Provider.CreateQuery<T>(resultExpression);
        }
    }
}
