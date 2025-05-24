using System.ComponentModel.DataAnnotations;

namespace pja_apbd_cwic11.Entities;

public class Patient
{
    [Key]
    public int IdPatient { get; set; }
    
    [MaxLength(100)]
    public string FirstName { get; set; }
    
    [MaxLength(100)]
    public string LastName { get; set; }
    
    [MaxLength(100)]
    public DateOnly Birthdate { get; set; }
    
    public ICollection<Prescription> Prescriptions{ get; set; }
}