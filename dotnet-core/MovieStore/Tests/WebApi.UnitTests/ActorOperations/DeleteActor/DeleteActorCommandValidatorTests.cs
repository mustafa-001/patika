// using System;
// using AutoMapper;
// using FluentAssertions;
// using TestSetup;
// using WebApi.ActorOperations.DeleteActor;
// using WebApi.DBOperations;
// using WebApi.Entities;
// using Xunit;

// namespace Application.ActorOperations.Commands.DeleteActor
// {
//     [Collection("NonParallelTestCollection")]
//     public class DeleteActorCommandValidatorTests : IClassFixture<CommonTestFixture>
//     {

//         [Fact]
//         public void WhenNotValidIdGiven_Validator_ShouldThrow()
//         {
//             var command = new DeleteActorCommand(null);
//             command.ActorId  = 0;
//             var result = new DeleteActorCommandValidator().Validate(command);
//             result.Errors.Count.Should().BeGreaterThan(0);
//         }

//         [Fact]
//         public void WhenValidInputIsGiven_Validator_ShallNotThrow()
//         {
//             var command = new DeleteActorCommand(null);
//             command.ActorId = 1;
//             var result = new DeleteActorCommandValidator().Validate(command);
//             result.Errors.Count.Should().Be(0);
//         }

//     }
// }