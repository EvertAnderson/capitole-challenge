using System;
using System.Diagnostics.CodeAnalysis;

namespace GtMotive.Estimate.Microservice.Domain.Fleet
{
    /// <summary>
    /// Thrown when trying to register a vehicle whose license plate already exists in the fleet.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class DuplicateLicensePlateException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateLicensePlateException"/> class.
        /// </summary>
        public DuplicateLicensePlateException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateLicensePlateException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public DuplicateLicensePlateException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="DuplicateLicensePlateException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public DuplicateLicensePlateException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
