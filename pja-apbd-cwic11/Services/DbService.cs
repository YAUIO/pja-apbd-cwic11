using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using pja_apbd_cwic11.Data;
using pja_apbd_cwic11.DTOs;
using pja_apbd_cwic11.Entities;
using pja_apbd_cwic11.Exceptions;

namespace pja_apbd_cwic11.Services;

public class DbService : IDbService
{
    private readonly DatabaseContext _context;

    public DbService(DatabaseContext context)
    {
        _context = context;
    }

    public async Task<int> AddNewPrescriptionAsync(PostPrescriptionDTO prescription)
    {
        if (await _context.Doctors.SingleOrDefaultAsync(a => a.IdDoctor == prescription.Doctor.IdDoctor) != null)
            throw new KeyExistsException(nameof(Doctor));

        var patient = prescription.Patient;

        if (await _context.Patients.SingleOrDefaultAsync(a =>
                a.IdPatient == patient.IdPatient &&
                a.FirstName.Equals(patient.FirstName) &&
                a.LastName.Equals(patient.LastName) &&
                a.Birthdate.Equals(patient.Birthdate)) == null)
        {
            var p =
                await _context.Patients.SingleOrDefaultAsync(a =>
                    a.IdPatient == patient.IdPatient);

            if (p != null) throw new KeyExistsException(nameof(Patient));

            await _context.Patients.AddAsync(new Patient
            {
                IdPatient = patient.IdPatient,
                FirstName = patient.FirstName,
                LastName = patient.LastName,
                Birthdate = patient.Birthdate
            });
        }

        if (prescription.Medicaments.Count > 10) throw new MaximumSizeExceededException(nameof(Medicament), 10);

        foreach (var m in prescription.Medicaments)
            if (await _context.Medicaments.SingleOrDefaultAsync(a => a.IdMedicament == m.IdMedicament) == null)
                throw new KeyNotFoundException(nameof(Medicament) + " " + m + " not found");

        if (prescription.DueDate < prescription.Date) throw new ValidationException("DueDate should be >= then Date");

        await _context.Prescriptions.AddAsync(new Prescription
        {
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdDoctor = prescription.Doctor.IdDoctor,
            IdPatient = prescription.Patient.IdPatient
        });

        await _context.SaveChangesAsync();

        var id = await _context.Prescriptions
            .Select(a => a.IdPrescription)
            .MaxAsync();

        foreach (var m in prescription.Medicaments)
            await _context.PrescriptionMedicaments.AddAsync(new PrescriptionMedicament
            {
                IdPrescription = id,
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Description
            });

        await _context.SaveChangesAsync();

        return id;
    }

    public async Task<GetPatientDTO> GetPatientByIdAsync(int id)
    {
        var patient = await _context.Patients
            .SingleOrDefaultAsync(a => a.IdPatient == id);

        if (patient == null) throw new KeyNotFoundException("No patient found with id " + id);

        var prescriptions = await _context.Prescriptions
            .Where(a => a.IdPatient == patient.IdPatient)
            .ToListAsync();

        return new GetPatientDTO()
        {
            IdPatient = patient.IdPatient,
            FirstName = patient.FirstName,
            LastName = patient.LastName,
            Birthdate = patient.Birthdate,
            Prescriptions = prescriptions.Select(a => new GetPrescriptionDTO()
            {
                IdPrescription = a.IdPrescription,
                Date = a.Date,
                DueDate = a.DueDate,
                Doctor = _context.Doctors
                    .Where(d => d.IdDoctor == a.IdDoctor)
                    .Select(d => new GetDoctorDTO(){
                            IdDoctor = d.IdDoctor,
                            FirstName = d.FirstName
                        })
                    .SingleOrDefault(),
                Medicaments = _context.PrescriptionMedicaments
                    .Where(m => m.IdPrescription == a.IdPrescription)
                    .Select(m => new
                    {
                        m.IdMedicament,
                        m.Dose,
                        m.Medicament.Description,
                        m.Medicament.Name
                    })
                    .Select(m => new GetMedicamentDTO()
                    {
                        IdMedicament = m.IdMedicament,
                        Dose = m.Dose,
                        Description = m.Description,
                        Name = m.Name
                    })
                    .ToList()
            }).ToList()
        };
    }
}