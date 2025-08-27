using System;
using System.Linq.Expressions;

namespace SpecificationPatternDemo.Specifications
{
    internal class ParameterReplacer : ExpressionVisitor
    {
        private readonly ParameterExpression _parameter;
        public ParameterReplacer(ParameterExpression parameter) => _parameter = parameter;

        protected override Expression VisitParameter(ParameterExpression node) => _parameter;
    }

    public static class ExpressionExtensions
    {
        public static Expression<Func<T, bool>> AndAlso<T>(
            this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            var param = Expression.Parameter(typeof(T));
            var leftBody = new ParameterReplacer(param).Visit(left.Body);
            var rightBody = new ParameterReplacer(param).Visit(right.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.AndAlso(leftBody, rightBody), param);
        }

        public static Expression<Func<T, bool>> OrElse<T>(
            this Expression<Func<T, bool>> left,
            Expression<Func<T, bool>> right)
        {
            var param = Expression.Parameter(typeof(T));
            var leftBody = new ParameterReplacer(param).Visit(left.Body);
            var rightBody = new ParameterReplacer(param).Visit(right.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.OrElse(leftBody, rightBody), param);
        }

        public static Expression<Func<T, bool>> Not<T>(
            this Expression<Func<T, bool>> expr)
        {
            var param = Expression.Parameter(typeof(T));
            var body = new ParameterReplacer(param).Visit(expr.Body);
            return Expression.Lambda<Func<T, bool>>(Expression.Not(body), param);
        }
    }
}
