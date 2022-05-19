// using System;
// using AutoMapper;
// using FluentAssertions;
// using TestSetup;
// using WebApi.DirectorOperations.GetDirectorDetail;
// using WebApi.DBOperations;
// using WebApi.Entities;
// using Xunit;

// namespace Application.DirectorOperations.Commands.GetDirectorDetail
// {
//     [Collection("NonParallelTestCollection")]
//     public class GetMovieDetailCommandValidatorTests : IClassFixture<CommonTestFixture>
//     {

//         [Fact]
//         public void WhenNotValidIdGiven_Validator_ShouldThrow()
//         {
//             var command = new GetDirectorDetail(null, null);
//             command.DirectorId  = 0;
//             var result = new GetDirectorByIdQueryValidator().Validate(command);
//             result.Errors.Count.Should().BeGreaterThan(0);
//         }

//         [Fact]
//         public void WhenValidInputIsGiven_Validator_ShallNotThrow()
//         {
//             var command = new GetDirectorByIdQuery(null, null);
//             command.DirectorId = 1;
//             var result = new GetDirectorByIdQueryValidator().Validate(command);
//             result.Errors.Count.Should().Be(0);
//         }
//     }
// }