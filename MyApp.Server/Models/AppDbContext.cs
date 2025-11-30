using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace MyApp.Server.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<Department> Departments { get; set; }

  

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=KOG_PAYSYS_2511112;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
         modelBuilder.Entity<Department>(entity =>
        {
            entity.HasKey(e => e.DId);

            entity.ToTable("department");

            entity.Property(e => e.DId).HasColumnName("d_id");
            entity.Property(e => e.DStats).HasColumnName("d_stats");
            entity.Property(e => e.DName)
                .HasMaxLength(50)
                .HasColumnName("department");
        });
            

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
