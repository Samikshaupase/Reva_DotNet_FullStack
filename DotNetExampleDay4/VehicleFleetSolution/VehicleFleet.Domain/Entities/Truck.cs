using System;
using System.Reflection;
using VehicleFleet.Domain.Interfaces;

namespace VehicleFleet.Domain.Entities
{
    public class Truck : Vehicle, IMaintainable, ITrackable
    {
        public Truck(string make, string model, double fuel)
            : base(make, model, fuel) { }

        public override void Start()
        {
            Console.WriteLine($"Truck {Make} {Model} roaring to life!");
        }

        // Normal implementation
        public void ScheduleMaintenance()
        {
            Console.WriteLine($"Truck {Id}: Heavy-duty maintenance scheduled.");
        }

        // Explicit implementation
        string ITrackable.GetLocation()
        {
            return "Truck is on Highway NH-48";
        }
    }
}

