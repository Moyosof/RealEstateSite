using Microsoft.EntityFrameworkCore;
using RealEstate.Domain.Entities.Auth;
using RealEstate.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using RealEstate.Domain.Entities.Core.AuthUser;
using RealEstate.Domain.Entities.Core;

namespace RealEstate.Data.Context
{
    public class RealEstateDbContext : DbContext
    {
        public RealEstateDbContext(DbContextOptions<RealEstateDbContext> options) : base(options)
        {
            
        }
        public DbSet<PropertyUpload> PropertyUploads { get; set; }

        public virtual DbSet<OneTimeCode> OneTimeCode { get; set; }

        public virtual DbSet<ErrorLog> ErrorLogs { get; set; }
        public virtual DbSet<RefreshToken> RefreshTokens { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
        }
    }
}
