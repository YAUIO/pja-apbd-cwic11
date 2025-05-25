using pja_apbd_cwic11.DTOs;

namespace pja_apbd_cwic11.Services;

public interface IDbService
{
    public Task<int> AddNewPrescriptionAsync(PostPrescriptionDTO prescription);
    public Task<GetPatientDTO> GetPatientByIdAsync(int id);
}