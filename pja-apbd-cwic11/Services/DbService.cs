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

        int newId = await _context.Prescriptions
            .Select(a => a.IdPrescription)
            .MaxAsync() + 1;
        
        await _context.Prescriptions.AddAsync(new Prescription
        {
            IdPrescription = newId,
            Date = prescription.Date,
            DueDate = prescription.DueDate,
            IdDoctor = prescription.Doctor.IdDoctor,
            IdPatient = prescription.Patient.IdPatient
        });

        foreach (var m in prescription.Medicaments)
        {
            await _context.PrescriptionMedicaments.AddAsync(new PrescriptionMedicament()
            {
                IdPrescription = newId,
                IdMedicament = m.IdMedicament,
                Dose = m.Dose,
                Details = m.Description
            });
        }
        
        //savechanges TODO

        return newId;
    }
}