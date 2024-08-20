using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace WebAPI.Models;

public partial class TravelUpContext : DbContext
{
    public TravelUpContext()
    {
    }

    public TravelUpContext(DbContextOptions<TravelUpContext> options)
        : base(options)
    {
    }

    public virtual DbSet<City> Cities { get; set; }

    public virtual DbSet<Passenger> Passengers { get; set; }

    public virtual DbSet<Segment> Segments { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server= DELL\\SQLEXPRESS;Database= TravelUp; User Id=sa;Password=startnew ; Encrypt=false ;Trusted_Connection=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<City>(entity =>
        {
            entity.HasKey(e => e.CityId).HasName("PK__Cities__F2D21B761589C8AA");

            entity.Property(e => e.CityName)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        modelBuilder.Entity<Passenger>(entity =>
        {
            entity.HasKey(e => e.PassengerId).HasName("PK__Passenge__88915F906DE56A8E");

            entity.Property(e => e.PassengerId).HasColumnName("PassengerID");
            entity.Property(e => e.Email)
                .HasMaxLength(100)
                .IsUnicode(false);
            entity.Property(e => e.FirstName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.LastName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.PhoneNumber)
                .HasMaxLength(20)
                .IsUnicode(false);
            entity.Property(e => e.SegmentId).HasColumnName("SegmentID");

            entity.HasOne(d => d.Segment).WithMany(p => p.Passengers)
                .HasForeignKey(d => d.SegmentId)
                .OnDelete(DeleteBehavior.Cascade)
                .HasConstraintName("FK__Passenger__Segme__5AEE82B9");
        });

        modelBuilder.Entity<Segment>(entity =>
        {
            entity.HasKey(e => e.SegmentId).HasName("PK__Segments__C680609B4A153D34");

            entity.Property(e => e.SegmentId).HasColumnName("SegmentID");
            entity.Property(e => e.ArrivalTime).HasColumnType("datetime");
            entity.Property(e => e.DepartureTime).HasColumnType("datetime");
            entity.Property(e => e.FlightNumber)
                .HasMaxLength(10)
                .IsUnicode(false);

            entity.HasOne(d => d.ArrivalCityNavigation).WithMany(p => p.SegmentArrivalCityNavigations)
                .HasForeignKey(d => d.ArrivalCity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Segments__Arriva__6754599E");

            entity.HasOne(d => d.DepartureCityNavigation).WithMany(p => p.SegmentDepartureCityNavigations)
                .HasForeignKey(d => d.DepartureCity)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK__Segments__Depart__68487DD7");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
