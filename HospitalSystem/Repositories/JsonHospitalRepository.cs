

using HospitalSystem.Models;

namespace HospitalSystem.Repositories
{
    public class JsonHospitalRepository : IHospitalRepository
    {
        private readonly string _filePath;
        private List<Hospital> _hospitals;
        private JsonHospitalRepository(string filePath, List<Hospital> hospitals)
        {
            _filePath = filePath;
            _hospitals = hospitals;
        }

        public static JsonHospitalRepository Create(string filePath)
        {
            try
            {
                if (File.Exists(filePath))
                {
                    var json = File.ReadAllText(filePath);
                    var hospitals = System.Text.Json.JsonSerializer.Deserialize<List<Hospital>>(json) ?? new List<Hospital>();
                    return new JsonHospitalRepository(filePath, hospitals);
                }
                return new JsonHospitalRepository(filePath, new List<Hospital>());
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Failed to load data: {ex.Message}");
                return new JsonHospitalRepository(filePath, new List<Hospital>());
            }
        }

        public void Add(Hospital hospital)
        {
            hospital.Id = _hospitals.Count > 0 ? _hospitals.Max(h => h.Id) + 1 : 1;
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
