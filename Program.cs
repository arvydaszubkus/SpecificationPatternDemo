using System;
using System.Collections.Generic;
using System.Linq;
using SpecificationPatternDemo.Models;
using SpecificationPatternDemo.Specifications;

namespace SpecificationPatternDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            var orders = new List<Order>
            {
                new Order { Id = 1, TotalAmount = 500m, IsPaid = true, CreatedAt = DateTime.UtcNow.AddDays(-10) },
                new Order { Id = 2, TotalAmount = 2000m, IsPaid = true, CreatedAt = DateTime.UtcNow.AddDays(-5) },
                new Order { Id = 3, TotalAmount = 1500m, IsPaid = false, CreatedAt = DateTime.UtcNow.AddDays(-2) },
                new Order { Id = 4, TotalAmount = 50m, IsPaid = false, CreatedAt = DateTime.UtcNow.AddMonths(-14) }, // last year
            };

            var paidSpec = new PaidOrderSpecification();
            var expensiveSpec = new ExpensiveOrderSpecification(1000m);
            var recentSpec = new CreatedWithinDaysSpecification(7);
            var cheapSpec = new CheapOrderSpecification(100m);
            var unpaidSpec = new UnpaidOrderSpecification();
            var currentYearSpec = new CurrentYearOrderSpecification();

            // Paid & Expensive & Recent
            var combined = paidSpec.And(expensiveSpec).And(recentSpec);
            var matches = orders.AsQueryable().Where(combined.ToExpression()).ToList();
            Console.WriteLine("Paid, expensive, and recent orders:");
            foreach (var o in matches)
                Console.WriteLine($"Order {o.Id}: {o.TotalAmount}, paid:{o.IsPaid}, created:{o.CreatedAt}");
            Console.WriteLine();

            // Cheap OR unpaid
            var cheapOrUnpaid = cheapSpec.Or(unpaidSpec);
            var cheapOrUnpaidOrders = orders.AsQueryable().Where(cheapOrUnpaid.ToExpression()).ToList();
            Console.WriteLine("Cheap OR unpaid orders:");
            foreach (var o in cheapOrUnpaidOrders)
                Console.WriteLine($"Order {o.Id}: {o.TotalAmount}, paid:{o.IsPaid}, created:{o.CreatedAt}");
            Console.WriteLine();

            // Orders from this year
            var thisYearOrders = orders.AsQueryable().Where(currentYearSpec.ToExpression()).ToList();
            Console.WriteLine("Orders created this year:");
            foreach (var o in thisYearOrders)
                Console.WriteLine($"Order {o.Id}: {o.TotalAmount}, paid:{o.IsPaid}, created:{o.CreatedAt}");
            Console.WriteLine();
        }
    }
}
