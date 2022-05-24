using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using WebApi.Database;
using WebApi.Entities;

namespace TestSetup
{
    public static class DataGenerator
    {
        public static void Initialize(this IFligthManagementDbContext dbContext)
        {

            if (dbContext.Fligths.Any())
            {
                return;
            }
            dbContext.Airfields.AddRange(
                new Airfield
                {
                    Id = "ADB"
                },
                new Airfield
                {
                    Id = "IST"
                },
                new Airfield
                {
                    Id = "AAA"
                },
                new Airfield
                {
                    Id = "BBB"
                }
            );
            dbContext.Companies.AddRange(
                new Company
                {
                    Fligths = new List<Fligth>(),
                    Name = "Company 1"
                },
                new Company
                {
                    Fligths = new List<Fligth>(),
                    Name = "Company 2"
                },
                new Company
                {
                    Fligths = new List<Fligth>(),
                    Name = "Company 3"
                }
            );
            dbContext.Pilots.AddRange(
                new Pilot
                {
                },
                new Pilot
                {
                },
                new Pilot
                {
                },
                new Pilot
                {
                },
                new Pilot
                {
                }
            );
            dbContext.PlaneModels.AddRange(
                new PlaneModel
                {
                    PilotNumber = 1,
                    PassengerNumber = 2
                },
                new PlaneModel
                {
                    PilotNumber = 2,
                    PassengerNumber = 20
                },
                new PlaneModel
                {
                    PilotNumber = 3,
                    PassengerNumber = 50
                }

            );
            dbContext.SaveChanges();
            dbContext.Planes.AddRange(
                new Plane
                {
                    isWorking = true,
                    Model = dbContext.PlaneModels.First(x => true)
                },
                new Plane
                {
                    isWorking = false,
                    Model = dbContext.PlaneModels.First(x => x.Id == 3)
                },
                new Plane
                {
                    isWorking = true,
                    Model = dbContext.PlaneModels.First(x => x.Id == 2)
                },
                new Plane
                {
                    isWorking = true,
                    Model = dbContext.PlaneModels.First(x => x.Id == 3)
                }
            );
            dbContext.SaveChanges();
            dbContext.Fligths.AddRange(
                new Fligth
                {
                    ArrivalAirfield = dbContext.Airfields.First(x => x.Id == "ADB"),
                    DepartureAirfield = dbContext.Airfields.First(x => x.Id == "IST"),
                    ArrivalTime = new DateTime(2002, 10, 12),
                    DepartureTime = new DateTime(2002, 10, 12),
                    Company = dbContext.Companies.First(x => true),
                    Pilots = new List<Pilot>(),
                    Plane = dbContext.Planes.First(x => true)
                }
            );
            dbContext.SaveChanges();
            dbContext.Fligths.First(x => true).Pilots.Add(dbContext.Pilots.First(x => x.Id == 1));
            dbContext.Fligths.First(x => true).Pilots.Add(dbContext.Pilots.First(x => x.Id == 2));
            dbContext.SaveChanges();
        }
    }
}
