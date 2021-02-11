using CBT_WebApi.Helper;
using CBT_WebApi.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CBT_WebApi.Models
{
    public class CbtAppDbContext: DbContext
    {
        public IConfiguration configuration { get; }
        private string currentConfig, connectionString;
        public CbtAppDbContext(DbContextOptions<CbtAppDbContext> options, IConfiguration _config)
            : base(options)
        {
            configuration = _config;
            connectionString = this.GetDbConnectionString();
        }

        private string GetDbConnectionString()
        {
            string connectionString = configuration.GetSection("ConnectionConfig").GetSection("db").Value;
            return connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            try
            {
                optionsBuilder.UseSqlServer(connectionString);
            }
            catch (Exception ex)
            {
                Log.Error(Util.LogError(ex));
                //throw;
            }
        }

        public virtual DbSet<Candidate> Candidates { get; set; }
        public virtual DbSet<TestScore> TestScores { get; set; }
    }
}
