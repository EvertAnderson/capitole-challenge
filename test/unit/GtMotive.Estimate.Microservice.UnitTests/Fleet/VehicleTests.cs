using System;
using FluentAssertions;
using GtMotive.Estimate.Microservice.Domain.Fleet;
using Xunit;

namespace GtMotive.Estimate.Microservice.UnitTests.Fleet
{
    public sealed class VehicleTests
    {
        private static readonly DateOnly Today = new(2026, 1, 1);

        [Fact]
        public void Register_WithValidData_CreatesAnAvailableVehicle()
        {
            var manufactureDate = Today.AddYears(-1);

            var vehicle = Vehicle.Register(VehicleId.NewId(), "Seat", "Leon", new LicensePlate("1234ABC"), manufactureDate, Today);

            vehicle.Status.Should().Be(VehicleStatus.Available);
            vehicle.CurrentRenterId.Should().BeNull();
            vehicle.Brand.Should().Be("Seat");
            vehicle.Model.Should().Be("Leon");
            vehicle.ManufactureDate.Should().Be(manufactureDate);
        }

        [Fact]
        public void Register_WithManufactureDateExactlyFiveYearsOld_CreatesAnAvailableVehicle()
        {
            var manufactureDate = Today.AddYears(-Vehicle.MaxFleetVehicleAgeInYears);

            var vehicle = Vehicle.Register(VehicleId.NewId(), "Seat", "Leon", new LicensePlate("1234ABC"), manufactureDate, Today);

            vehicle.Status.Should().Be(VehicleStatus.Available);
        }

        [Fact]
        public void Register_WithManufactureDateOlderThanFiveYears_ThrowsVehicleTooOldException()
        {
            var manufactureDate = Today.AddYears(-Vehicle.MaxFleetVehicleAgeInYears).AddDays(-1);

            var act = () => Vehicle.Register(VehicleId.NewId(), "Seat", "Leon", new LicensePlate("1234ABC"), manufactureDate, Today);

            act.Should().Throw<VehicleTooOldException>();
        }

        [Fact]
        public void Register_WithManufactureDateInTheFuture_ThrowsArgumentOutOfRangeException()
        {
            var manufactureDate = Today.AddDays(1);

            var act = () => Vehicle.Register(VehicleId.NewId(), "Seat", "Leon", new LicensePlate("1234ABC"), manufactureDate, Today);

            act.Should().Throw<ArgumentOutOfRangeException>();
        }

        [Fact]
        public void Rent_WhenAvailable_MarksTheVehicleAsRentedByTheGivenRenter()
        {
            var vehicle = Vehicle.Register(VehicleId.NewId(), "Seat", "Leon", new LicensePlate("1234ABC"), Today.AddYears(-1), Today);
            var renterId = new RenterId("12345678A");
            var rentedAt = new DateTime(2026, 1, 1, 10, 0, 0, DateTimeKind.Utc);

            vehicle.Rent(renterId, rentedAt);

            vehicle.Status.Should().Be(VehicleStatus.Rented);
            vehicle.CurrentRenterId.Should().NotBeNull();
            vehicle.RentedAtUtc.Should().Be(rentedAt);
        }

        [Fact]
        public void Rent_WhenAlreadyRented_ThrowsVehicleNotAvailableException()
        {
            var vehicle = Vehicle.Register(VehicleId.NewId(), "Seat", "Leon", new LicensePlate("1234ABC"), Today.AddYears(-1), Today);
            vehicle.Rent(new RenterId("12345678A"), DateTime.UtcNow);

            var act = () => vehicle.Rent(new RenterId("87654321B"), DateTime.UtcNow);

            act.Should().Throw<VehicleNotAvailableException>();
        }

        [Fact]
        public void Return_WhenRented_MakesTheVehicleAvailableAgain()
        {
            var vehicle = Vehicle.Register(VehicleId.NewId(), "Seat", "Leon", new LicensePlate("1234ABC"), Today.AddYears(-1), Today);
            vehicle.Rent(new RenterId("12345678A"), DateTime.UtcNow);
            var returnedAt = DateTime.UtcNow;

            vehicle.Return(returnedAt);

            vehicle.Status.Should().Be(VehicleStatus.Available);
            vehicle.CurrentRenterId.Should().BeNull();
            vehicle.RentedAtUtc.Should().BeNull();
            vehicle.LastReturnedAtUtc.Should().Be(returnedAt);
        }

        [Fact]
        public void Return_WhenNotRented_ThrowsVehicleNotRentedException()
        {
            var vehicle = Vehicle.Register(VehicleId.NewId(), "Seat", "Leon", new LicensePlate("1234ABC"), Today.AddYears(-1), Today);

            var act = () => vehicle.Return(DateTime.UtcNow);

            act.Should().Throw<VehicleNotRentedException>();
        }
    }
}
