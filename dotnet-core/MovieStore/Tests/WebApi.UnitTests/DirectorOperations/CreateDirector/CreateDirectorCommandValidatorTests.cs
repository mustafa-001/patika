// using System;
// using AutoMapper;
// using FluentAssertions;
// using TestSetup;
// using WebApi.DirectorOperations.CreateDirector;
// using WebApi.DBOperations;
// using WebApi.Entities;
// using Xunit;

// namespace Application.DirectorOperations.Commands.CreateDirector
// {
//     [Collection("NonParallelTestCollection")]
//     public class CreateDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
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
//             CreateDirectorCommand command = new CreateDirectorCommand(null, null);
//             command.Model = new CreateDirectorModel()
//             {
//                 Title = title,
//                 PageCount = pagecount,
//                 PublishDate = DateTime.Now.Date.AddDays(-1),
//                 GenreId = genre
//             };
//             var validator = new CreateDirectorCommandValidator();
//             var result = validator.Validate(command);
//             result.Errors.Count.Should().BeGreaterThan(0);
//         }

//         [Fact]
//         public void WhenDateTimeEqualNowIsGiven_Validator_ShouldThrow()
//         {
//             var command = new CreateDirectorCommand(null, null);
//             command.Model = new CreateDirectorModel()
//             {
//                 Title = "LOTR",
//                 PageCount = 100,
//                 PublishDate = DateTime.Now.Date,
//                 GenreId = 0
//             };
//             var result = new CreateDirectorCommandValidator().Validate(command);
//             result.Errors.Count.Should().BeGreaterThan(0);
//         }
//         [Fact]
//         public void WhenValidInputIsGiven_Validator_ShallNotThrow()
//         {
//             var command = new CreateDirectorCommand(null, null);
//             command.Model = new CreateDirectorModel()
//             {
//                 Title = "Lord of the Rings",
//                 PageCount = 200,
//                 PublishDate = DateTime.Now.Date,
//                 GenreId = 1
//             };
//             var result = new CreateDirectorCommandValidator().Validate(command);
//             result.Errors.Count.Should().Be(0);
//         }

//     }
// }