using System;
using System.Collections.Generic;

namespace ProjectWorkshop.Models;

public partial class MedicalClaim
{
    public int HealthCareProviderId { get; set; }

    public string Ndc { get; set; } = null!;

    public int? Quantity { get; set; }

    public int? TotalAmount { get; set; }

    public int? DaysSupply { get; set; }

    public string PrescriptionNumber { get; set; } = null!;

    public DateOnly? DateFilled { get; set; }

    public string PatientId { get; set; } = null!;

    public virtual HealthCareProvider HealthCareProvider { get; set; } = null!;

    public virtual Patient Patient { get; set; } = null!;
}
