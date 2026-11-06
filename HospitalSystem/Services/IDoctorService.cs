using HospitalSystem.Models;


namespace HospitalSystem.Services
{
    public interface IDoctorService
    {
        void AddDoctor(int hospitalId, Doctor doctor);
        void RemoveDoctor(int hospitalId, int doctorId);
        void UpdateDoctor(int hospitalId, Doctor doctor);
    }
}
