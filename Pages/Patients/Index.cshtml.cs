using ProjectWorkshop.Models;
using ProjectWorkshop.Models.PageModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectWorkshop.Pages.Patient
{
    public class IndexModel : PageModel
    {
        private readonly BuildProject2024Context _context;
        public List<PatientModel> Patients;

        public string HospitalName { get; set; }
        public int PageIndex { get; set; }
        public int TotalPages { get; set; }
        public bool HasPreviousPage => PageIndex > 1;
        public bool HasNextPage => PageIndex < TotalPages;

        //let's initialise our context
        public IndexModel(BuildProject2024Context context)
        {
            _context = context;
        }

        // OnGet method is called when page is loaded. Lets pull all hospitals from our database
        public void OnGet(int? hospitalId, int? pageIndex)
        {
            const int pageSize = 5;
            PageIndex = pageIndex ?? 1;

            IQueryable<Models.Patient> patientsQuery = _context.Patients;

            if (hospitalId.HasValue)
            {
                patientsQuery = patientsQuery.Where(p => p.HospitalId == hospitalId);

                HospitalName = _context.Hospitals.Find(hospitalId).HospitalName;
            }

            int totalPatients = patientsQuery.Count();
            TotalPages = (int)Math.Ceiling(totalPatients / (double)pageSize); 

            Patients = _context.Patients.Where(x => !hospitalId.HasValue || x.HospitalId == hospitalId).Select(x => new PatientModel
            {
                Gender = x.Gender,
                LastName = x.LastName,
                Mrn = x.Mrn,
                Ssn = x.Ssn,
                HospitalId = x.HospitalId,
                HospitalName = x.Hospital.HospitalName,
                Dob = x.Dob,
                FirstName = x.FirstName,
                InsuranceId = x.InsuranceId,
            })
            .Skip((PageIndex - 1) * pageSize) //NEW LINE
            .Take(pageSize) //NEW LINE    
            .ToList();
        }
    }
}