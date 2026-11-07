

using HospitalSystem.Models;
using HospitalSystem.Repositories;
using HospitalSystem.Services;
using HospitalSystem.Simulation;

namespace HospitalSystem.UI
{
    public class Menu
    {
        private readonly IHospitalRepository _repository;
        private readonly IDoctorService _doctorService;
        private readonly RaceConditionDemo? _demo;
        public Menu(IHospitalRepository repository, RaceConditionDemo demo, IDoctorService doctorService)
        {
            _repository = repository;
            _doctorService = doctorService;
            _demo = demo;
        }

        public void PrintMenu()
        {
            Console.WriteLine("\n=== Hospital Management System ===");
            Console.WriteLine("----------------------------------");
            Console.WriteLine("1. View all hospitals");
            Console.WriteLine("2. Select hospital by ID");
            Console.WriteLine("3. Add a doctor to a hospital");
            Console.WriteLine("4. Remove a doctor from a hospital");
            Console.WriteLine("5. Update a doctor's information");
            Console.WriteLine("6. Save changes");
            Console.WriteLine("7. Add hospital");
            Console.WriteLine("8. Run race condition demo");
            Console.WriteLine("0. Exit");
            Console.WriteLine("----------------------------------");
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
                    case "6":
                        SaveChanges();
                        break;
                    case "7":
                        AddHospital();
                        break;
                    case "8":
                        StartDemo();
                        break;
                    case "0":
                        return;
                    default:
                        Console.WriteLine("Invalid choice. Please try again.");
                        break;
                }
            }
        }

        private void StartDemo()
        {
            if (_demo != null)
            {
                _demo.RunDemo();
                _demo.RunDemoWithLock();
            }
            else
            {
                Console.WriteLine("Race condition demo is not available.");
            }
        }

        private void AddHospital()
        {
            Console.Write("Enter hospital name: ");
            var name = Console.ReadLine();

            _repository.Add(new Hospital {Name = name });
            Console.WriteLine($"Hospital '{name}' added successfully.");
        }

        private void SaveChanges()
        {
            _repository.Save();
            Console.WriteLine("Changes saved successfully.");
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
            Console.WriteLine($"Doctor with ID {doctorId} updated successfully.");
        }

        private void RemoveDoctorFromHospital()
        {
            Console.WriteLine("Enter hospital ID: ");
            if (!int.TryParse(Console.ReadLine(), out int hospitalId)) return;
            Console.WriteLine("Enter doctor ID to remove: ");
            if (!int.TryParse(Console.ReadLine(), out int doctorId)) return;
            _doctorService.RemoveDoctor(hospitalId, doctorId);
            Console.WriteLine($"Doctor with ID {doctorId} removed successfully.");
        }

        private void AddDoctorToHospital()
        {
            Console.Write("Enter hospital ID: ");
            if (!int.TryParse(Console.ReadLine(), out int hospitalId)) return;
            Console.Write("Enter doctor name: ");
            var name = Console.ReadLine();
            Console.Write("Enter doctor specialty: ");
            var specialty = Console.ReadLine();

            _doctorService.AddDoctor(hospitalId, new Doctor {Name = name, Specialty = specialty });
            Console.WriteLine($"Doctor '{name}' added successfully.");
        }

        private void ViewAllHospitals()
        {
            var hospitals = _repository.GetAll().ToList();
            Console.WriteLine("List of Hospitals:");
            if (!hospitals.Any())
            {
                Console.WriteLine("No hospitals found.");
                return;
            }
            foreach (var hospital in hospitals)
            {
                Console.WriteLine($"ID: {hospital.Id}, Name: {hospital.Name}");
            }
        }
        private void SelectHospitalById()
        {
            Console.Write("Enter hospital ID: ");
            if (!int.TryParse(Console.ReadLine(), out int hospitalId)) return;

            var hospital = _repository.GetById(hospitalId);
            if (hospital == null)
            {
                Console.WriteLine("Hospital not found.");
                return;
            }

            if (!hospital.Doctors.Any())
            {
                Console.WriteLine("No doctors found.");
                return;
            }

            Console.WriteLine($"Hospital Name: {hospital.Name}");
            Console.WriteLine("Doctors:");
            foreach (var doctor in hospital.Doctors)
            {
                Console.WriteLine($"ID: {doctor.Id}, Name: {doctor.Name}, Specialty: {doctor.Specialty}");
            }
        }
    }
}