namespace pja_apbd_cwic11.DTOs;

public class PostPatientDTO
{
    public int IdPatient { set; get; }
    public string FirstName { set; get; }
    public string LastName { set; get; }
    public DateOnly Birthdate { get; set; }
}