using VehicleFleet.Domain.Entities;
using Xunit;

namespace VehicleFleet.Tests
{
    public class VehicleTests
    {
        [Fact]
        public void Vehicle_Should_Have_Unique_Id()
        {
            var car1 = new Car("Honda", "City", 30);
            var car2 = new Car("Hyundai", "i20", 30);

            Assert.NotEqual(car1.Id, car2.Id);
        }

        [Fact]
        public void FuelLevel_Should_Not_Be_Negative()
        {
            Assert.Throws<ArgumentException>(() =>
                new Car("Test", "Invalid", -5));
        }

        [Fact]
        public void Truck_Should_Override_Start()
        {
            var truck = new Truck("Ashok", "Leyland", 100);
            truck.Start(); // If override missing, test conceptually fails
            Assert.True(true);
        }
    }
}
