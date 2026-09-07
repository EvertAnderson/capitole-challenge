using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.RentVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Fleet.RentVehicle
{
    /// <summary>
    /// Builds the HTTP response for the <see cref="IRentVehicleUseCase"/>.
    /// </summary>
    public sealed class RentVehiclePresenter : IWebApiPresenter, IRentVehicleOutputPort
    {
        /// <inheritdoc/>
        public IActionResult ActionResult { get; private set; }

        /// <inheritdoc/>
        public void StandardHandle(RentVehicleOutput response)
        {
            ActionResult = new OkObjectResult(new RentVehicleResponse(response));
        }

        /// <inheritdoc/>
        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(message);
        }

        /// <inheritdoc/>
        public void VehicleNotAvailableHandle(string message)
        {
            ActionResult = new ConflictObjectResult(message);
        }

        /// <inheritdoc/>
        public void RenterAlreadyHasActiveRentalHandle(string message)
        {
            ActionResult = new ConflictObjectResult(message);
        }
    }
}
