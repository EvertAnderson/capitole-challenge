using System;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Api.Fleet.CreateVehicle;
using GtMotive.Estimate.Microservice.Api.Fleet.ListAvailableVehicles;
using GtMotive.Estimate.Microservice.Api.Fleet.RentVehicle;
using GtMotive.Estimate.Microservice.Api.Fleet.ReturnVehicle;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace GtMotive.Estimate.Microservice.Api.Controllers
{
    /// <summary>
    /// Exposes the fleet management operations: registering vehicles, listing available vehicles,
    /// renting a vehicle and returning it.
    /// </summary>
    [ApiController]
    [Route("api/vehicles")]
    public sealed class VehiclesController(IMediator mediator) : ControllerBase
    {
        private readonly IMediator _mediator = mediator ?? throw new ArgumentNullException(nameof(mediator));

        /// <summary>
        /// Registers a new vehicle in the fleet.
        /// </summary>
        /// <param name="request">The vehicle data.</param>
        /// <returns>The created vehicle.</returns>
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CreateVehicleRequest request)
        {
            var presenter = await _mediator.Send(request).ConfigureAwait(false);
            return presenter.ActionResult;
        }

        /// <summary>
        /// Lists every vehicle currently available to be rented.
        /// </summary>
        /// <returns>The available vehicles.</returns>
        [HttpGet(Name = "GetAvailableVehicles")]
        public async Task<IActionResult> Get()
        {
            var presenter = await _mediator.Send(new ListAvailableVehiclesRequest()).ConfigureAwait(false);
            return presenter.ActionResult;
        }

        /// <summary>
        /// Rents a vehicle to a renter.
        /// </summary>
        /// <param name="vehicleId">The identifier of the vehicle to rent.</param>
        /// <param name="body">The renter data.</param>
        /// <returns>The rented vehicle.</returns>
        [HttpPost("{vehicleId}/rent")]
        public async Task<IActionResult> Rent(string vehicleId, [FromBody] RentVehicleRequestBody body)
        {
            ArgumentNullException.ThrowIfNull(body);

            var presenter = await _mediator.Send(new RentVehicleRequest(vehicleId, body.RenterId)).ConfigureAwait(false);
            return presenter.ActionResult;
        }

        /// <summary>
        /// Returns a rented vehicle.
        /// </summary>
        /// <param name="vehicleId">The identifier of the vehicle to return.</param>
        /// <returns>The returned vehicle.</returns>
        [HttpPost("{vehicleId}/return")]
        public async Task<IActionResult> Return(string vehicleId)
        {
            var presenter = await _mediator.Send(new ReturnVehicleRequest(vehicleId)).ConfigureAwait(false);
            return presenter.ActionResult;
        }
    }
}
