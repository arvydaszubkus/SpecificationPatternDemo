using System;
using System.Linq.Expressions;

namespace SpecificationPatternDemo.Specifications
{
    public abstract class Specification<T> : ISpecification<T>
    {
        public abstract Expression<Func<T, bool>> ToExpression();

        public bool IsSatisfiedBy(T entity) => ToExpression().Compile()(entity);

        public ISpecification<T> And(ISpecification<T> other) => new AndSpecification<T>(this, other);
        public ISpecification<T> Or(ISpecification<T> other) => new OrSpecification<T>(this, other);
        public ISpecification<T> Not() => new NotSpecification<T>(this);
    }
}
