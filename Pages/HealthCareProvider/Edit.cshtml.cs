using ProjectWorkshop.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ProjectWorkshop.Models.PageModels;
using System.Numerics;
using System.Reflection;
using System.Runtime.Intrinsics.X86;

namespace ProjectWorkshop.Pages.HealthCareProvider
{
    public class EditModel : PageModel
    {
        private readonly BuildProject2024Context _context;

        public EditModel(BuildProject2024Context context)
        {
            _context = context;
        }

        [BindProperty]
        public HealthCareProviderModel HealthCareProvider { get; set; }
        public SelectList Hospitals { get; set; }
        public void OnGet(int? id)
        {
            if (id == null)
            {
                HealthCareProvider = new HealthCareProviderModel();
            }
            else
            {
                var dbHealthCareProvider = _context.HealthCareProviders.Find(id);

                if (dbHealthCareProvider == null)
                {
                    HealthCareProvider = new HealthCareProviderModel();
                }
                else
                {
                    HealthCareProvider = new HealthCareProviderModel
                    {
                        FirstName = dbHealthCareProvider.FirstName,
                        LastName = dbHealthCareProvider.LastName,
                        Dob = dbHealthCareProvider.Dob,
                        Gender = dbHealthCareProvider.Gender,
                        Id = dbHealthCareProvider.Id,
                        HospitalId = dbHealthCareProvider.HospitalId,
                        Adress = dbHealthCareProvider.Adress,
                        City = dbHealthCareProvider.City,
                        Phone = dbHealthCareProvider.Phone,
                        Npi = dbHealthCareProvider.Npi,
                        State = dbHealthCareProvider.State,
                        Zip = dbHealthCareProvider.Zip
                    };
                }
            }

            var HospitalsList = _context.Hospitals.Select(x => new Models.Hospital { Id = x.Id, HospitalName = x.HospitalName });
            Hospitals = new SelectList(HospitalsList, "Id", "HospitalName");
        }
        public async Task<IActionResult> OnPost() // action that is used to save patients information to the database
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            if (HealthCareProvider.Id == 0)
            {
                var dbHealthCareProvider = new Models.HealthCareProvider
                {
                    FirstName = HealthCareProvider.FirstName,
                    LastName = HealthCareProvider.LastName,
                    Dob = HealthCareProvider.Dob,
                    Gender = HealthCareProvider.Gender,
                    Zip = HealthCareProvider.Zip,
                    Adress = HealthCareProvider.Adress,
                    City = HealthCareProvider.City,
                    HospitalId = HealthCareProvider.HospitalId,
                    Phone = HealthCareProvider.Phone,
                    Npi = HealthCareProvider.Npi,
                    State = HealthCareProvider.State

                };
                _context.HealthCareProviders.Add(dbHealthCareProvider);
            }
            else
            {
                var dbHealthCareProvider = _context.HealthCareProviders.Find(HealthCareProvider.Id);

                dbHealthCareProvider.FirstName = HealthCareProvider.FirstName;
                dbHealthCareProvider.LastName = HealthCareProvider.LastName;
                dbHealthCareProvider.Dob = HealthCareProvider.Dob;
                dbHealthCareProvider.Gender = HealthCareProvider.Gender;
                dbHealthCareProvider.Zip = HealthCareProvider.Zip;
                dbHealthCareProvider.Adress = HealthCareProvider.Adress;
                dbHealthCareProvider.City = HealthCareProvider.City;
                dbHealthCareProvider.HospitalId = HealthCareProvider.HospitalId;
                dbHealthCareProvider.Phone = HealthCareProvider.Phone;
                dbHealthCareProvider.Npi = HealthCareProvider.Npi;
                dbHealthCareProvider.State = HealthCareProvider.State;
                _context.HealthCareProviders.Update(dbHealthCareProvider);
            }

            await _context.SaveChangesAsync();

            return RedirectToPage("./Index"); // Adjust the redirection as per your routing setup
        }
    }
}
