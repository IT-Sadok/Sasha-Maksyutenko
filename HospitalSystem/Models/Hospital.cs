using System;

/// <summary>
/// Summary description for Class1
/// </summary>
namespace HospitalSystem.Models
{
    public class Hospital
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public List<Doctor> Doctors { get; set; } = new List<Doctor>();
    }
}
