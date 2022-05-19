using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.DirectorOperations.CreateDirector;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using System.Collections.Generic;

namespace Application.DirectorOperations.Commands.CreateDirector
{
    [Collection("NonParallelTestCollection")]
    public class CreateDirectorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateDirectorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistDirectorNameIsGiven_InvalidOperationsExceptions_ShouldBeThrown()
        {
            var Director = new Director()
            {
                Name = "Sergio",
                Surname = "Leone",
                BirthDate = default,
                Movies = default
            };


            _dbContext.Directors.Add(Director);
            _dbContext.SaveChanges();

            var model = new CreateDirectorModel() { Name = Director.Name };
            var command = new CreateDirectorCommand(_dbContext, _mapper, model);

            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void WhenValidInputIsGiven_Director_ShouldBeCreated()
        {
            var model = new CreateDirectorModel()
            {
                Name = "Antonio",
                Surname = "Casale",
                BirthDate = default,
                MovieIDs = default
            };


            var command = new CreateDirectorCommand(_dbContext, _mapper, model);


            FluentActions.Invoking(() => command.Handle()).Invoke();
            var Director = _dbContext.Directors.SingleOrDefault(b => b.Name == model.Name);

            Director.Should().NotBeNull();
            if (Director is not null)
            {
                Director.Name.Should().Be(model.Name);
                Director.BirthDate.Should().Be(default);
            }

        }
    }
}