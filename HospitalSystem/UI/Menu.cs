

using HospitalSystem.Models;

namespace HospitalSystem.UI
{
    public class Menu
    {
        private readonly List<Hospital> _hospitals;
        public Menu(List<Hospital> hospitals)
        {
            _hospitals = hospitals;
        }
        public void PrintMenu()
        {
            Console.WriteLine("Hospital Management System");
            Console.WriteLine("1. View all hospitals");
            Console.WriteLine("2. Select hospital by ID");
            Console.WriteLine("0. Exit");
        }
        public void Run()
        {
            while (true)
            {
                PrintMenu();
                Console.Write("Enter your choice: ");
                var choice = Console.ReadLine();
                switch (choice)
                {
                    case "1":
                        ViewAllHospitals();
                        break;
                    case "2":
                        SelectHospitalById();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }
        private void ViewAllHospitals()
        {
            Console.WriteLine("List of Hospitals:");
            foreach (var hospital in _hospitals)
            {
                Console.WriteLine($"ID: {hospital.Id}, Name: {hospital.Name}");
            }
        }
        private void SelectHospitalById()
        {
            Console.Write("Enter hospital ID: ");
            if (int.TryParse(Console.ReadLine(), out int hospitalId))
            {
                var hospital = _hospitals.FirstOrDefault(h => h.Id == hospitalId);
                if (hospital != null)
                {
                    Console.WriteLine($"Hospital Name: {hospital.Name}");
                    Console.WriteLine("Doctors:");
                    foreach (var doctor in hospital.Doctors)
                    {
                        Console.WriteLine($"ID: {doctor.Id}, Name: {doctor.Name}, Specialty: {doctor.Specialty}");
                    }
                }
                else
                {
                    Console.WriteLine("Hospital not found.");
                }
            }
            else
            {
                Console.WriteLine("Invalid ID. Please enter a valid number.");
            }
        }
    }
}