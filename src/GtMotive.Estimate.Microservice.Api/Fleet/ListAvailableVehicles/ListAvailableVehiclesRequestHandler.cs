using System;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ListAvailableVehicles;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Fleet.ListAvailableVehicles
{
    /// <summary>
    /// Handles a <see cref="ListAvailableVehiclesRequest"/>.
    /// </summary>
    public sealed class ListAvailableVehiclesRequestHandler(
        IListAvailableVehiclesUseCase useCase,
        ListAvailableVehiclesPresenter presenter) : IRequestHandler<ListAvailableVehiclesRequest, IWebApiPresenter>
    {
        private readonly IListAvailableVehiclesUseCase _useCase = useCase ?? throw new ArgumentNullException(nameof(useCase));
        private readonly ListAvailableVehiclesPresenter _presenter = presenter ?? throw new ArgumentNullException(nameof(presenter));

        /// <inheritdoc/>
        public async Task<IWebApiPresenter> Handle(ListAvailableVehiclesRequest request, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(request);

            await _useCase.Execute(new ListAvailableVehiclesInput()).ConfigureAwait(false);

            return _presenter;
        }
    }
}
