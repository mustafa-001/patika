// using System;
// using AutoMapper;
// using FluentAssertions;
// using TestSetup;
// using WebApi.DirectorOperations.DeleteDirector;
// using WebApi.DBOperations;
// using WebApi.Entities;
// using Xunit;

// namespace Application.DirectorOperations.Commands.DeleteDirector
// {
//     [Collection("NonParallelTestCollection")]
//     public class DeleteDirectorCommandValidatorTests : IClassFixture<CommonTestFixture>
//     {

//         [Fact]
//         public void WhenNotValidIdGiven_Validator_ShouldThrow()
//         {
//             var command = new DeleteDirectorCommand(null);
//             command.DirectorId  = 0;
//             var result = new DeleteDirectorCommandValidator().Validate(command);
//             result.Errors.Count.Should().BeGreaterThan(0);
//         }

//         [Fact]
//         public void WhenValidInputIsGiven_Validator_ShallNotThrow()
//         {
//             var command = new DeleteDirectorCommand(null);
//             command.DirectorId = 1;
//             var result = new DeleteDirectorCommandValidator().Validate(command);
//             result.Errors.Count.Should().Be(0);
//         }

//     }
// }