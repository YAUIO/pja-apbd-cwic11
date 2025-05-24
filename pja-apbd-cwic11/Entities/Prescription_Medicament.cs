using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace pja_apbd_cwic11.Entities;

[Table("Prescription_Medicament")]
[PrimaryKey(nameof(IdMedicament), nameof(IdPrescription))]
public class PrescriptionMedicament
{
    [ForeignKey(nameof(Medicament))]
    public int IdMedicament { set; get; }
    
    [ForeignKey(nameof(Prescription))]
    public int IdPrescription { set; get; }
    
    public int? Dose { set; get; }
    
    [MaxLength(100)]
    public string Details { set; get; }
    
    public Medicament Medicament { set; get; }
    
    public Prescription Prescription { set; get; }
}