using System;
using System.Data.Entity;
using System.Data.Entity.Migrations;
using System.Linq;

namespace FMI.WeAzure.Boxing.Database.Migrations
{

    internal sealed class Configuration : DbMigrationsConfiguration<FMI.WeAzure.Boxing.Database.BoxingDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(FMI.WeAzure.Boxing.Database.BoxingDbContext context)
        {
            // Setup some admins
            // Using only test values for demo purposes; should be generated in another way
            context.Administrators.AddOrUpdate(
                new Administrator() { Key = "AdminKey1", Active = true },
                new Administrator() { Key = "AdminKey2", Active = true },
                new Administrator() { Key = "AdminKey3", Active = false },
                new Administrator() { Key = "AdminKey4", Active = true });

            // Setup prediction result types
            var values =
                Enum.GetNames(typeof(PredictionResultEnum))
                .Zip(Enum.GetValues(typeof(PredictionResultEnum)).Cast<int>(),
                    (Name, Value) => new { Name, Value })
                .Select(a => new PredictionResult() { Id = a.Value, Description = a.Name })
                .ToArray();
            context.PredictionResults.AddOrUpdate(values);

            // Setup some boxers for test and demo purposes
            context.Boxers.AddOrUpdate(
                x => x.Name,
                new Boxer() { Name = "Dragan", Biography = "Awesome fighter" },
                new Boxer() { Name = "Petkan", Biography = "Dramatic story" },
                new Boxer() { Name = "Rocky", Biography = "You should know this" },
                new Boxer() { Name = "Ivan", Biography = "No info" }
            );

        }
    }
}
