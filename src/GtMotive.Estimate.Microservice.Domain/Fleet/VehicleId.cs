using System;

namespace GtMotive.Estimate.Microservice.Domain.Fleet
{
    /// <summary>
    /// Value Object that uniquely identifies a <see cref="Vehicle"/>.
    /// </summary>
    public readonly struct VehicleId
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="VehicleId"/> struct.
        /// </summary>
        /// <param name="value">The underlying identifier value.</param>
        public VehicleId(Guid value)
        {
            if (value == Guid.Empty)
            {
                throw new VehicleIdShouldNotBeEmptyException("The vehicle identifier should not be empty.");
            }

            Value = value;
        }

        /// <summary>
        /// Gets the underlying identifier value.
        /// </summary>
        public Guid Value { get; }

        /// <summary>
        /// Creates a new, unique <see cref="VehicleId"/>.
        /// </summary>
        /// <returns>A new <see cref="VehicleId"/>.</returns>
        public static VehicleId NewId() => new(Guid.NewGuid());

        /// <inheritdoc/>
        public override string ToString() => Value.ToString();
    }
}
