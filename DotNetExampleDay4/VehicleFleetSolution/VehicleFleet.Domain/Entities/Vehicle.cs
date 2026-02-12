using System;
using VehicleFleet.Domain.Helpers;

namespace VehicleFleet.Domain.Entities
{
    public abstract class Vehicle
    {
        public int Id { get; }
        public string Make { get; }
        public string Model { get; }

        private double _fuelLevel;

        public double FuelLevel
        {
            get => _fuelLevel;
            protected set
            {
                if (value < 0)
                    throw new ArgumentException("Fuel level cannot be below 0");
                _fuelLevel = value;
            }
        }

        protected Vehicle(string make, string model, double fuelLevel)
        {
            Id = IdGenerator.GenerateId();
            Make = make;
            Model = model;
            FuelLevel = fuelLevel;
        }

        public abstract void Start();

        public virtual void Stop()
        {
            Console.WriteLine($"{Make} {Model} stopped.");
        }
    }
}
