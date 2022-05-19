using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.DirectorOperations.UpdateDirector;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using System.Collections.Generic;

namespace Application.DirectorOperations.Commands.UpdateDirector
{

    [Collection("NonParallelTestCollection")]
    public class UpdateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _context;
        private readonly IMapper _mapper;
        public UpdateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenDoesNotExistDirectorIdIsGiven_InvalidOperationsExceptions_ShouldBeThrown()
        {
            var model = new UpdateDirectorModel()
            {
                Name = "Eli",
                Surname = "Wallach",
                BirthDate = "1950, 12, 12",
                MovieIDs = new List<int>(),
            };
            var command = new UpdateDirectorCommand(_context, _mapper, model, int.MaxValue);

            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();
        }
        [Fact]
        public void WhenAMovieAddedToDirector_NewRelationship_ShouldBeEstablishedAtBothEnds(){
            var director = _context.Directors.Single(d => d.Name == "Sergio");
            var movie = _context.Movies.Single(m => m.Name == "The Lord of the Rings");
            var oldMovie = director.Movies.FirstOrDefault(x => true);
            var directorModel = new UpdateDirectorModel{
                Name = "Sergio",
                MovieIDs = new List<int>{movie.Id}
            };

            var command = new UpdateDirectorCommand(_context, _mapper, directorModel, director.Id);
            command.Handle();
            movie.DirectorId.Should().Be(director.Id);
            director.Movies.Select(x => x.Name).Should().Contain("The Lord of the Rings");
            director.Movies.Count.Should().Be(1);
        }

        [Fact]
        public void WhenAMovieAddedToDirector_OldRelationShip_ShouldBeREmovedAtBothEnds(){
            var director = _context.Directors.First(x => true);
            var oldMovie = director.Movies.First(x => true);
            var movie = _context.Movies.First(m => m.Name != oldMovie.Name);

            var directorModel = new UpdateDirectorModel{
                MovieIDs = new List<int>{movie.Id}
            };

            var command = new UpdateDirectorCommand(_context, _mapper, directorModel, director.Id);
            command.Handle();
            movie.DirectorId.Should().Be(director.Id);
            oldMovie.Director.Should().Be(null);
            director.Movies.Select(x => x.Name).Should().NotContain(oldMovie.Name);
            director.Movies.Count.Should().Be(1);
        }

        [Fact]
        public void WhenValidInputIsGiven_Director_ShouldBeUpdated()
        {
            var model = new UpdateDirectorModel()
            {
                Name = "Peter 2",
                Surname = "Jackson",
                BirthDate = "12/12/1950",
                MovieIDs = new List<int>(),
            };
            int id = _context.Directors.SingleOrDefault(x => x.Name == "Peter").Id;
            var command = new UpdateDirectorCommand(_context, _mapper, model, id );


            FluentActions.Invoking(() => command.Handle()).Invoke();
            var director = _context.Directors.SingleOrDefault(b => b.Id == id);

            director.Should().NotBeNull();
            if (director is not null)
            {
                director.Name.ToString().Should().Be(model.Name);
                director.BirthDate.ToShortDateString().Should().Be(model.BirthDate);
            }

        }
    }
}