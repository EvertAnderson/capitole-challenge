using System;
using System.Threading;
using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.Domain.Fleet;
using Moq;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Fleet
{
    public sealed class FleetRentalServiceTests
    {
        private static readonly DateOnly Today = new(2026, 1, 1);

        [Fact]
        public async Task RentAsync_WhenRenterAlreadyHasAnActiveRental_ThrowsRenterAlreadyHasActiveRentalException()
        {
            var vehicle = Vehicle.Register(VehicleId.NewId(), "Seat", "Leon", new LicensePlate("1234ABC"), Today.AddYears(-1), Today);
            var renterId = new RenterId("12345678A");

            var repository = new Mock<IVehicleRepository>();
            repository.Setup(r => r.GetByIdAsync(vehicle.Id, It.IsAny<CancellationToken>())).ReturnsAsync(vehicle);
            repository.Setup(r => r.HasActiveRentalAsync(renterId, It.IsAny<CancellationToken>())).ReturnsAsync(true);

            var sut = new FleetRentalService(repository.Object);

            var act = () => sut.RentAsync(vehicle.Id, renterId, DateTime.UtcNow, CancellationToken.None);

            await act.Should().ThrowAsync<RenterAlreadyHasActiveRentalException>();
            repository.Verify(r => r.UpdateAsync(It.IsAny<Vehicle>(), It.IsAny<CancellationToken>()), Times.Never);
        }

        [Fact]
        public async Task RentAsync_WhenVehicleIsAvailableAndRenterHasNoActiveRental_RentsTheVehicle()
        {
            var vehicle = Vehicle.Register(VehicleId.NewId(), "Seat", "Leon", new LicensePlate("1234ABC"), Today.AddYears(-1), Today);
            var renterId = new RenterId("12345678A");

            var repository = new Mock<IVehicleRepository>();
            repository.Setup(r => r.GetByIdAsync(vehicle.Id, It.IsAny<CancellationToken>())).ReturnsAsync(vehicle);
            repository.Setup(r => r.HasActiveRentalAsync(renterId, It.IsAny<CancellationToken>())).ReturnsAsync(false);

            var sut = new FleetRentalService(repository.Object);

            var result = await sut.RentAsync(vehicle.Id, renterId, DateTime.UtcNow, CancellationToken.None);

            result.Status.Should().Be(VehicleStatus.Rented);
            repository.Verify(r => r.UpdateAsync(vehicle, It.IsAny<CancellationToken>()), Times.Once);
        }

        [Fact]
        public async Task RentAsync_WhenVehicleDoesNotExist_ThrowsVehicleNotFoundException()
        {
            var repository = new Mock<IVehicleRepository>();
            repository.Setup(r => r.GetByIdAsync(It.IsAny<VehicleId>(), It.IsAny<CancellationToken>())).ReturnsAsync((Vehicle)null);

            var sut = new FleetRentalService(repository.Object);

            var act = () => sut.RentAsync(VehicleId.NewId(), new RenterId("12345678A"), DateTime.UtcNow, CancellationToken.None);

            await act.Should().ThrowAsync<VehicleNotFoundException>();
        }
    }
}
