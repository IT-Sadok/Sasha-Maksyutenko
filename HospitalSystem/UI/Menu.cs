

using HospitalSystem.Models;
using HospitalSystem.Repositories;
using HospitalSystem.Services;

namespace HospitalSystem.UI
{
    public class Menu
    {
        private readonly IHospitalRepository _repository;
        private readonly IDoctorService _doctorService;
        public Menu(IHospitalRepository repository, IDoctorService doctorService)
        {
            _repository = repository;
            _doctorService = doctorService;
        }

        public Menu(IDoctorService doctorService)
        {
            _doctorService = doctorService;
        }

        public void PrintMenu()
        {
            Console.WriteLine("Hospital Management System");
            Console.WriteLine("1. View all hospitals");
            Console.WriteLine("2. Select hospital by ID");
            Console.WriteLine("3. Add a doctor to a hospital");
            Console.WriteLine("4. Remove a doctor from a hospital");
            Console.WriteLine("5. Update a doctor's information");
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
                    case "3":
                        AddDoctorToHospital();
                        break;
                    case "4":
                        RemoveDoctorFromHospital();
                        break;
                    case "5":
                        UpdateDoctorInformation();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void UpdateDoctorInformation()
        {
            Console.Write("Enter hospital ID: ");
            if (!int.TryParse(Console.ReadLine(), out int hospitalId)) return;
            Console.Write("Enter doctor ID to update: ");
            if (!int.TryParse(Console.ReadLine(), out int doctorId)) return;
            Console.Write("Enter new doctor name (leave empty to keep current): ");
            var name = Console.ReadLine();
            Console.Write("Enter new doctor specialty (leave empty to keep current): ");
            var specialty = Console.ReadLine();
            _doctorService.UpdateDoctor(hospitalId, new Doctor { Id = doctorId, Name = name, Specialty = specialty });
        }

        private void RemoveDoctorFromHospital()
        {
            Console.WriteLine("Enter hospital ID: ");
            if (!int.TryParse(Console.ReadLine(), out int hospitalId)) return;
            Console.WriteLine("Enter doctor ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int doctorId)) return;
            _doctorService.RemoveDoctor(hospitalId, doctorId);
        }

        private void AddDoctorToHospital()
        {
            Console.Write("Enter hospital ID: ");
            if (!int.TryParse(Console.ReadLine(), out int hospitalId)) return;

            Console.Write("Enter doctor ID: ");
            if (!int.TryParse(Console.ReadLine(), out int doctorId)) return;

            Console.Write("Enter doctor name: ");
            var name = Console.ReadLine();

            Console.Write("Enter doctor specialty: ");
            var specialty = Console.ReadLine();

            _doctorService.AddDoctor(hospitalId, new Doctor { Id = doctorId, Name = name, Specialty = specialty });
        }

        private void ViewAllHospitals()
        {
            Console.WriteLine("List of Hospitals:");
            foreach (var hospital in _repository.GetAll())
            {
                Console.WriteLine($"ID: {hospital.Id}, Name: {hospital.Name}");
            }
        }
        private void SelectHospitalById()
        {
            Console.Write("Enter hospital ID: ");
            if (int.TryParse(Console.ReadLine(), out int hospitalId))
            {
                var hospital = _repository.GetById(hospitalId);
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