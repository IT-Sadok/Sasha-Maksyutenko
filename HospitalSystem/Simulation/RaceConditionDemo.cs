
using HospitalSystem.Models;

namespace HospitalSystem.Simulation
{
    public class RaceConditionDemo
    {
        public void RunDemo()
        {
            var apartment = new Apartment{ Id = 1, Price = 100 };
            var thread1 = new Thread(() => { for (int i = 0; i < 100000; i++) apartment.Price += 10; });
            var thread2 = new Thread(() => { for (int i = 0; i < 100000; i++) apartment.Price += 10; });
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            Console.WriteLine("=== Race Condition Demo ===");
            Console.WriteLine($"Expected Price: {100+100000*10*2}");
            Console.WriteLine($"Actual Price: {apartment.Price}");
        }
        public void RunDemoWithLock()
        {
            var apartment = new Apartment { Id = 1, Price = 100 };
            var lockObject = new object();
            var thread1 = new Thread(() => { for (int i = 0; i < 100000; i++) { lock (lockObject) { apartment.Price += 10; } } });
            var thread2 = new Thread(() => { for (int i = 0; i < 100000; i++) { lock (lockObject) { apartment.Price += 10; } } });
            thread1.Start();
            thread2.Start();
            thread1.Join();
            thread2.Join();
            Console.WriteLine("=== Race Condition Demo with Lock ===");
            Console.WriteLine($"Expected Price with lock: {100 + 100000 * 10 * 2}");
            Console.WriteLine($"Actual Price with lock: {apartment.Price}");
           Console.WriteLine("----------------------------------");
        }

    }
}
