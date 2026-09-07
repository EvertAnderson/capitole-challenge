using System;
using System.Diagnostics.CodeAnalysis;

namespace GtMotive.Estimate.Microservice.Domain.Fleet
{
    /// <summary>
    /// Thrown when a renter tries to rent a vehicle while already renting another one.
    /// </summary>
    [ExcludeFromCodeCoverage]
    public sealed class RenterAlreadyHasActiveRentalException : DomainException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="RenterAlreadyHasActiveRentalException"/> class.
        /// </summary>
        public RenterAlreadyHasActiveRentalException()
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RenterAlreadyHasActiveRentalException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        public RenterAlreadyHasActiveRentalException(string message)
            : base(message)
        {
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="RenterAlreadyHasActiveRentalException"/> class.
        /// </summary>
        /// <param name="message">Message.</param>
        /// <param name="innerException">Inner exception.</param>
        public RenterAlreadyHasActiveRentalException(string message, Exception innerException)
            : base(message, innerException)
        {
        }
    }
}
