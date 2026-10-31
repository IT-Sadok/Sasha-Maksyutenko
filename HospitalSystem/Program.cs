using HospitalSystem.Models;
using HospitalSystem.UI;
var hospitals = new List<Hospital>
{
    new Hospital
    {
        Id = 1,
        Name = "Kyiv Regional Clinical Hospital",
        Doctors = new List<Doctor>
        {
            new Doctor { Id = 1, Name = "Ivan Petrenko", Specialty = "Cardiology" },
            new Doctor { Id = 2, Name = "Olena Shevchenko", Specialty = "Neurology" },
            new Doctor { Id = 3, Name = "Andrii Kovalenko", Specialty = "Pediatrics" }
        }
    },
    new Hospital
    {
        Id = 2,
        Name = "Lviv City Hospital",
        Doctors = new List<Doctor>
        {
            new Doctor { Id = 4, Name = "Mariia Bondarenko", Specialty = "Dermatology" },
            new Doctor { Id = 5, Name = "Vasyl Melnyk", Specialty = "Orthopedics" },
            new Doctor { Id = 6, Name = "Nataliia Tkachenko", Specialty = "Ophthalmology" }
        }
    },
    new Hospital
    {
        Id = 3,
        Name = "Ivano-Frankivsk Central Hospital",
        Doctors = new List<Doctor>
        {
            new Doctor { Id = 7, Name = "Serhii Kravchenko", Specialty = "Gastroenterology" },
            new Doctor { Id = 8, Name = "Iryna Savchuk", Specialty = "Endocrinology" },
            new Doctor { Id = 9, Name = "Maksym Romaniuk", Specialty = "Urology" }
        }
    },
    new Hospital
    {
        Id = 4,
        Name = "Volyn Regional Medical Center",
        Doctors = new List<Doctor>
        {
            new Doctor { Id = 10, Name = "Yuliia Polishchuk", Specialty = "Oncology" },
            new Doctor { Id = 11, Name = "Dmytro Havryliuk", Specialty = "Radiology" },
            new Doctor { Id = 12, Name = "Tetiana Lysenko", Specialty = "Psychiatry" }
        }
    },
    new Hospital
    {
        Id = 5,
        Name = "Ternopil University Hospital",
        Doctors = new List<Doctor>
        {
            new Doctor { Id = 13, Name = "Oleksandr Boiko", Specialty = "Pulmonology" },
            new Doctor { Id = 14, Name = "Kateryna Moroz", Specialty = "Nephrology" },
            new Doctor { Id = 15, Name = "Bohdan Kravets", Specialty = "Surgery" }
        }
    }
};
var menu = new Menu(hospitals);
menu.Run();