using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using G2_UMA.Models;
namespace G2_UMA.Data
{
    public class dataContext : DbContext
    {

        public virtual DbSet<G2_UMA.Models.Students> Students { get; set; }

        public virtual DbSet<G2_UMA.Models.User> User { get; set; }

        public virtual DbSet<G2_UMA.Models.StudentInfo> StudentInfo { get; set; }

        public virtual DbSet<G2_UMA.Models.Fees> Fees { get; set; }

        public virtual DbSet<G2_UMA.Models.Payment> Payment { get; set; }

        public virtual DbSet<G2_UMA.Models.Months> Months { get; set; }


        public dataContext(DbContextOptions<dataContext> options)
            : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Students>(entity =>
            {
                entity.HasKey(e => e.Std_Id);
                entity.ToTable("Students");

                entity.Property(e => e.Std_Id).HasColumnName("Std_Id").ValueGeneratedNever();

                entity.Property(e => e.Name).HasColumnName("Name").IsRequired();
                entity.Property(e => e.Birth_Date).HasColumnName("Birth_Date").IsRequired();
                entity.Property(e => e.Gender).HasColumnName("Gender").IsRequired();
                entity.Property(e => e.Contact).HasColumnName("Contact").IsRequired();
                entity.Property(e => e.Address).HasColumnName("Address").IsRequired();
                entity.Property(e => e.Father_Name).HasColumnName("Father_Name").IsRequired();
                entity.Property(e => e.Mother_Name).HasColumnName("Mother_Name").IsRequired();
                entity.Property(e => e.Class).HasColumnName("Class").IsRequired();

                entity.Property(e => e.M_Id).HasColumnName("M_Id").IsRequired();


            });

            modelBuilder.Entity<Fees>(entity =>
            {
                entity.HasKey(e => e.Fee_Id);
                entity.ToTable("Fees");

                entity.Property(e => e.Fee_Id).HasColumnName("Fee_Id").ValueGeneratedNever();

                entity.Property(e => e.F_Name).HasColumnName("Name").IsRequired();


            });

            modelBuilder.Entity<Months>(entity =>
            {
                entity.HasKey(e => e.M_Id);
                entity.ToTable("Months");

                entity.Property(e => e.M_Id).HasColumnName("M_Id").ValueGeneratedNever();

                entity.Property(e => e.M_Name).HasColumnName("Name").IsRequired();


            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.ToTable("Payment");

                entity.Property(e => e.Std_Id).HasColumnName("Std_Id").ValueGeneratedNever();

                entity.Property(e => e.Fee_Id).HasColumnName("Fee_Id");

                entity.Property(e => e.Amount).HasColumnName("Amount").IsRequired();

                entity.Property(e => e.M_Id).HasColumnName("M_Id").IsRequired();

                entity.HasOne(d => d.Fees).WithMany(p => p.Payment)
                .HasForeignKey(d => d.Fee_Id).OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Fees");

                entity.HasOne(d => d.Students).WithMany(p => p.Payment)
                .HasForeignKey(d => d.Std_Id).OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Students");

                entity.HasOne(d => d.Months).WithMany(p => p.Payment)
                .HasForeignKey(d => d.M_Id).OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_Payment_Months");

            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasKey(e => e.username);
                entity.ToTable("user");

                entity.Property(e => e.username).ValueGeneratedNever();

                entity.Property(e => e.pass).IsRequired();

                entity.Property(e => e.type).IsRequired();
            });

            modelBuilder.Entity<StudentInfo>(entity =>
            {
                entity.HasKey(e => e.id);
                entity.ToTable("student_info");

                entity.Property(e => e.std_id).ValueGeneratedNever();

                entity.Property(e => e.name).IsRequired();
                entity.Property(e => e.age).IsRequired();
                entity.Property(e => e.gender).IsRequired();
                entity.Property(e => e.Class).IsRequired();
                entity.Property(e => e.paid).IsRequired();
                entity.Property(e => e.due).IsRequired();
            });

        }
    }
}
