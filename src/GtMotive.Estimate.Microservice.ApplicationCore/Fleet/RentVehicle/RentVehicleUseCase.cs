using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Fleet;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.RentVehicle
{
    /// <summary>
    /// Handles renting a vehicle to a renter.
    /// </summary>
    public sealed class RentVehicleUseCase(
        FleetRentalService fleetRentalService,
        IClock clock,
        IRentVehicleOutputPort outputPort) : IRentVehicleUseCase
    {
        private readonly FleetRentalService _fleetRentalService = fleetRentalService ?? throw new ArgumentNullException(nameof(fleetRentalService));
        private readonly IClock _clock = clock ?? throw new ArgumentNullException(nameof(clock));
        private readonly IRentVehicleOutputPort _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));

        /// <inheritdoc/>
        public async Task Execute(RentVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            if (!Guid.TryParse(input.VehicleId, out var parsedVehicleId))
            {
                _outputPort.NotFoundHandle($"The vehicle {input.VehicleId} does not exist in the fleet.");
                return;
            }

            try
            {
                var renterId = new RenterId(input.RenterId);
                var vehicle = await _fleetRentalService
                    .RentAsync(new VehicleId(parsedVehicleId), renterId, _clock.UtcNow, CancellationToken.None)
                    .ConfigureAwait(false);

                _outputPort.StandardHandle(new RentVehicleOutput(vehicle.Id, input.RenterId, vehicle.RentedAtUtc!.Value));
            }
            catch (VehicleNotFoundException notFoundException)
            {
                _outputPort.NotFoundHandle(notFoundException.Message);
            }
            catch (VehicleNotAvailableException notAvailableException)
            {
                _outputPort.VehicleNotAvailableHandle(notAvailableException.Message);
            }
            catch (RenterAlreadyHasActiveRentalException activeRentalException)
            {
                _outputPort.RenterAlreadyHasActiveRentalHandle(activeRentalException.Message);
            }
        }
    }
}
