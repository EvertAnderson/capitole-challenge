using System;
using System.Linq;
using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ListAvailableVehicles;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Fleet.ListAvailableVehicles
{
    /// <summary>
    /// Builds the HTTP response for the <see cref="IListAvailableVehiclesUseCase"/>.
    /// </summary>
    public sealed class ListAvailableVehiclesPresenter : IWebApiPresenter, IListAvailableVehiclesOutputPort
    {
        /// <inheritdoc/>
        public IActionResult ActionResult { get; private set; }

        /// <inheritdoc/>
        public void StandardHandle(ListAvailableVehiclesOutput response)
        {
            ArgumentNullException.ThrowIfNull(response);

            var vehicles = response.Vehicles.Select(vehicle => new AvailableVehicleResponse(vehicle)).ToList();

            ActionResult = new OkObjectResult(new ListAvailableVehiclesResponse(vehicles));
        }
    }
}
