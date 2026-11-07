using HospitalSystem.Models;
using HospitalSystem.Repositories;

namespace HospitalSystem.Services
{
    public class DoctorService : IDoctorService
    {
        private readonly IHospitalRepository _hospitalRepository;
        public DoctorService(IHospitalRepository hospitalRepository)
        {
            _hospitalRepository = hospitalRepository;
        }
        public bool AddDoctor(int hospitalId, Doctor doctor)
        {
            var hospital = _hospitalRepository.GetById(hospitalId);
            if (hospital == null) return false;
                doctor.Id = hospital.Doctors.Count > 0 ? hospital.Doctors.Max(d => d.Id) + 1 : 1;
                hospital.Doctors.Add(doctor);
                return true;
        }

        public bool RemoveDoctor(int hospitalId, int doctorId)
        {
            var hospital = _hospitalRepository.GetById(hospitalId);
            if (hospital == null) return false;
            var doctor = hospital.Doctors.FirstOrDefault(d => d.Id == doctorId);
            if (doctor == null) return false;
            hospital.Doctors.Remove(doctor);
            return true;
        }

        public bool UpdateDoctor(int hospitalId, Doctor doctor)
        {
            var hospital = _hospitalRepository.GetById(hospitalId);
            if (hospital == null) return false;
            var existingDoctor = hospital.Doctors.FirstOrDefault(d => d.Id == doctor.Id);
            if (existingDoctor == null) return false;
            if (!string.IsNullOrEmpty(doctor.Name))
                existingDoctor.Name = doctor.Name;
            if (!string.IsNullOrEmpty(doctor.Specialty))
                existingDoctor.Specialty = doctor.Specialty;
            return true;
        }
    }
}
