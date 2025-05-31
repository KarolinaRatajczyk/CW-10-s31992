using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace CW10.Models;

public partial class ApbdContext : DbContext
{
    public ApbdContext(DbContextOptions<ApbdContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Client> Clients { get; set; }

    public virtual DbSet<ClientTrip> ClientTrips { get; set; }

    public virtual DbSet<Country> Countries { get; set; }
    
    public virtual DbSet<CountryTrip> CountryTrips { get; set; }


    public virtual DbSet<Group> Groups { get; set; }

    public virtual DbSet<Partium> Partia { get; set; }

    public virtual DbSet<Polityk> Polityks { get; set; }

    public virtual DbSet<Przynaleznosc> Przynaleznoscs { get; set; }

    public virtual DbSet<Student> Students { get; set; }

    public virtual DbSet<Trip> Trips { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Client>(entity =>
        {
            entity.HasKey(e => e.IdClient).HasName("Client_pk");

            entity.ToTable("Client");

            entity.Property(e => e.Email).HasMaxLength(120);
            entity.Property(e => e.FirstName).HasMaxLength(120);
            entity.Property(e => e.LastName).HasMaxLength(120);
            entity.Property(e => e.Pesel).HasMaxLength(120);
            entity.Property(e => e.Telephone).HasMaxLength(120);
        });

        modelBuilder.Entity<ClientTrip>(entity =>
        {
            entity.HasKey(e => new { e.IdClient, e.IdTrip }).HasName("Client_Trip_pk");

            entity.ToTable("Client_Trip");

            entity.Property(e => e.PaymentDate).HasColumnType("datetime");
            entity.Property(e => e.RegisteredAt).HasColumnType("datetime");

            entity.HasOne(d => d.Client).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.IdClient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_5_Client");

            entity.HasOne(d => d.Trip).WithMany(p => p.ClientTrips)
                .HasForeignKey(d => d.IdTrip)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Table_5_Trip");
        });

        modelBuilder.Entity<Country>(entity =>
        {
            entity.HasKey(e => e.IdCountry).HasName("Country_pk");

            entity.ToTable("Country");

            entity.Property(e => e.Name).HasMaxLength(120);
        });


        modelBuilder.Entity<Group>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Group_pk");

            entity.ToTable("Group");

            entity.Property(e => e.Name)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Partium>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Partia_pk");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.DataZalozenia).HasColumnType("datetime");
            entity.Property(e => e.Nazwa)
                .HasMaxLength(300)
                .IsUnicode(false);
            entity.Property(e => e.Skrot)
                .HasMaxLength(10)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Polityk>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Polityk_pk");

            entity.ToTable("Polityk");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Imie)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Nazwisko)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.Powiedzenie)
                .HasMaxLength(200)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Przynaleznosc>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Przynaleznosc_pk");

            entity.ToTable("Przynaleznosc");

            entity.Property(e => e.Id).HasColumnName("ID");
            entity.Property(e => e.Do).HasColumnType("datetime");
            entity.Property(e => e.Od).HasColumnType("datetime");
            entity.Property(e => e.PartiaId).HasColumnName("Partia_ID");
            entity.Property(e => e.PolitykId).HasColumnName("Polityk_ID");

            entity.HasOne(d => d.Partia).WithMany(p => p.Przynaleznoscs)
                .HasForeignKey(d => d.PartiaId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Przynaleznosc_Partia");

            entity.HasOne(d => d.Polityk).WithMany(p => p.Przynaleznoscs)
                .HasForeignKey(d => d.PolitykId)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Przynaleznosc_Polityk");
        });

        modelBuilder.Entity<Student>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("Student_pk");

            entity.ToTable("Student");

            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);

            entity.HasMany(d => d.Groups).WithMany(p => p.Students)
                .UsingEntity<Dictionary<string, object>>(
                    "GroupAssignment",
                    r => r.HasOne<Group>().WithMany()
                        .HasForeignKey("GroupId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Table_3_Group"),
                    l => l.HasOne<Student>().WithMany()
                        .HasForeignKey("StudentId")
                        .OnDelete(DeleteBehavior.ClientSetNull)
                        .HasConstraintName("Table_3_Student"),
                    j =>
                    {
                        j.HasKey("StudentId", "GroupId").HasName("GroupAssignment_pk");
                        j.ToTable("GroupAssignment");
                        j.IndexerProperty<int>("StudentId").HasColumnName("Student_Id");
                        j.IndexerProperty<int>("GroupId").HasColumnName("Group_Id");
                    });
        });

        modelBuilder.Entity<Trip>(entity =>
        {
            entity.HasKey(e => e.IdTrip).HasName("Trip_pk");

            entity.ToTable("Trip");

            entity.Property(e => e.DateFrom).HasColumnType("datetime");
            entity.Property(e => e.DateTo).HasColumnType("datetime");
            entity.Property(e => e.Description).HasMaxLength(220);
            entity.Property(e => e.Name).HasMaxLength(120);
        });

        modelBuilder.Entity<CountryTrip>(entity =>
        {
            entity.HasKey(e => new { e.IdCountry, e.IdTrip }).HasName("Country_Trip_pk");

            entity.ToTable("Country_Trip");

            entity.HasOne(d => d.Country)
                .WithMany(p => p.CountryTrips)
                .HasForeignKey(d => d.IdCountry)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Country_Trip_Country");

            entity.HasOne(d => d.Trip)
                .WithMany(p => p.CountryTrips)
                .HasForeignKey(d => d.IdTrip)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("Country_Trip_Trip");
        });


        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
