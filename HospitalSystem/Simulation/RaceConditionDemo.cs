
using HospitalSystem.Models;

namespace HospitalSystem.Simulation
{
    public class RaceConditionDemo
    {
        public async Task RunDemo()
        {
            var apartment = new Apartment { Id = 1, Price = 100 };

            var task1 = Task.Run(() => { for (int i = 0; i < 100000; i++) apartment.Price += 10; });
            var task2 = Task.Run(() => { for (int i = 0; i < 100000; i++) apartment.Price += 10; });

            await Task.WhenAll(task1, task2);

            Console.WriteLine("=== Race Condition Demo ===");
            Console.WriteLine($"Expected Price: {100 + 100000 * 10 * 2}");
            Console.WriteLine($"Actual Price: {apartment.Price}");
        }
        public async Task RunDemoWithLock()
        {
            var apartment = new Apartment { Id = 1, Price = 100 };
            var task1 = Task.Run(() => { for (int i = 0; i < 100000; i++) Interlocked.Add(ref apartment.Price, 10); });
            var task2 = Task.Run(() => { for (int i = 0; i < 100000; i++) Interlocked.Add(ref apartment.Price, 10); });
            await Task.WhenAll(task1, task2);
            Console.WriteLine("=== Race Condition Demo with Lock ===");
            Console.WriteLine($"Expected Price with lock: {100 + 100000 * 10 * 2}");
            Console.WriteLine($"Actual Price with lock: {apartment.Price}");
            Console.WriteLine("----------------------------------");
        }

    }
}
