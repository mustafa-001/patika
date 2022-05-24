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

namespace WebApi.Unittests
{

    [Collection("NonParallelTestCollection")]
    public class DeleteFligthCommandTests : IClassFixture<CommonTestFixture>
    {
        private readonly IFligthManagementDbContext _context;
        private readonly IMapper _mapper;
        public DeleteFligthCommandTests(CommonTestFixture testFixture)
        {
            _context = testFixture.Context;
            _mapper = testFixture.Mapper;
        }

        [Fact]
        public void IfAnotherRelationTableReferences_Exception_ShouldBeThrown()
        {
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

            DeleteFligthCommand command = new(_context, newFligthId);
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }
        [Fact]
        public void WhenNonExistingEntitiesReferred_Exception_ShouldBeThrown()
        {
            DeleteFligthCommand command = new(_context, int.MaxValue);
            FluentActions.Invoking(() => command.Handle()).Should().Throw<InvalidOperationException>();
        }
    }
}