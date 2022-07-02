using System;
using System.Linq.Expressions;

// https://medium.com/c-sharp-progarmming/specification-design-pattern-c814649be0ef
// https://thecodeblogger.com/2021/07/02/net-composite-specifications-using-ef-core/
// https://enterprisecraftsmanship.com/posts/specification-pattern-c-implementation/

namespace Core.Application.Specifications.Base
{
    public interface IBaseSpecification<T>
    {
        Expression<Func<T, bool>> FilterExpression { get; }
        bool IsSatisfiedBy(T entity);
    }
}
