using System;
using System.Diagnostics.CodeAnalysis;

namespace GtMotive.Estimate.Microservice.Domain.Fleet
{
    /// <summary>
    /// Thrown when a <see cref="RenterId"/> is built from an empty value.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class RenterIdShouldNotBeEmptyException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenterIdShouldNotBeEmptyException"/> class.
        /// </summary>
        public RenterIdShouldNotBeEmptyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RenterIdShouldNotBeEmptyException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public RenterIdShouldNotBeEmptyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RenterIdShouldNotBeEmptyException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public RenterIdShouldNotBeEmptyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
