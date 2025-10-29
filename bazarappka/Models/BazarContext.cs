using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace bazarappka.Models
{
    public class BazarContext : DbContext
    {
        public DbSet<AutaInfo> AutaInfos { get; set; }

        public BazarContext(DbContextOptions<BazarContext> options) : base(options)
        {
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<AutaInfo>().HasIndex(x => x.LicensePlate).IsUnique();
        }
        public async Task SeedFromJsonAsync(string jsonPath)
        {
            if (AutaInfos.Any())
                return;

            var jsonData = await File.ReadAllTextAsync(jsonPath);
            var vehicles = JsonSerializer.Deserialize<List<AutaInfo>>(jsonData);

            if (vehicles != null && vehicles.Any())
            {
                AutaInfos.AddRange(vehicles);
                await SaveChangesAsync();
            }
        }
    }
}

