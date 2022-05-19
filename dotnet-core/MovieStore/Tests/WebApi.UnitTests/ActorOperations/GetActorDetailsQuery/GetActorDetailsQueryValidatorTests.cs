// using System;
// using AutoMapper;
// using FluentAssertions;
// using TestSetup;
// using WebApi.ActorOperations.GetActorDetail;
// using WebApi.DBOperations;
// using WebApi.Entities;
// using Xunit;

// namespace Application.ActorOperations.Commands.GetActorDetail
// {
//     [Collection("NonParallelTestCollection")]
//     public class GetMovieDetailCommandValidatorTests : IClassFixture<CommonTestFixture>
//     {

//         [Fact]
//         public void WhenNotValidIdGiven_Validator_ShouldThrow()
//         {
//             var command = new GetActorDetail(null, null);
//             command.ActorId  = 0;
//             var result = new GetActorByIdQueryValidator().Validate(command);
//             result.Errors.Count.Should().BeGreaterThan(0);
//         }

//         [Fact]
//         public void WhenValidInputIsGiven_Validator_ShallNotThrow()
//         {
//             var command = new GetActorByIdQuery(null, null);
//             command.ActorId = 1;
//             var result = new GetActorByIdQueryValidator().Validate(command);
//             result.Errors.Count.Should().Be(0);
//         }
//     }
// }