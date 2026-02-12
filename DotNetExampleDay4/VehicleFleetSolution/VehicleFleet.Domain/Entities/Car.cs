using System;
using System.Reflection;
using VehicleFleet.Domain.Interfaces;

namespace VehicleFleet.Domain.Entities
{
    public class Car : Vehicle, IMaintainable, ITrackable
    {
        public Car(string make, string model, double fuel)
            : base(make, model, fuel) { }

        public override void Start()
        {
            Console.WriteLine($"Car {Make} {Model} started.");
        }

        void IMaintainable.ScheduleMaintenance()
        {
            Console.WriteLine($"Car {Id}: Maintenance scheduled.");
        }

        string ITrackable.GetLocation()
        {
            return "Car is at Parking Lot A";
        }
    }
}
