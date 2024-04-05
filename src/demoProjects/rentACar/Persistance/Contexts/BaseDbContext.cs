﻿using Domain.Entities;
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
