using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pja_apbd_cwic11.Entities;

public class Prescription
{
    [Key] public int IdPrescription { get; set; }

    public DateOnly Date { set; get; }

    public DateOnly DueDate { set; get; }

    [ForeignKey(nameof(Patient))] public int IdPatient { set; get; }

    [ForeignKey(nameof(Doctor))] public int IdDoctor { set; get; }

    public virtual Patient Patient { set; get; }

    public virtual Doctor Doctor { set; get; }
}