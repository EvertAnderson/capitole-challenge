using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.CreateVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Fleet.CreateVehicle
{
    /// <summary>
    /// Builds the HTTP response for the <see cref="ICreateVehicleUseCase"/>.
    /// </summary>
    public sealed class CreateVehiclePresenter : IWebApiPresenter, ICreateVehicleOutputPort
    {
        /// <inheritdoc/>
        public IActionResult ActionResult { get; private set; }

        /// <inheritdoc/>
        public void StandardHandle(CreateVehicleOutput response)
        {
            var body = new CreateVehicleResponse(response);

            ActionResult = new CreatedAtRouteResult(
                "GetAvailableVehicles",
                routeValues: null,
                body);
        }

        /// <inheritdoc/>
        public void DuplicateLicensePlateHandle(string message)
        {
            ActionResult = new ConflictObjectResult(message);
        }
    }
}
