using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using WebApi.Entities;

namespace WebApi.Database
{
    public interface  IFligthManagementDbContext
    {
        DbSet<Fligth> Fligths {get; set;}
        DbSet<Airfield> Airfields {get; set;}
        DbSet<Company> Companies {get; set;}
        DbSet<PlaneModel> PlaneModels {get; set;}
        DbSet<Pilot> Pilots  {get; set;}
        DbSet<Plane> Planes {get; set;}
        int SaveChanges();
        EntityEntry<T> Attach<T>(T entity) where T: class;
    }
}
