// using System;
// using AutoMapper;
// using FluentAssertions;
// using TestSetup;
// using WebApi.BookOperations.CreateBook;
// using WebApi.DBOperations;
// using WebApi.Entities;
// using Xunit;

// namespace Application.BookOperations.Commands.CreateBook
// {
//     [Collection("NonParallelTestCollection")]
//     public class CreateBookCommandValidatorTests : IClassFixture<CommonTestFixture>
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
//             CreateBookCommand command = new CreateBookCommand(null, null);
//             command.Model = new CreateBookModel()
//             {
//                 Title = title,
//                 PageCount = pagecount,
//                 PublishDate = DateTime.Now.Date.AddDays(-1),
//                 GenreId = genre
//             };
//             var validator = new CreateBookCommandValidator();
//             var result = validator.Validate(command);
//             result.Errors.Count.Should().BeGreaterThan(0);
//         }

//         [Fact]
//         public void WhenDateTimeEqualNowIsGiven_Validator_ShouldThrow()
//         {
//             var command = new CreateBookCommand(null, null);
//             command.Model = new CreateBookModel()
//             {
//                 Title = "LOTR",
//                 PageCount = 100,
//                 PublishDate = DateTime.Now.Date,
//                 GenreId = 0
//             };
//             var result = new CreateBookCommandValidator().Validate(command);
//             result.Errors.Count.Should().BeGreaterThan(0);
//         }
//         [Fact]
//         public void WhenValidInputIsGiven_Validator_ShallNotThrow()
//         {
//             var command = new CreateBookCommand(null, null);
//             command.Model = new CreateBookModel()
//             {
//                 Title = "Lord of the Rings",
//                 PageCount = 200,
//                 PublishDate = DateTime.Now.Date,
//                 GenreId = 1
//             };
//             var result = new CreateBookCommandValidator().Validate(command);
//             result.Errors.Count.Should().Be(0);
//         }

//     }
// }