using Microsoft.Data.SqlClient; 
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.SqlServer;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

CrmContext _context = new CrmContext();

var employees = _context.Employee
      .Where(e=>e.Age>25)
      .ToList();

// Without ToList()
// Console.WriteLine("customers.GetType(): " + customers.GetType());
// Console.WriteLine($"Query: {customers.ToQueryString()}");

// With ToList()
// Console.WriteLine("customers.GetType(): " + customers.GetType());

// With AsEnumerable()
// Console.WriteLine("customers.GetType(): " + customers.GetType());

// employees.Add(new Employee { Name = "John Doe", Age = 30 });
// _context.SaveChanges();

Console.WriteLine($" Employee Count: {employees.Count()}");

employees.Add(new Employee { Name = "Danny Lee", Age = 30 });
_context.SaveChanges();

// Because DbSet is not IList, index operator will not work
// var first = _context.Customers[0];

var john = _context.Employee.FirstOrDefault(e => e.Name == "Rahul");
if (john != null) john.Age = 31;

_context.SaveChanges();

foreach(var employee in employees)
{
    Console.WriteLine($"Id: {employee.Id} Employee:{employee.Name},Age:{employee.Age}");
}

class CrmContext :DbContext
{
    public DbSet<Employee> Employee {get; set;}
    public DbSet<Order> Orders { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlServer("Server=localhost\\SQLEXPRESS;Database=CrmApplicationDb;Integrated Security=True;TrustServerCertificate=True");
    }
}
public class Order
{
    [Key]
    public int OrderId { get; set; }

    [Required]
    [MaxLength(100)]
    [MinLength(3)]
    public string Product { get; set; }

    [Required]
    [Precision(18, 2)]
    public decimal Price { get; set; }

    [ForeignKey("CustomerId")]
    public int CustomerId { get; set; }
    public Employee Employee { get; set; }
}

public class Employee
{
    public int Id{get; set;}
    public string Name{get; set;}
    public int Age {get; set;}
}