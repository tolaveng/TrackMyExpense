using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Infrastructure.Extensions
{
    public static class LinqExtensions
    {
        // https://stackoverflow.com/questions/307512/how-do-i-apply-orderby-on-an-iqueryable-using-a-string-column-name-within-a-gene/1670085#1670085
        public static IQueryable<T> SortBy<T>(this IQueryable<T> source, String propertyName, string direction)
        {
            if (source == null) throw new ArgumentNullException("source");
            if (String.IsNullOrEmpty(propertyName)) return source;

            // Create a parameter to pass into the Lambda expression
            //(Entity => Entity.OrderByField).
            var parameter = Expression.Parameter(typeof(T), "Entity");

            //  create the selector part, but support child properties (it works without . too)
            String[] childProperties = propertyName.Split('.');
            MemberExpression property = Expression.Property(parameter, childProperties[0]);
            for (int i = 1; i < childProperties.Length; i++)
            {
                property = Expression.Property(property, childProperties[i]);
            }

            LambdaExpression selector = Expression.Lambda(property, parameter);

            string methodName = direction.ToLower() == "asc" ? "OrderBy" : "OrderByDescending";

            MethodCallExpression resultExp = Expression.Call(typeof(Queryable), methodName,
                                            new Type[] { source.ElementType, property.Type },
                                            source.Expression, Expression.Quote(selector));

            return source.Provider.CreateQuery<T>(resultExp);
        }
    }
}
