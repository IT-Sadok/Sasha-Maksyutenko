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
        public void AddDoctor(int hospitalId, Doctor doctor)
        {
            var hospital = _hospitalRepository.GetById(hospitalId);
            if (hospital != null)
            {
                hospital.Doctors.Add(doctor);
            }
        }

        public void RemoveDoctor(int hospitalId, int doctorId)
        {
            var hospital = _hospitalRepository.GetById(hospitalId);
            if (hospital != null)
            {
                var doctor = hospital.Doctors.FirstOrDefault(d => d.Id == doctorId);
                if (doctor != null)
                {
                    hospital.Doctors.Remove(doctor);
                }
            }
        }   

        public void UpdateDoctor(int hospitalId, Doctor doctor)
        {
            var hospital = _hospitalRepository.GetById(hospitalId);
            if (hospital != null)
            {
                var existingDoctor = hospital.Doctors.FirstOrDefault(d => d.Id == doctor.Id);
                if (existingDoctor != null)
                {
                    if (!string.IsNullOrEmpty(doctor.Name))
                        existingDoctor.Name = doctor.Name;

                    if (!string.IsNullOrEmpty(doctor.Specialty))
                        existingDoctor.Specialty = doctor.Specialty;
                }
            }
        }
    }
}
