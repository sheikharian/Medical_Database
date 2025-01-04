using ProjectWorkshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectWorkshop.Models.PageModels;
using System.Numerics;
using System.Reflection;
using System.Runtime.Intrinsics.X86;

namespace ProjectWorkshop.Pages.Patients
{
    public class EditModel : PageModel
    {
        private readonly BuildProject2024Context _context;

        public EditModel(BuildProject2024Context context)
        {
            _context = context;
        }

        [BindProperty]
        public PatientModel Patient { get; set; }
        public SelectList Hospitals { get; set; }
        public void OnGet(string? id) // Action to displat data for existing patient or page to add new patient
        {
            if (id == null)
            {
                Patient = new PatientModel();
            }
            else
            {
                var dbPatient = _context.Patients.Find(id);

                if (dbPatient == null)
                {
                    Patient = new PatientModel();
                }
                else
                {
                    Patient = new PatientModel
                    {
                        FirstName = dbPatient.FirstName,
                        LastName = dbPatient.LastName,
                        Dob = dbPatient.Dob,
                        Gender = dbPatient.Gender,
                        HospitalId = dbPatient.HospitalId,
                        InsuranceId = dbPatient.InsuranceId,
                        Mrn = dbPatient.Mrn,
                        Ssn = dbPatient.Ssn
                    };
                }
            }

            var HospitalsList = _context.Hospitals.Select(x => new Models.Hospital { Id = x.Id, HospitalName = x.HospitalName });
            Hospitals = new SelectList(HospitalsList, "Id", "HospitalName");


        }
        public async Task<IActionResult>
    OnPost() // action that is used to save patients information to the database
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (Patient.Id == 0)
            {
                var dbPatient = new Models.Patient
                {
                    FirstName = Patient.FirstName,
                    LastName = Patient.LastName,
                    Dob = Patient.Dob,
                    Gender = Patient.Gender,
                    InsuranceId = Patient.InsuranceId,
                    HospitalId = Patient.HospitalId,
                    Mrn = Patient.Mrn,
                    Ssn = Patient.Ssn
                };
                _context.Patients.Add(dbPatient);
            }
            else
            {
                var dbPatient = _context.Patients.Find(Patient.Id);

                dbPatient.FirstName = Patient.FirstName;
                dbPatient.LastName = Patient.LastName;
                dbPatient.Dob = Patient.Dob;
                dbPatient.Gender = Patient.Gender;
                dbPatient.InsuranceId = Patient.InsuranceId;
                dbPatient.HospitalId = Patient.HospitalId;
                dbPatient.Mrn = Patient.Mrn;
                dbPatient.Ssn = Patient.Ssn;
                _context.Patients.Update(dbPatient);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index"); // Adjust the redirection as per your routing setup
        }
    }
}
