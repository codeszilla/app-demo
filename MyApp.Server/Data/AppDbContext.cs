using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using MyApp.Server.Models;

namespace MyApp.Server.Data;

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

  //  protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=.\\SQLEXPRESS;Database=apps-demo;Trusted_Connection=True;TrustServerCertificate=True");

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) => optionsBuilder.UseSqlServer("Server=tcp:appsdevnet.database.windows.net,1433;Initial Catalog=apspdevnet;Persist Security Info=False;User ID=samakalawa;Password=ik@lawan@poKami!!;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

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
