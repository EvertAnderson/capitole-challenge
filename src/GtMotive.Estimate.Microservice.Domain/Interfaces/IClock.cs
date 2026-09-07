using System;

namespace GtMotive.Estimate.Microservice.Domain.Interfaces
{
    /// <summary>
    /// Abstraction over the current date and time, so use cases stay deterministic and testable.
    /// </summary>
    public interface IClock
    {
        /// <summary>
        /// Gets the current date and time, in UTC.
        /// </summary>
        DateTime UtcNow { get; }
    }
}
