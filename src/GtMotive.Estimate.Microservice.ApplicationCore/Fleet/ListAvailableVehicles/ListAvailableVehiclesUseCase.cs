using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Fleet;

namespace GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ListAvailableVehicles
{
    /// <summary>
    /// Handles listing every vehicle currently available to be rented.
    /// </summary>
    public sealed class ListAvailableVehiclesUseCase(
        IVehicleRepository vehicleRepository,
        IListAvailableVehiclesOutputPort outputPort) : IListAvailableVehiclesUseCase
    {
        private readonly IVehicleRepository _vehicleRepository = vehicleRepository ?? throw new ArgumentNullException(nameof(vehicleRepository));
        private readonly IListAvailableVehiclesOutputPort _outputPort = outputPort ?? throw new ArgumentNullException(nameof(outputPort));

        /// <inheritdoc/>
        public async Task Execute(ListAvailableVehiclesInput input)
        {
            ArgumentNullException.ThrowIfNull(input);

            var availableVehicles = await _vehicleRepository.GetAvailableAsync(CancellationToken.None).ConfigureAwait(false);

            var vehicles = availableVehicles
                .Select(vehicle => new AvailableVehicle(vehicle.Id, vehicle.Brand, vehicle.Model, vehicle.LicensePlate.ToString(), vehicle.ManufactureDate))
                .ToList();

            _outputPort.StandardHandle(new ListAvailableVehiclesOutput(vehicles));
        }
    }
}
