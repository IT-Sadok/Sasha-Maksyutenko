using HospitalSystem.Repositories;
using HospitalSystem.Services;
using HospitalSystem.Simulation;
using HospitalSystem.UI;
var repository = new JsonHospitalRepository("hospitals.json");
var doctorService = new DoctorService(repository);
var demo = new RaceConditionDemo();
var menu = new Menu(repository, demo, doctorService, repository);
menu.Run();
