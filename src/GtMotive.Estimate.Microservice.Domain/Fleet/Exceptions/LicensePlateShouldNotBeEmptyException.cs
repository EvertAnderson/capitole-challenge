using System;
using System.Diagnostics.CodeAnalysis;

namespace GtMotive.Estimate.Microservice.Domain.Fleet
{
    /// <summary>
    /// Thrown when a <see cref="LicensePlate"/> is built from an empty value.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class LicensePlateShouldNotBeEmptyException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="LicensePlateShouldNotBeEmptyException"/> class.
        /// </summary>
        public LicensePlateShouldNotBeEmptyException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LicensePlateShouldNotBeEmptyException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public LicensePlateShouldNotBeEmptyException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="LicensePlateShouldNotBeEmptyException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public LicensePlateShouldNotBeEmptyException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
