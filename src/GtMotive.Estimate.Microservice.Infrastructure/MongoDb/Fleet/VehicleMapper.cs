using System;
using GtMotive.Estimate.Microservice.Domain.Fleet;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Fleet
{
    /// <summary>
    /// Maps between the <see cref="Vehicle"/> domain aggregate and its <see cref="VehicleDocument"/>
    /// persistence representation.
    /// </summary>
    internal static class VehicleMapper
    {
        /// <summary>
        /// Maps a domain vehicle to its persistence document.
        /// </summary>
        /// <param name="vehicle">The vehicle to map.</param>
        /// <returns>The persistence document.</returns>
        public static VehicleDocument ToDocument(Vehicle vehicle)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            return new VehicleDocument
            {
                Id = vehicle.Id.Value,
                Brand = vehicle.Brand,
                Model = vehicle.Model,
                LicensePlate = vehicle.LicensePlate.ToString(),
                ManufactureDate = vehicle.ManufactureDate.ToDateTime(TimeOnly.MinValue),
                Status = vehicle.Status.ToString(),
                CurrentRenterId = vehicle.CurrentRenterId?.ToString(),
                RentedAtUtc = vehicle.RentedAtUtc,
                LastReturnedAtUtc = vehicle.LastReturnedAtUtc,
            };
        }

        /// <summary>
        /// Maps a persistence document to the domain vehicle it represents.
        /// </summary>
        /// <param name="document">The persistence document to map.</param>
        /// <returns>The reconstructed domain vehicle.</returns>
        public static Vehicle ToDomain(VehicleDocument document)
        {
            ArgumentNullException.ThrowIfNull(document);

            var status = Enum.Parse<VehicleStatus>(document.Status);

            var renterId = document.CurrentRenterId is null
                ? (RenterId?)null
                : new RenterId(document.CurrentRenterId);

            var rentalState = new VehicleRentalState(status, renterId, document.RentedAtUtc, document.LastReturnedAtUtc);

            return Vehicle.Restore(
                new VehicleId(document.Id),
                document.Brand,
                document.Model,
                new LicensePlate(document.LicensePlate),
                DateOnly.FromDateTime(document.ManufactureDate),
                rentalState);
        }
    }
}
