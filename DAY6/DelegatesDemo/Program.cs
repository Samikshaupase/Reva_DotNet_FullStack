using System.Runtime.InteropServices;
using System;

namespace DelegatesDemo;
class Program
{
    static void Main(string[] args)
     {
         DelegatesDemoApp app = new DelegatesDemoApp();

            // app.Add(10,20);
            // app.Substract(10,20);
         app.Run();
     }
 }

class DelegatesDemoApp
 {
     delegate int MathOperation(int a, int b);
        
          
     public void Run()
     {
         MathOperation operation = Add;

         operation += Substract;
         operation += Multiply;
         operation += Divide;
         
         operation -= Substract;

         var result = operation(5,3);
         Console.WriteLine($"Final Result: {result}");
     }

     public int Add(int a ,int b)
     {
         Console.WriteLine($"The sum of {a} and {b} is : {a+b}");
         return a+b;
     }

     public int Substract(int a ,int b)
     {
         Console.WriteLine($"The difference between {a} and {b} is : {a-b}");
         return a-b;
     }

     public int Multiply(int a ,int b)
     {
         Console.WriteLine($"The multiplication of {a} and {b} is : {a*b}");
         return a*b;
     }

     public int Divide(int a ,int b)
     {
        if (b != 0)
        {
            Console.WriteLine($"The division of {a} and {b} is : {a/b}");
            return a/b;
        }
        else
        {
            Console.WriteLine("Cannot divided by 0");
        }
        return 0;
     }
 }
