using System;
using System.Linq.Expressions;

namespace Core.Application.Specifications.Base
{
    public class BaseSpecification<T> : IBaseSpecification<T>
    {
        public virtual Expression<Func<T, bool>> FilterExpression { get; protected set; }

        public BaseSpecification() { }

        public BaseSpecification(Expression<Func<T, bool>> filter)
        {
            FilterExpression = filter;
        }


        public bool IsSatisfiedBy(T entity)
        {
            Func<T, bool> predicate = FilterExpression.Compile();
            return predicate(entity);
        }

        public BaseSpecification<T> And(BaseSpecification<T> specification)
        {
            return new AndSpecification<T>(this, specification);
        }

        public BaseSpecification<T> Or(BaseSpecification<T> specification)
        {
            return new OrSpecification<T>(this, specification);
        }
    }
}
