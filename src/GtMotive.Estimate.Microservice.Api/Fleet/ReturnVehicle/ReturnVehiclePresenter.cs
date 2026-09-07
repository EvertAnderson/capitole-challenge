using GtMotive.Estimate.Microservice.Api.UseCases;
using GtMotive.Estimate.Microservice.ApplicationCore.Fleet.ReturnVehicle;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Fleet.ReturnVehicle
{
    /// <summary>
    /// Builds the HTTP response for the <see cref="IReturnVehicleUseCase"/>.
    /// </summary>
    public sealed class ReturnVehiclePresenter : IWebApiPresenter, IReturnVehicleOutputPort
    {
        /// <inheritdoc/>
        public IActionResult ActionResult { get; private set; }

        /// <inheritdoc/>
        public void StandardHandle(ReturnVehicleOutput response)
        {
            ActionResult = new OkObjectResult(new ReturnVehicleResponse(response));
        }

        /// <inheritdoc/>
        public void NotFoundHandle(string message)
        {
            ActionResult = new NotFoundObjectResult(message);
        }

        /// <inheritdoc/>
        public void VehicleNotRentedHandle(string message)
        {
            ActionResult = new ConflictObjectResult(message);
        }
    }
}
