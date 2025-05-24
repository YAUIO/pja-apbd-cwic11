using Microsoft.EntityFrameworkCore;
using pja_apbd_cwic11.Entities;

namespace pja_apbd_cwic11.Data;

public class DatabaseContext : DbContext
{
    protected DatabaseContext()
    {
    }

    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        
    }

    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Medicament> Medicaments { get; set; }
    public DbSet<Patient> Patients {set; get;}
    public DbSet<Prescription> Prescriptions {set; get;}
    public DbSet<PrescriptionMedicament> PrescriptionMedicaments {set; get;}

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        //Data
        modelBuilder.Entity<Doctor>().HasData(new List<Doctor>()
        {
            new Doctor() { IdDoctor = 1, FirstName = "Artiom", LastName = "Elny", Email = "nbyu@gm.co" }
        });

        modelBuilder.Entity<Medicament>().HasData(new List<Medicament>()
        {
            new Medicament() { IdMedicament = 1, Name = "UnTilter", Description = "Good", Type = "Pills" }
        });

        modelBuilder.Entity<Patient>().HasData(new List<Patient>(){
            new Patient(){IdPatient = 1, FirstName = "Kirill", LastName = "9impulse", Birthdate = DateOnly.MinValue}
        });

        modelBuilder.Entity<Prescription>().HasData(new List<Prescription>()
        {
            new Prescription(){IdPrescription = 1, IdPatient = 1, IdDoctor = 1, Date = DateOnly.MinValue, DueDate = DateOnly.MaxValue}
        });

        modelBuilder.Entity<PrescriptionMedicament>().HasData(new List<PrescriptionMedicament>()
        {
            new PrescriptionMedicament(){IdMedicament = 1, IdPrescription = 1, Details = "He was sad", Dose = 2}
        });
    }
}