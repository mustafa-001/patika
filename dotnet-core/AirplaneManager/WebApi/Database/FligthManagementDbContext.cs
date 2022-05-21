using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Database
{

    public class FligthManagementDbContext : DbContext, IFligthManagementDbContext
    {
        public FligthManagementDbContext(DbContextOptions<FligthManagementDbContext> options) : base(options) { }

        public DbSet<Fligth> Fligths { get; set; }
        public DbSet<Airfield> Airfields { get; set; }
        public DbSet<Company> Companies { get; set; }
        public DbSet<PlaneModel> PlaneModels { get; set; }
        public DbSet<Pilot> Pilots { get; set; }
        public DbSet<Plane> Planes {get; set;}
    }
}