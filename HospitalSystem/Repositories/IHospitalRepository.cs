using HospitalSystem.Models;

namespace HospitalSystem.Repositories
{
    public interface IHospitalRepository
    {
        IEnumerable<Hospital> GetAll();
        Hospital GetById(int id);
        void Add(Hospital hospital);
        void Save();
    }
}
