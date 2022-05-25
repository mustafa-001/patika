using TestSetup;
using WebApi.PilotOperations;
using WebApi.Database;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Xunit;
using System;
using WebApi.Entities;

namespace WebApi.Unittests;

[Collection("NonParallelTestCollection")]
public class DeletePilotCommandTests: IClassFixture<CommonTestFixture>
{
    private readonly IFligthManagementDbContext _context;
    private readonly IMapper _mapper;

    public DeletePilotCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;

    }
    [Fact]
    public void WhenValidInputIsGiven_ThisEntity_ShouldBeDeleted()
    {
        var pilot = _context.Pilots.Add(new Pilot{});
        var pId = pilot.Entity.Id;
        _context.SaveChanges();
        var command = new DeletePilotCommand(_context, pId);
        command.Handle();
        _context.Pilots.Select(x=> x.Id).Should().NotContain(pId);
    }

    [Fact]
    public void WhenPilotHasReferencingFligthEntity_Exception_ShouldBeThrown()
    {
        var pilot = _context.Fligths.First(x=> true).Pilots.First(x=> true);
        DeletePilotCommand command  = new(_context, pilot.Id);
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
    }
}
