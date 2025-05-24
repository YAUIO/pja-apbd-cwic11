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
        /*//Data
        modelBuilder.Entity<Author>().HasData(new List<Author>()
        {
            new Author() { AuthorId = 1, FirstName = "Artiom", LastName = "Bezkorovainyi" },
            new Author() { AuthorId = 2, FirstName = "Anton", LastName = "Kostyn" }
        });*/
    }
}