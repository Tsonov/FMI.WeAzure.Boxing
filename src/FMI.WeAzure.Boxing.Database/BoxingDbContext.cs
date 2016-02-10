using System;
using System.Data.Entity;
using System.Linq;

namespace FMI.WeAzure.Boxing.Database
{
    public class BoxingDbContext : DbContext
    {
        public virtual DbSet<Administrator> Administrators { get; set; }

        public virtual DbSet<Boxer> Boxers { get; set; }

        public virtual DbSet<BoxingMatch> BoxingMatches { get; set; }

        public virtual DbSet<Login> Logins { get; set; }

        public virtual DbSet<Prediction> Predictions { get; set; }

        public virtual DbSet<User> Users { get; set; }

        public virtual DbSet<PredictionResult> PredictionResults { get; set; }
    }
}