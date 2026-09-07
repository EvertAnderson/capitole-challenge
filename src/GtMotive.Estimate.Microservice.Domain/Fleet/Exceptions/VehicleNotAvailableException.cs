using System;
using System.Diagnostics.CodeAnalysis;

namespace GtMotive.Estimate.Microservice.Domain.Fleet
{
    /// <summary>
    /// Thrown when attempting to rent a vehicle that is already rented.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class VehicleNotAvailableException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotAvailableException"/> class.
        /// </summary>
        public VehicleNotAvailableException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotAvailableException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public VehicleNotAvailableException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleNotAvailableException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public VehicleNotAvailableException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
