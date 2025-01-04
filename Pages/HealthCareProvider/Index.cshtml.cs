using ProjectWorkshop.Models;
using ProjectWorkshop.Models.PageModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace ProjectWorkshop.Pages.HealthCareProvider
{
    public class IndexModel : PageModel
    {
        
        private readonly BuildProject2024Context _context;

        public List<HealthCareProviderModel> HealthCareProviders;
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
            const int pageSize = 5;  //NEW LINE
            PageIndex = pageIndex ?? 1; //NEW LINE

            IQueryable<Models.HealthCareProvider> providerQuery = _context.HealthCareProviders; //NEW LINE

            if (hospitalId.HasValue) //NEW LINE
            {
                providerQuery = providerQuery.Where(p => p.HospitalId == hospitalId); //NEW LINE
                HospitalName = _context.Hospitals.Find(hospitalId).HospitalName;
            }

            int totalProviders = providerQuery.Count(); //NEW LINE
            TotalPages = (int)Math.Ceiling(totalProviders / (double)pageSize); //NEW LINE
            HealthCareProviders = _context.HealthCareProviders.Where(x => !hospitalId.HasValue || x.HospitalId == hospitalId).Select(x => new HealthCareProviderModel
            {
                Id = x.Id,
                FirstName = x.FirstName,
                LastName = x.LastName,
                Gender = x.Gender,
                Dob = x.Dob,
                Adress = x.Adress,
                City = x.City,
                State = x.State,
                Zip = x.Zip,
                Phone = x.Phone,
                Npi = x.Npi,
                HospitalId = x.HospitalId,
                RoleId = x.RoleId
            })
            .Skip((PageIndex - 1) * pageSize) //NEW LINE
            .Take(pageSize) //NEW LINE
            .ToList();
        }
    }
}