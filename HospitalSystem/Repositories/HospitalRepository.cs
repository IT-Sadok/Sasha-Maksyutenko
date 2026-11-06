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
        public IEnumerable<Hospital> GetAll()
        {
            return _hospitals;
        }

        public Hospital GetById(int id)
        {
            return _hospitals.FirstOrDefault(h => h.Id == id);
        }
    }
}
