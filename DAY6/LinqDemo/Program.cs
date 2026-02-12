using System;
using System.Collections.Generic;
using System.Linq;

namespace LinqDemo

{
    class Program
    {
        public static void Main()
        {

            
            // Customers list
            var customers = new List<Customer>
            {
                new Customer { CustomerId = 1, CustomerName = "Alice" },
                new Customer { CustomerId = 2, CustomerName = "Bob" },
                new Customer { CustomerId = 3, CustomerName = "Charlie" }
            };

            // Orders list
            var orders = new List<Order>
            {
                new Order { OrderId = 101, CustomerId = 1, OrderAmount = 200 },
                new Order { OrderId = 102, CustomerId = 1, OrderAmount = 150 },
                new Order { OrderId = 103, CustomerId = 2, OrderAmount = 300 },
                new Order { OrderId = 104, CustomerId = 2, OrderAmount = 100 }
            };

            // LINQ GroupJoin
            var result = customers.GroupJoin(
                orders,
                c => c.CustomerId,
                o => o.CustomerId,
                (customer, customerOrders) => new
                {
                    customer.CustomerName,
                    OrderCount = customerOrders.Count(),
                    TotalAmount = customerOrders.Sum(o => o.OrderAmount),
                    Orders = customerOrders.ToList()
                });

            
            foreach (var item in result)
            {
                Console.WriteLine($"Customer: {item.CustomerName}");
                Console.WriteLine($"Number of Orders: {item.OrderCount}");
                Console.WriteLine($"Total Order Value: {item.TotalAmount}");
            }

            Console.ReadLine();
        }
    }
}
