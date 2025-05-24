using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace pja_apbd_cwic11.Entities;

public class Patient
{
    [Key] [DatabaseGenerated(DatabaseGeneratedOption.None)] public int IdPatient { get; set; }

    [MaxLength(100)] public string FirstName { get; set; }

    [MaxLength(100)] public string LastName { get; set; }

    [MaxLength(100)] public DateOnly Birthdate { get; set; }

    public virtual ICollection<Prescription> Prescriptions { get; set; }
}