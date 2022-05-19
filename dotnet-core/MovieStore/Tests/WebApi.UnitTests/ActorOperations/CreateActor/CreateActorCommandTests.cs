using System;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using TestSetup;
using WebApi.ActorOperations.CreateActor;
using WebApi.DBOperations;
using WebApi.Entities;
using Xunit;
using System.Collections.Generic;

namespace Application.ActorOperations.Commands.CreateActor
{
    [Collection("NonParallelTestCollection")]
    public class CreateActorCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly MovieStoreDbContext _dbContext;
        private readonly IMapper _mapper;
        public CreateActorCommandTests(CommonTestFixture testFixture)
        {
            _dbContext = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void WhenAlreadyExistActorTitleIsGiven_InvalidOperationsExceptions_ShouldBeThrown()
        {
            var actor = new Actor()
            {
                Name = "Clint",
                Surname = "Eastwood",
                BirthDate = default,
                Movies = default
            };


            _dbContext.Actors.Add(actor);
            _dbContext.SaveChanges();

            var model = new CreateActorModel() { Name = actor.Name };
            var command = new CreateActorCommand(_dbContext, _mapper, model);

            FluentActions
            .Invoking(() => command.Handle())
            .Should().Throw<InvalidOperationException>().And.Message.Should().NotBeNullOrEmpty();
        }

        [Fact]
        public void WhenValidInputIsGiven_Actor_ShouldBeCreated()
        {
            var model = new CreateActorModel()
            {
                Name = "Antonio",
                Surname = "Casale",
                BirthDate = default,
                MovieIDs = default
            };


            var command = new CreateActorCommand(_dbContext, _mapper, model);


            FluentActions.Invoking(() => command.Handle()).Invoke();
            var Actor = _dbContext.Actors.SingleOrDefault(b => b.Name == model.Name);

            Actor.Should().NotBeNull();
            if (Actor is not null)
            {
                Actor.Name.Should().Be(model.Name);
                Actor.BirthDate.Should().Be(default);
            }

        }
    }
}