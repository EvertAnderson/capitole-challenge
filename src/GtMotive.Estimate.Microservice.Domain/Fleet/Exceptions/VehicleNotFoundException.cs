using System;
using System.Diagnostics.CodeAnalysis;

namespace GtMotive.Estimate.Microservice.Domain.Fleet
{
    /// <summary>
    /// Thrown when a requested vehicle does not exist in the fleet.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class VehicleNotFoundException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotFoundException"/> class.
        /// </summary>
        public VehicleNotFoundException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotFoundException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public VehicleNotFoundException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotFoundException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public VehicleNotFoundException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
