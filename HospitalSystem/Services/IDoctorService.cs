using HospitalSystem.Models;


namespace HospitalSystem.Services
{
    public interface IDoctorService
    {
        bool AddDoctor(int hospitalId, Doctor doctor);
        bool RemoveDoctor(int hospitalId, int doctorId);
        bool UpdateDoctor(int hospitalId, Doctor doctor);
    }
}
