// using System;
// using AutoMapper;
// using FluentAssertions;
// using TestSetup;
// using WebApi.ActorOperations.CreateActor;
// using WebApi.DBOperations;
// using WebApi.Entities;
// using Xunit;

// namespace Application.ActorOperations.Commands.CreateActor
// {
//     [Collection("NonParallelTestCollection")]
//     public class CreateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
//     {
//         [Theory]
//         [InlineData("Lord of The", 0, 0)]
//         [InlineData("Lord of The", 0, 1)]
//         [InlineData("", 0, 0)]
//         [InlineData("", 100, 0)]
//         [InlineData("Lor", 100, 0)]
//         [InlineData("Lord", 100, 0)]
//         [InlineData("Lor", 0, 0)]
//         public void WhenInvalidInputIsGiven_Validator_ShouldReturnErrors(string title, int pagecount, int genre)
//         {
//             CreateActorCommand command = new CreateActorCommand(null, null);
//             command.Model = new CreateActorModel()
//             {
//                 Title = title,
//                 PageCount = pagecount,
//                 PublishDate = DateTime.Now.Date.AddDays(-1),
//                 GenreId = genre
//             };
//             var validator = new CreateActorCommandValidator();
//             var result = validator.Validate(command);
//             result.Errors.Count.Should().BeGreaterThan(0);
//         }

//         [Fact]
//         public void WhenDateTimeEqualNowIsGiven_Validator_ShouldThrow()
//         {
//             var command = new CreateActorCommand(null, null);
//             command.Model = new CreateActorModel()
//             {
//                 Title = "LOTR",
//                 PageCount = 100,
//                 PublishDate = DateTime.Now.Date,
//                 GenreId = 0
//             };
//             var result = new CreateActorCommandValidator().Validate(command);
//             result.Errors.Count.Should().BeGreaterThan(0);
//         }
//         [Fact]
//         public void WhenValidInputIsGiven_Validator_ShallNotThrow()
//         {
//             var command = new CreateActorCommand(null, null);
//             command.Model = new CreateActorModel()
//             {
//                 Title = "Lord of the Rings",
//                 PageCount = 200,
//                 PublishDate = DateTime.Now.Date,
//                 GenreId = 1
//             };
//             var result = new CreateActorCommandValidator().Validate(command);
//             result.Errors.Count.Should().Be(0);
//         }

//     }
// }