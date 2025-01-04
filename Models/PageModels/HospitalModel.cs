namespace ProjectWorkshop.Models.PageModels
{
    public class HospitalModel
    {
        public int Id { get; set; }

        public string HospitalName { get; set; } = null!;

        public string? Address { get; set; }

        public string? City { get; set; }

        public string? State { get; set; }

        public string? Zip { get; set; }

        public string? Phone { get; set; }

        public int HealthCareProviderCount {  get; set; }

        public int PatientCount { get; set; }

    }
}
