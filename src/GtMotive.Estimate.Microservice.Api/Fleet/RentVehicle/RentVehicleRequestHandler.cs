using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.RentVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Fleet.RentVehicle
{
    /// <summary>
    /// Handles a <see cref="RentVehicleRequest"/>.
    /// </summary>
    public sealed class RentVehicleRequestHandler(
        IRentVehicleUseCase useCase,
        RentVehiclePresenter presenter) : IRequestHandler<RentVehicleRequest, IWebApiPresenter>
    {
        private readonly IRentVehicleUseCase _useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
        private readonly RentVehiclePresenter _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));

        /// <inheritdoc/>
        public async Task<IWebApiPresenter> Handle(RentVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new RentVehicleInput(request.VehicleId, request.RenterId);

            await _useCase.Execute(input).ConfigureAwait(false);

            return _presenter;
        }
    }
}
