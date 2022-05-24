using Microsoft.EntityFrameworkCore;
using WebApi.Database;
using AutoMapper;
using WebApi.Common;

namespace TestSetup
{
    public class CommonTestFixture{
        public FligthManagementDbContext Context {get; set;}
        public IMapper Mapper {get; set;} 

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<FligthManagementDbContext>().UseInMemoryDatabase(databaseName: "FligthTestDb").EnableSensitiveDataLogging().Options;
            Context = new FligthManagementDbContext(options);
            Context.Database.EnsureCreated();
            Context.Database.EnsureDeleted();
            Context.Initialize();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => {cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }

    }
}