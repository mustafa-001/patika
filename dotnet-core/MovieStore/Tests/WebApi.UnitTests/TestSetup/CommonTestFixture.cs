using AutoMapper;
using Microsoft.EntityFrameworkCore;
using WebApi.Common;
using WebApi.DBOperations;

namespace TestSetup
{
    public class CommonTestFixture{
        public MovieStoreDbContext Context {get; set;}
        public IMapper Mapper {get; set;} 

        public CommonTestFixture()
        {
            var options = new DbContextOptionsBuilder<MovieStoreDbContext>().UseInMemoryDatabase(databaseName: "MovieStoreTestDb").Options;
            Context = new MovieStoreDbContext(options);
            Context.Database.EnsureCreated();
            Context.Database.EnsureDeleted();
            Context.AddDirectors();
            Context.AddActors();
            Context.SaveChanges();
            Context.AddMovies();
            Context.SaveChanges();

            Mapper = new MapperConfiguration(cfg => {cfg.AddProfile<MappingProfile>(); }).CreateMapper();
        }

    }
}