using Microsoft.EntityFrameworkCore;
using WebApi.Entities;

namespace WebApi.Database
{
    public class DataGenerator
    {
        public static void Initialize(IServiceProvider serviceProvider)
        {
            using (var dbContext = new FligthManagementDbContext(
                serviceProvider.GetRequiredService<DbContextOptions<FligthManagementDbContext>>()))
            {

                if (dbContext.Fligths.Any())
                {
                    return;
                }
            dbContext.Airfields.AddRange(
                new Airfield{
                    Id = "ADB"
                },
                new Airfield{
                    Id  = "IST"
                }
            );
            dbContext.Companies.AddRange(
                new Company{
                    Fligths = new List<Fligth>(),
                    Name = "Company 1"
                }
            );
            dbContext.Pilots.AddRange(
                new Pilot{
                }
            );
            dbContext.Planes.AddRange(
                new Plane{
                    isWorking = true,
                    Model = null
                }
            );
            dbContext.PlaneModels.AddRange(
                new PlaneModel{
                    PilotNumber = 1,
                    PassengerNumber = 2
                }
            );
            dbContext.SaveChanges();
            dbContext.Fligths.AddRange(
                new Fligth{
                    ArrivalAirfield = dbContext.Airfields.First(x=>x.Id == "ADB"),
                    DepartureAirfield = dbContext.Airfields.First(x=>x.Id == "IST"),
                    ArrivalTime = new DateTime(2002, 10, 12),
                    DepartureTime = new DateTime(2002, 10, 12),
                    Company = dbContext.Companies.First(x=> true),
                    Pilots = new List<Pilot>(),
                    Plane = dbContext.Planes.First(x=> true)
                }
            );
            dbContext.SaveChanges();


            }
        }
    }
}