namespace pja_apbd_cwic11.DTOs;

public class GetPrescriptionDTO
{
    public int IdPrescription { set; get; }
    public DateOnly Date { set; get; }
    public DateOnly DueDate { set; get; }
    public List<GetMedicamentDTO> Medicaments { set; get; }
    public GetDoctorDTO? Doctor { set; get; }
}