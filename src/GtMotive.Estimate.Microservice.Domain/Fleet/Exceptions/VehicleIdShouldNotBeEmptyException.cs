using System;
using System.Diagnostics.CodeAnalysis;

namespace GtMotive.Estimate.Microservice.Domain.Fleet
{
    /// <summary>
    /// Thrown when a <see cref="VehicleId"/> is built from an empty value.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class VehicleIdShouldNotBeEmptyException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleIdShouldNotBeEmptyException"/> class.
        /// </summary>
        public VehicleIdShouldNotBeEmptyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleIdShouldNotBeEmptyException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public VehicleIdShouldNotBeEmptyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleIdShouldNotBeEmptyException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public VehicleIdShouldNotBeEmptyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
