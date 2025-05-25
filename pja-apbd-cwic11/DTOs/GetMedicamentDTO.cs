namespace pja_apbd_cwic11.DTOs;

public class GetMedicamentDTO
{
    public int IdMedicament { set; get; }
    public string Name { set; get; }
    public int? Dose { set; get; }
    public string Description { set; get; }
}