// using System;
// using AutoMapper;
// using FluentAssertions;
// using TestSetup;
// using WebApi.ActorOperations.UpdateActor;
// using WebApi.DBOperations;
// using WebApi.Entities;
// using Xunit;

// namespace Application.ActorOperations.Commands.UpdateActor
// {
//     [Collection("NonParallelTestCollection")]
//     public class UpdateActorCommandValidatorTests : IClassFixture<CommonTestFixture>
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
//             var command = new UpdateActorCommand(null, null);
//             command.Model = new UpdateActorModel()
//             {
//                 Title = title,
//                 PageCount = pagecount,
//                 PublishDate = DateTime.Now.Date.AddDays(-1),
//                 GenreId = genre
//             };
//             var validator = new UpdateActorCommandValidator();
//             var result = validator.Validate(command);
//             result.Errors.Count.Should().BeGreaterThan(0);
//         }

//         [Fact]
//         public void WhenDateTimeEqualNowIsGiven_Validator_ShouldThrow()
//         {
//             var command = new UpdateActorCommand(null, null);
//             command.Model = new UpdateActorModel()
//             {
//                 Title = "Lord of The Rings 3",
//                 PageCount = 200,
//                 PublishDate = DateTime.Now.Date,
//                 GenreId = 1
//             };
//             var result = new UpdateActorCommandValidator().Validate(command);
//             result.Errors.Count.Should().BeGreaterThan(0);
//         }
//         [Fact]
//         public void WhenValidInputIsGiven_Validator_ShallNotThrow()
//         {
//             var command = new UpdateActorCommand(null, null);
//             command.Model = new UpdateActorModel()
//             {
//                 Title = "Lord of the Rings",
//                 PageCount = 300,
//                 PublishDate = DateTime.Now.AddDays(-1).Date,
//                 GenreId = 1
//             };
//             var result = new UpdateActorCommandValidator().Validate(command);
//             result.Errors.Count.Should().Be(0);
//         }

//     }
// }