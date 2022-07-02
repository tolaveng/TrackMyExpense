using System.Linq.Expressions;

namespace Core.Application.Specifications.Base
{
    public class AndSpecification<T> : BaseSpecification<T>
    {
        private readonly BaseSpecification<T> _left;
        private readonly BaseSpecification<T> _right;

        public AndSpecification(BaseSpecification<T> left, BaseSpecification<T> right)
        {
            _right = right;
            _left = left;
        }

        // Overriden virtual property
        public override Expression<Func<T, bool>> FilterExpression {
            get {
                var rightExp = _right.FilterExpression;
                var leftExp = _left.FilterExpression;
                var rightParam = new ParameterVisitor(rightExp.Parameters, leftExp.Parameters)
                      .VisitAndConvert(rightExp.Body, "AndAlso");

            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(leftExp.Body, rightParam),
                leftExp.Parameters);
            }
        }
    }
}
