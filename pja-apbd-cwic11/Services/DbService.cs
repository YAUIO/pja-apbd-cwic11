using Microsoft.EntityFrameworkCore;
using pja_apbd_cwic11.Data;
using pja_apbd_cwic11.DTOs;
using pja_apbd_cwic11.Entities;

namespace pja_apbd_cwic11.Services;

public class DbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }
    
    public async Task<List<GetDoctorDTO>> GetDoctors()
    {
        var doctors = await _context.Doctors
            .Select(e => new GetDoctorDTO()
            {
                FirstName = e.FirstName,
                LastName = e.LastName,
                Email = e.Email
            }).ToListAsync();

        return doctors;
    }
}