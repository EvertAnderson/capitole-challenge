using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.CreateVehicle;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Fleet.CreateVehicle
{
    /// <summary>
    /// Handles a <see cref="CreateVehicleRequest"/>.
    /// </summary>
    public sealed class CreateVehicleRequestHandler(
        ICreateVehicleUseCase useCase,
        CreateVehiclePresenter presenter) : IRequestHandler<CreateVehicleRequest, IWebApiPresenter>
    {
        private readonly ICreateVehicleUseCase _useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
        private readonly CreateVehiclePresenter _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));

        /// <inheritdoc/>
        public async Task<IWebApiPresenter> Handle(CreateVehicleRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            var input = new CreateVehicleInput(request.Brand, request.Model, request.LicensePlate, request.ManufactureDate);

            await _useCase.Execute(input).ConfigureAwait(false);

            return _presenter;
        }
    }
}
