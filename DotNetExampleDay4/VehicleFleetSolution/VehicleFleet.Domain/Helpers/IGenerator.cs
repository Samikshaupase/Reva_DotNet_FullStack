using System.Threading;

namespace VehicleFleet.Domain.Helpers
{
    public static class IdGenerator
    {
        private static int _currentId = 0;

        public static int GenerateId()
        {
            return Interlocked.Increment(ref _currentId);
        }
    }
}
