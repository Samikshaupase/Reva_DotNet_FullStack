using VehicleFleet.Domain.Entities;
using VehicleFleet.Domain.Interfaces;

namespace VehicleFleet.ConsoleApp
{
    class Program
    {
        static void Main()
        {
            List<Vehicle> fleet = new()
            {
                new Car("Toyota", "Corolla", 40),
                new Truck("Tata", "Ultra", 80)
            };

            // Polymorphism
            foreach (var vehicle in fleet)
            {
                vehicle.Start();
                vehicle.Stop();
            }

            Console.WriteLine();

            // Interface usage
            foreach (var vehicle in fleet)
            {
                if (vehicle is IMaintainable maintainable)
                {
                    maintainable.ScheduleMaintenance();
                }

                if (vehicle is ITrackable trackable)
                {
                    Console.WriteLine(trackable.GetLocation());
                }
            }

            Console.ReadLine();
        }
    }
}

