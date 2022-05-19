// using System;
// using AutoMapper;
// using FluentAssertions;
// using TestSetup;
// using WebApi.BookOperations.GetBookDetail;
// using WebApi.DBOperations;
// using WebApi.Entities;
// using Xunit;

// namespace Application.BookOperations.Commands.GetBookDetail
// {
//     [Collection("NonParallelTestCollection")]
//     public class GetMovieDetailCommandValidatorTests : IClassFixture<CommonTestFixture>
//     {

//         [Fact]
//         public void WhenNotValidIdGiven_Validator_ShouldThrow()
//         {
//             var command = new GetBookDetail(null, null);
//             command.BookId  = 0;
//             var result = new GetBookByIdQueryValidator().Validate(command);
//             result.Errors.Count.Should().BeGreaterThan(0);
//         }

//         [Fact]
//         public void WhenValidInputIsGiven_Validator_ShallNotThrow()
//         {
//             var command = new GetBookByIdQuery(null, null);
//             command.BookId = 1;
//             var result = new GetBookByIdQueryValidator().Validate(command);
//             result.Errors.Count.Should().Be(0);
//         }
//     }
// }