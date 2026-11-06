using HospitalSystem.Repositories;
using HospitalSystem.Services;
using HospitalSystem.UI;
var repository = new JsonHospitalRepository("hospitals.json");
var doctorService = new DoctorService(repository);
var menu = new Menu(repository, doctorService, repository);
menu.Run();
