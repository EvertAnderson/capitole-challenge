using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Fleet;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.CreateVehicle
{
    /// <summary>
    /// Handles the registration of a new vehicle in the fleet.
    /// </summary>
    public sealed class CreateVehicleUseCase(
        IVehicleRepository vehicleRepository,
        IClock clock,
        ICreateVehicleOutputPort outputPort) : ICreateVehicleUseCase
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
        private readonly IClock _clock = clock ?? throw new ArgumentNullException(nameof(clock));
        private readonly ICreateVehicleOutputPort _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));

        /// <inheritdoc/>
        public async Task Execute(CreateVehicleInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var licensePlate = new LicensePlate(input.LicensePlate);

            if (await _vehicleRepository.ExistsByLicensePlateAsync(licensePlate, CancellationToken.None).ConfigureAwait(false))
            {
                _outputPort.DuplicateLicensePlateHandle($"A vehicle with license plate {licensePlate} is already registered in the fleet.");
                return;
            }

            var today = DateOnly.FromDateTime(_clock.UtcNow);
            var vehicle = Vehicle.Register(VehicleId.NewId(), input.Brand, input.Model, licensePlate, input.ManufactureDate, today);

            await _vehicleRepository.AddAsync(vehicle, CancellationToken.None).ConfigureAwait(false);

            var output = new CreateVehicleOutput(vehicle.Id, vehicle.Brand, vehicle.Model, vehicle.LicensePlate.ToString(), vehicle.ManufactureDate, vehicle.Status);

            _outputPort.StandardHandle(output);
        }
    }
}
