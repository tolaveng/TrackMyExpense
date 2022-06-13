﻿using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Core.Application.Utils
{
    // https://stackoverflow.com/questions/5430996/replacing-the-parameter-name-in-the-body-of-an-expression
    public static class LinqUtil
    {
        public static Expression<Func<T, bool>> AndAlso<T>(
          Expression<Func<T, bool>> x, Expression<Func<T, bool>> y)
        {
            var newY = new ParameterVisitor(y.Parameters, x.Parameters)
                      .VisitAndConvert(y.Body, "AndAlso");
            return Expression.Lambda<Func<T, bool>>(
                Expression.AndAlso(x.Body, newY),
                x.Parameters);
        }
    }

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
