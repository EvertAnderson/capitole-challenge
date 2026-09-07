namespace GtMotive.Estimate.Microservice.Domain.Fleet
{
    /// <summary>
    /// The renting status of a <see cref="Vehicle"/>.
    /// </summary>
    public enum VehicleStatus
    {
        /// <summary>
        /// The vehicle is part of the fleet and can be rented.
        /// </summary>
        Available = 0,

        /// <summary>
        /// The vehicle is currently rented and cannot be rented again until it is returned.
        /// </summary>
        Rented = 1,
    }
}
