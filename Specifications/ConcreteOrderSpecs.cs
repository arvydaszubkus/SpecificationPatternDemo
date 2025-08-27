using System;
using System.Linq.Expressions;
using SpecificationPatternDemo.Models;

namespace SpecificationPatternDemo.Specifications
{
    public class PaidOrderSpecification : Specification<Order>
    {
        public override Expression<Func<Order, bool>> ToExpression() => o => o.IsPaid;
    }

    public class ExpensiveOrderSpecification : Specification<Order>
    {
        private readonly decimal _minAmount;
        public ExpensiveOrderSpecification(decimal minAmount) => _minAmount = minAmount;

        public override Expression<Func<Order, bool>> ToExpression() => o => o.TotalAmount >= _minAmount;
    }

    public class CreatedWithinDaysSpecification : Specification<Order>
    {
        private readonly int _days;
        public CreatedWithinDaysSpecification(int days) => _days = days;

        public override Expression<Func<Order, bool>> ToExpression() =>
            o => (DateTime.UtcNow - o.CreatedAt).TotalDays <= _days;
    }

    public class CheapOrderSpecification : Specification<Order>
    {
        private readonly decimal _maxAmount;
        public CheapOrderSpecification(decimal maxAmount) => _maxAmount = maxAmount;

        public override Expression<Func<Order, bool>> ToExpression() => o => o.TotalAmount <= _maxAmount;
    }

    public class UnpaidOrderSpecification : Specification<Order>
    {
        public override Expression<Func<Order, bool>> ToExpression() => o => !o.IsPaid;
    }

    public class CurrentYearOrderSpecification : Specification<Order>
    {
        public override Expression<Func<Order, bool>> ToExpression() =>
            o => o.CreatedAt.Year == DateTime.UtcNow.Year;
    }
}
