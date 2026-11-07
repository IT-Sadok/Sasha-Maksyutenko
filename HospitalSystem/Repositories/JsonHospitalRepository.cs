

using HospitalSystem.Models;

namespace HospitalSystem.Repositories
{
    public class JsonHospitalRepository : IHospitalRepository
    {
        private readonly string _filePath;
        private List<Hospital> _hospitals;
        public JsonHospitalRepository(string filePath)
        {
            _filePath = filePath;
            if(File.Exists(_filePath))
            {
                var json = File.ReadAllText(_filePath);
                _hospitals = System.Text.Json.JsonSerializer.Deserialize<List<Hospital>>(json) ?? new List<Hospital>();
            }
            else
            {
                _hospitals = new List<Hospital>();
            }
        }

        public void Add(Hospital hospital)
        {
            _hospitals.Add(hospital);
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
            var json = System.Text.Json.JsonSerializer.Serialize(_hospitals);
            File.WriteAllText(_filePath, json);
        }
    }
}
