using Core.Security.Entities;
using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Persistance.Contexts;

public class BaseDbContext : DbContext
{
    protected IConfiguration Configuration { get; set; }
    public DbSet<Brand> Brands { get; set; }
    public DbSet<Model> Models { get; set; }
    public DbSet<User> Users { get; set; }
    public DbSet<OperationClaim> OperationClaims { get; set; }
    public DbSet<UserOperationClaim> UserOperationClaims { get; set; }
    public DbSet<RefreshToken> RefreshTokens { get; set; }

    public BaseDbContext(DbContextOptions dbContextOptions, IConfiguration configuration) : base(dbContextOptions)
    {
        Configuration = configuration;
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {

    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Brand>(b =>
        {
            b.ToTable("Brands").HasKey(k => k.Id);

            b.Property(p => p.Id).HasColumnName("Id");
            b.Property(p => p.Name).HasColumnName("Name");

            b.HasMany(p => p.Models);
        });

        modelBuilder.Entity<Model>(m =>
        {
            m.ToTable("Models").HasKey(k => k.Id);

            m.Property(p => p.Id).HasColumnName("Id");
            m.Property(p => p.BrandId).HasColumnName("BrandId");
            m.Property(p => p.Name).HasColumnName("Name");
            m.Property(p => p.DailyPrice).HasColumnName("DailyPrice");
            m.Property(p => p.ImageUrl).HasColumnName("ImageUrl");

            m.HasOne(p => p.Brand);
        });

        //modelBuilder.Entity<User>(u =>
        //{
        //    u.ToTable("Users").HasKey(k => k.Id);

        //    u.Property(p => p.Id).HasColumnName("Id");
        //    u.Property(p => p.FirstName).HasColumnName("FirstName");
        //    u.Property(p => p.LastName).HasColumnName("LastName");
        //    u.Property(p => p.Email).HasColumnName("Email");
        //    u.Property(p => p.PasswordSalt).HasColumnName("PasswordSalt");
        //    u.Property(p => p.PasswordHash).HasColumnName("PasswordHash");
        //    u.Property(p => p.Status).HasColumnName("Status");
        //    u.Property(p => p.AuthenticatorType).HasColumnName("AuthenticatorType");

        //    u.HasMany(p => p.UserOperationClaims);
        //    u.HasOne(p => p.RefreshTokens);
        //});

        //modelBuilder.Entity<OperationClaim>(o =>
        //{
        //    o.ToTable("OperationClaims").HasKey(k =>k.Id);

        //    o.Property(p => p.Id).HasColumnName("Id");
        //    o.Property(p => p.Name).HasColumnName("Name");
        //});

        //modelBuilder.Entity<UserOperationClaim>(u =>
        //{
        //    u.ToTable("UserOperationClaims").HasKey(k => k.Id);

        //    u.Property(p => p.Id).HasColumnName("Id");
        //    u.Property(p => p.UserId).HasColumnName("UserId");
        //    u.Property(p => p.OperationClaimId).HasColumnName("OperationClaimId");

        //    u.HasOne(p => p.User);
        //    u.HasOne(p => p.OperationClaim);
        //});

        //modelBuilder.Entity<RefreshToken>(r =>
        //{
        //    r.ToTable("RefreshTokens").HasKey(k => k.Id);

        //    r.Property(p => p.Id).HasColumnName("Id");
        //    r.Property(p => p.UserId).HasColumnName("UserId");
        //    r.Property(p => p.Token).HasColumnName("Token");
        //    r.Property(p => p.Expires).HasColumnName("Expires");
        //    r.Property(p => p.Created).HasColumnName("Created");
        //    r.Property(p => p.CreatedByIp).HasColumnName("CreatedByIp");
        //    r.Property(p => p.Revoked).HasColumnName("Revoked");
        //    r.Property(p => p.RevokedByIp).HasColumnName("RevokedByIp");
        //    r.Property(p => p.ReplacedByToken).HasColumnName("ReplacedByToken");
        //    r.Property(p => p.ReasonRevoked).HasColumnName("ReasonRevoked");

        //    r.HasOne(p => p.User);
        //});

        Brand[] brandEntitySeeds = {
            new(1, "BMW"),
            new(2, "Mercedes")
        };
        modelBuilder.Entity<Brand>().HasData(brandEntitySeeds);

        Model[] modelEntitySeeds = {
            new(1, 1, "Series 4", 1500, ""),
            new(2, 1, "Series 3", 1200, ""),
            new(3, 2, "A180", 1000, ""),
        };
        modelBuilder.Entity<Model>().HasData(modelEntitySeeds);
    }
}
