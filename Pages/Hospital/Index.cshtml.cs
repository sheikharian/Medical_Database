using ProjectWorkshop.Models;
using ProjectWorkshop.Models.PageModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Linq;

namespace ProjectWorkshop.Pages.Hospital
{
    public class IndexModel : PageModel
    {
        private readonly BuildProject2024Context _context;
        public List<HospitalModel> Hospitals;
        public int PageIndex { get; set; } //NEW LINE
        public int TotalPages { get; set; } //NEW LINE
        public bool HasPreviousPage => PageIndex > 1; //NEW LINE
        public bool HasNextPage => PageIndex < TotalPages; //NEW LINE

        //let's initialise our context
        public IndexModel(BuildProject2024Context context)
        {
            _context = context;
        }

        // OnGet method is called when page is loaded. Lets pull all hospitals from our database
        public void OnGet(int? pageIndex)
        {
            const int pageSize = 5;  //NEW LINE
            PageIndex = pageIndex ?? 1; //NEW LINE

            IQueryable<Models.Hospital> hospitalsQuery = _context.Hospitals; //NEW LINE

            int hospitalCount = hospitalsQuery.Count();
            TotalPages = (int)Math.Ceiling(hospitalCount / (double)pageSize); //NEW LINE

            Hospitals = hospitalsQuery.Select(x => new HospitalModel
            {
                Id = x.Id,
                Address = x.Address,
                City = x.City,
                HospitalName = x.HospitalName,
                HealthCareProviderCount = x.HealthCareProviders.Count,
                PatientCount = x.Patients.Count,
                Phone = x.Phone,
                State = x.State,
                Zip = x.Zip
            })
            .Skip((PageIndex - 1) * pageSize) //NEW LINE
            .Take(pageSize) //NEW LINE
            .ToList();
        }
    }
}