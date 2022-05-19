// using System;
// using AutoMapper;
// using FluentAssertions;
// using TestSetup;
// using WebApi.BookOperations.DeleteBook;
// using WebApi.DBOperations;
// using WebApi.Entities;
// using Xunit;

// namespace Application.BookOperations.Commands.DeleteBook
// {
//     [Collection("NonParallelTestCollection")]
//     public class DeleteBookCommandValidatorTests : IClassFixture<CommonTestFixture>
//     {

//         [Fact]
//         public void WhenNotValidIdGiven_Validator_ShouldThrow()
//         {
//             var command = new DeleteBookCommand(null);
//             command.BookId  = 0;
//             var result = new DeleteBookCommandValidator().Validate(command);
//             result.Errors.Count.Should().BeGreaterThan(0);
//         }

//         [Fact]
//         public void WhenValidInputIsGiven_Validator_ShallNotThrow()
//         {
//             var command = new DeleteBookCommand(null);
//             command.BookId = 1;
//             var result = new DeleteBookCommandValidator().Validate(command);
//             result.Errors.Count.Should().Be(0);
//         }

//     }
// }