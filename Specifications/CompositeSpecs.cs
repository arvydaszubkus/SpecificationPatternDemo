using System;
using System.Linq.Expressions;

namespace SpecificationPatternDemo.Specifications
{
    public class AndSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public AndSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression() =>
            _left.ToExpression().AndAlso(_right.ToExpression());
    }

    public class OrSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _left;
        private readonly ISpecification<T> _right;

        public OrSpecification(ISpecification<T> left, ISpecification<T> right)
        {
            _left = left;
            _right = right;
        }

        public override Expression<Func<T, bool>> ToExpression() =>
            _left.ToExpression().OrElse(_right.ToExpression());
    }

    public class NotSpecification<T> : Specification<T>
    {
        private readonly ISpecification<T> _inner;

        public NotSpecification(ISpecification<T> inner) => _inner = inner;

        public override Expression<Func<T, bool>> ToExpression() =>
            _inner.ToExpression().Not();
    }
}
