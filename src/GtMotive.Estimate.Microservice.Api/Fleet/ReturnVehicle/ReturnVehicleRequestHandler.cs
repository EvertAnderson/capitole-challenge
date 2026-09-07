using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ReturnVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Fleet.ReturnVehicle
{
    /// <summary>
    /// Handles a <see cref="ReturnVehicleRequest"/>.
    /// </summary>
    public sealed class ReturnVehicleRequestHandler(
        IReturnVehicleUseCase useCase,
        ReturnVehiclePresenter presenter) : IRequestHandler<ReturnVehicleRequest, IWebApiPresenter>
    {
        private readonly IReturnVehicleUseCase _useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
        private readonly ReturnVehiclePresenter _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));

        /// <inheritdoc/>
        public async Task<IWebApiPresenter> Handle(ReturnVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new ReturnVehicleInput(request.VehicleId);

            await _useCase.Execute(input).ConfigureAwait(false);

            return _presenter;
        }
    }
}
