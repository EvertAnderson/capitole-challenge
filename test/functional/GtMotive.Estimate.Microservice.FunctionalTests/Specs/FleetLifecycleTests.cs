using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.Api.Fleet.CreateVehicle;
using GtMotive.Estimate.Microservice.Api.Fleet.ListAvailableVehicles;
using GtMotive.Estimate.Microservice.Api.Fleet.RentVehicle;
using GtMotive.Estimate.Microservice.Api.Fleet.ReturnVehicle;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.FunctionalTests.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using Xunit;

namespace GtMotive.Estimate.Microservice.FunctionalTests.Specs
{
    public sealed class FleetLifecycleTests(CompositionRootTestFixture fixture) : FunctionalTestBase(fixture)
    {
        [Fact]
        public async Task RentingAndReturningAVehicle_FollowsTheFullLifecycle()
        {
            var licensePlate = $"FN{Guid.NewGuid():N}"[..8];
            var renterId = Guid.NewGuid().ToString();

            var created = await CreateVehicleAsync(licensePlate);

            IWebApiPresenter rentPresenter = null;
            await Fixture.UsingHandlerForRequestResponse<RentVehicleRequest, IWebApiPresenter>(async handler =>
            {
                rentPresenter = await handler.Handle(new RentVehicleRequest(created.VehicleId.ToString(), renterId), CancellationToken.None);
            });

            rentPresenter.ActionResult.Should().BeOfType<OkObjectResult>();

            IWebApiPresenter listPresenter = null;
            await Fixture.UsingHandlerForRequestResponse<ListAvailableVehiclesRequest, IWebApiPresenter>(async handler =>
            {
                listPresenter = await handler.Handle(new ListAvailableVehiclesRequest(), CancellationToken.None);
            });

            var listBody = (ListAvailableVehiclesResponse)((OkObjectResult)listPresenter.ActionResult).Value;
            listBody.Vehicles.Should().NotContain(vehicle => vehicle.VehicleId == created.VehicleId);

            IWebApiPresenter returnPresenter = null;
            await Fixture.UsingHandlerForRequestResponse<ReturnVehicleRequest, IWebApiPresenter>(async handler =>
            {
                returnPresenter = await handler.Handle(new ReturnVehicleRequest(created.VehicleId.ToString()), CancellationToken.None);
            });

            returnPresenter.ActionResult.Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public async Task RentingTheSameVehicleTwice_ReturnsConflict()
        {
            var licensePlate = $"FN{Guid.NewGuid():N}"[..8];
            var created = await CreateVehicleAsync(licensePlate);

            await RentVehicleAsync(created.VehicleId.ToString(), Guid.NewGuid().ToString());

            IWebApiPresenter secondRentPresenter = null;
            await Fixture.UsingHandlerForRequestResponse<RentVehicleRequest, IWebApiPresenter>(async handler =>
            {
                secondRentPresenter = await handler.Handle(
                    new RentVehicleRequest(created.VehicleId.ToString(), Guid.NewGuid().ToString()),
                    CancellationToken.None);
            });

            secondRentPresenter.ActionResult.Should().BeOfType<ConflictObjectResult>();
        }

        [Fact]
        public async Task SameRenter_CannotRentTwoVehiclesAtOnce()
        {
            var firstVehicle = await CreateVehicleAsync($"FN{Guid.NewGuid():N}"[..8]);
            var secondVehicle = await CreateVehicleAsync($"FN{Guid.NewGuid():N}"[..8]);
            var renterId = Guid.NewGuid().ToString();

            await RentVehicleAsync(firstVehicle.VehicleId.ToString(), renterId);

            IWebApiPresenter secondRentPresenter = null;
            await Fixture.UsingHandlerForRequestResponse<RentVehicleRequest, IWebApiPresenter>(async handler =>
            {
                secondRentPresenter = await handler.Handle(
                    new RentVehicleRequest(secondVehicle.VehicleId.ToString(), renterId),
                    CancellationToken.None);
            });

            secondRentPresenter.ActionResult.Should().BeOfType<ConflictObjectResult>();
        }

        private async Task<CreateVehicleResponse> CreateVehicleAsync(string licensePlate)
        {
            IWebApiPresenter createPresenter = null;
            await Fixture.UsingHandlerForRequestResponse<CreateVehicleRequest, IWebApiPresenter>(async handler =>
            {
                createPresenter = await handler.Handle(
                    new CreateVehicleRequest
                    {
                        Brand = "Toyota",
                        Model = "Corolla",
                        LicensePlate = licensePlate,
                        ManufactureDate = DateOnly.FromDateTime(DateTime.UtcNow),
                    },
                    CancellationToken.None);
            });

            var createdResult = (CreatedAtRouteResult)createPresenter.ActionResult;
            return (CreateVehicleResponse)createdResult.Value;
        }

        private async Task RentVehicleAsync(string vehicleId, string renterId)
        {
            await Fixture.UsingHandlerForRequestResponse<RentVehicleRequest, IWebApiPresenter>(async handler =>
            {
                await handler.Handle(new RentVehicleRequest(vehicleId, renterId), CancellationToken.None);
            });
        }
    }
}
