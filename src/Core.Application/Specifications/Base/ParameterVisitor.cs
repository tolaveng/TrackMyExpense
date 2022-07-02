using System;
using System.Collections.ObjectModel;
using System.Linq.Expressions;


namespace Core.Application.Specifications
{
    // https://stackoverflow.com/questions/5430996/replacing-the-parameter-name-in-the-body-of-an-expression
    class ParameterVisitor : ExpressionVisitor
    {
        private readonly ReadOnlyCollection<ParameterExpression> from, to;
        public ParameterVisitor(
            ReadOnlyCollection<ParameterExpression> from,
            ReadOnlyCollection<ParameterExpression> to)
        {
            if (from == null) throw new ArgumentNullException("from");
            if (to == null) throw new ArgumentNullException("to");
            if (from.Count != to.Count) throw new InvalidOperationException(
                  "Parameter lengths must match");
            this.from = from;
            this.to = to;
        }
        protected override Expression VisitParameter(ParameterExpression node)
        {
            for (int i = 0; i < from.Count; i++)
            {
                if (node == from[i]) return to[i];
            }
            return node;
        }
    }
}
