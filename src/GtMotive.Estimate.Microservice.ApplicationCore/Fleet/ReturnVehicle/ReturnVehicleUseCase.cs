using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Fleet;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ReturnVehicle
{
    /// <summary>
    /// Handles returning a rented vehicle.
    /// </summary>
    public sealed class ReturnVehicleUseCase(
        FleetRentalService fleetRentalService,
        IClock clock,
        IReturnVehicleOutputPort outputPort) : IReturnVehicleUseCase
    {
        private readonly FleetRentalService _fleetRentalService = fleetRentalService ?? throw new ArgumentNullException(nameof(fleetRentalService));
        private readonly IClock _clock = clock ?? throw new ArgumentNullException(nameof(clock));
        private readonly IReturnVehicleOutputPort _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));

        /// <inheritdoc/>
        public async Task Execute(ReturnVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            if (!Guid.TryParse(input.VehicleId, out var parsedVehicleId))
            {
                _outputPort.NotFoundHandle($"The vehicle {input.VehicleId} does not exist in the fleet.");
                return;
            }

            try
            {
                var vehicle = await _fleetRentalService
                    .ReturnAsync(new VehicleId(parsedVehicleId), _clock.UtcNow, CancellationToken.None)
                    .ConfigureAwait(false);

                _outputPort.StandardHandle(new ReturnVehicleOutput(vehicle.Id, vehicle.LastReturnedAtUtc!.Value));
            }
            catch (VehicleNotFoundException notFoundException)
            {
                _outputPort.NotFoundHandle(notFoundException.Message);
            }
            catch (VehicleNotRentedException notRentedException)
            {
                _outputPort.VehicleNotRentedHandle(notRentedException.Message);
            }
        }
    }
}
