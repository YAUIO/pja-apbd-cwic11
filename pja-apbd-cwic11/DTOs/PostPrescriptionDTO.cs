namespace pja_apbd_cwic11.DTOs;

public class PostPrescriptionDTO
{
    public PostPatientDTO Patient { set; get; }
    public PostDoctorDTO Doctor { set; get; }
    public List<PostMedicamentDTO> Medicaments { set; get; }
    public DateOnly Date { set; get; }
    public DateOnly DueDate { set; get; }
}