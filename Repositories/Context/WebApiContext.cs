using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using WebApi.Models;

namespace WebApi.Repositories.Context
{
    public class WebApiContext : DbContext
    {
        public DbSet<ArpenspModel> Arpensp { get; set; }
        public DbSet<CadespModel> Cadesp { get; set; }
        public DbSet<JucespModel> Jucesp { get; set; }
        public DbSet<CensecModel> Censec { get; set; }
        public DbSet<CagedModel> Caged { get; set; }
        public DbSet<DetranModel> Detran { get; set; }
        public DbSet<SielModel> Siel { get; set; }
        public DbSet<SivecModel> Sivec { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                var config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                optionsBuilder.UseSqlServer(config.GetConnectionString("MPSP"));
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
