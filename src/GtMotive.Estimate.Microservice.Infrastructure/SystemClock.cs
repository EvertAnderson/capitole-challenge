using System;
using GtMotive.Estimate.Microservice.Domain.Interfaces;

namespace GtMotive.Estimate.Microservice.Infrastructure
{
    /// <summary>
    /// <see cref="IClock"/> implementation backed by the system clock.
    /// </summary>
    public sealed class SystemClock : IClock
    {
        /// <inheritdoc/>
        public DateTime UtcNow => DateTime.UtcNow;
    }
}
