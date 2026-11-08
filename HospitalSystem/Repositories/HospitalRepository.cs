using HospitalSystem.Models;

namespace HospitalSystem.Repositories
{
    public class HospitalRepository : IHospitalRepository
    {
        private readonly List<Hospital> _hospitals;
        public HospitalRepository(List<Hospital> hospitals)
        {
            _hospitals = hospitals;
        }

        public void Add(Hospital hospital)
        {
            hospital.Id = _hospitals.Count > 0 ? _hospitals.Max(h => h.Id) + 1 : 1;
        }

        public IEnumerable<Hospital> GetAll()
        {
            return _hospitals;
        }

        public Hospital GetById(int id)
        {
            return _hospitals.FirstOrDefault(h => h.Id == id);
        }

        public void Save()
        {
            throw new NotSupportedException("In-memory repository does not support saving.");
        }
    }
}
