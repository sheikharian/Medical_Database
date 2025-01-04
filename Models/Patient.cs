using System;
using System.Collections.Generic;

namespace ProjectWorkshop.Models;

public partial class Patient
{
    public string FirstName { get; set; } = null!;

    public string LastName { get; set; } = null!;

    public DateOnly? Dob { get; set; }

    public string? Gender { get; set; }

    public string? InsuranceId { get; set; }

    public int HospitalId { get; set; }

    public int? Ssn { get; set; }

    public string Mrn { get; set; } = null!;

    public virtual Hospital Hospital { get; set; } = null!;

    public virtual ICollection<MedicalClaim> MedicalClaims { get; set; } = new List<MedicalClaim>();
}
