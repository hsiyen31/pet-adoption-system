using Microsoft.EntityFrameworkCore;
using PetAdoptionSystem.Models;

namespace PetAdoptionSystem.Data
{
    public class CmsContext : DbContext
    {
        public CmsContext(DbContextOptions<CmsContext> options)
            : base(options)
        {
        }

        // User 資料表
        public DbSet<User> Users { get; set; }

        // Pet 資料表
        public DbSet<Pet> Pets { get; set; }

        // Shelter 資料表
        public DbSet<Shelter> Shelters { get; set; }

        // Adoption_Application 資料表
        public DbSet<Adoption_Application> Adoption_Applications { get; set; }
    }
}