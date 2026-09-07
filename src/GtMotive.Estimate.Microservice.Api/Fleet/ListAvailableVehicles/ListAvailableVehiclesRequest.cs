using GtMotive.Estimate.Microservice.Api.UseCases;
using MediatR;

namespace GtMotive.Estimate.Microservice.Api.Fleet.ListAvailableVehicles
{
    /// <summary>
    /// HTTP request to list every vehicle currently available to be rented.
    /// </summary>
    public sealed class ListAvailableVehiclesRequest : IRequest<IWebApiPresenter>
    {
    }
}
