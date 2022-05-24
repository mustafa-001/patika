using System.Collections.Generic;
using TestSetup;
using WebApi.Entities;
using WebApi.FligthOperations;
using WebApi.Database;
using WebApi.Common;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using AutoMapper;
using FluentAssertions;
using Xunit;
using System;

namespace WebApi.Unittests;

[Collection("NonParallelTestCollection")]
public class UpdateFligthCommandTests : IClassFixture<CommonTestFixture>
{
    private readonly IFligthManagementDbContext _context;
    private readonly IMapper _mapper;
    public UpdateFligthCommandTests(CommonTestFixture testFixture)
    {
        _context = testFixture.Context;
        _mapper = testFixture.Mapper;
    }

    [Fact]
    public void WhenValidInputIsGiven_ThisEntity_ShouldBeUpdated()
    {
        var updateModel = new FligthViewModel
        {
            Company = "Company 3",
            PlaneId = 2,
            ArrivalAirfield = "AAA",
            DepartureAirfield = "BBB",
            ArrivalTime = System.DateTime.Today,
            DepartureTime = System.DateTime.Today,
            PilotIds = new List<int> { 2, 3 }
        };
        var fligth = new Fligth
        {
            Company = _context.Companies.Single(x => x.Name == "Company 2"),
            ArrivalAirfield = _context.Airfields.Single(x => x.Id == "ADB"),
            DepartureAirfield = _context.Airfields.Single(x => x.Id == "IST"),
            ArrivalTime = System.DateTime.Today.AddDays(1),
            DepartureTime = System.DateTime.Today.AddDays(1),
            Pilots = new List<Pilot> { _context.Pilots.Single(x => x.Id == 2), _context.Pilots.Single(x => x.Id == 3) },
            Plane = _context.Planes.Single(x => x.Id == 1)
        };
        _context.Fligths.Add(fligth);
        _context.SaveChanges();
        int newFligthId = _context.Fligths.AsNoTracking().Single(x => x.ArrivalTime == fligth.ArrivalTime).Id;

        UpdateFligthCommand command = new(_context, _mapper, updateModel, newFligthId);
        command.Handle();

        fligth = _context.Fligths.Single(x => x.Id == newFligthId);
        fligth.ArrivalAirfield.Id.Should().Be("AAA");
        fligth.DepartureAirfield.Id.Should().Be("BBB");
        fligth.Company.Name.Should().Be("Company 3");
        fligth.Pilots.Select(x => x.Id).Should().OnlyContain(x => new List<int> { 2, 3 }.Contains(x));
    }

    [Fact]
    public void WhenFligthUpdated_OldRelations_ShouldBeDeleted()
    {
        var updateModel = new FligthViewModel
        {
            PilotIds = new List<int> { 3, 4 }
        };
        var fligth = _context.Fligths.Single(x => x.Id == 1);
        var oldPilotOneId = fligth.Pilots.Skip(0).First(x => true).Id;
        var oldPilotSecondId = fligth.Pilots.Skip(1).First(x => true).Id;
        UpdateFligthCommand command = new(_context, _mapper, updateModel, fligth.Id);
        command.Handle();

        fligth = _context.Fligths.Single(x => x.Id == 1);
        _context.Pilots.Single(x => x.Id == 3).Fligths.Select(x => x.Id).Should().Contain(fligth.Id);
        _context.Pilots.Single(x => x.Id == 4).Fligths.Select(x => x.Id).Should().Contain(fligth.Id);
        _context.Pilots.Single(x => x.Id == 1).Fligths.Select(x => x.Id).Should().NotContain(fligth.Id);
        _context.Pilots.Single(x => x.Id == 2).Fligths.Select(x => x.Id).Should().NotContain(fligth.Id);
    }

    [Fact]
    public void OnlyGivenFields_ShouldBeUpdated()
    {
        // Given 
        var updateModel = new FligthViewModel
        {
            ArrivalAirfield = "AAA"
        };

        var fligth = new Fligth
        {
            Company = _context.Companies.Single(x => x.Name == "Company 2"),
            ArrivalAirfield = _context.Airfields.Single(x => x.Id == "BBB"),
            DepartureAirfield = _context.Airfields.Single(x => x.Id == "IST"),
            ArrivalTime = System.DateTime.Today.AddDays(1),
            DepartureTime = System.DateTime.Today.AddDays(1),
            Pilots = new List<Pilot> { _context.Pilots.Single(x => x.Id == 2), _context.Pilots.Single(x => x.Id == 3) },
            Plane = _context.Planes.Single(x => x.Id == 1)
        };
        _context.Fligths.Add(fligth);
        _context.SaveChanges();
        fligth = _context.Fligths.First(x => x.ArrivalAirfield.Id == "BBB");

        UpdateFligthCommand command = new(_context, _mapper, updateModel, fligth.Id);
        command.Handle();

        var fligth2 = _context.Fligths.First(x => x.Id == fligth.Id);
        fligth2.ArrivalAirfield.Id.Should().Be("AAA");
        fligth2.DepartureAirfield.Should().Be(fligth.DepartureAirfield);
        fligth2.Company.Should().Be(fligth.Company);
        fligth2.ArrivalTime.Should().Be(fligth.ArrivalTime);
        fligth2.DepartureTime.Should().Be(fligth.DepartureTime);
        fligth2.Plane.Should().Be(fligth.Plane);
        fligth2.Id.Should().Be(fligth.Id);
        fligth2.Pilots.Select(x => x.Id).Should().OnlyContain(x => fligth.Pilots.Select(x => x.Id).Contains(x));
    }
    [Fact]
    public void WhenNonExistingEntitiesReferred_Exception_ShouldBeThrown()
    {
        var updateModel = new FligthViewModel
        {
            ArrivalAirfield = "AAA"
        };

        UpdateFligthCommand command = new(_context, _mapper, updateModel, int.MaxValue);
        FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
    }
}