using System;

namespace GtMotive.Estimate.Microservice.Domain.Fleet
{
    /// <summary>
    /// Value Object that identifies the person renting a <see cref="Vehicle"/>.
    /// </summary>
    public readonly struct RenterId
    {
        private readonly string _text;

        /// <summary>
        /// Initializes a new instance of the <see cref="RenterId"/> struct.
        /// </summary>
        /// <param name="value">The renter identifier, for example a national identity document number.</param>
        public RenterId(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new RenterIdShouldNotBeEmptyException("The renter identifier should not be empty.");
            }

            _text = value.Trim();
        }

        /// <summary>
        /// Determines whether two <see cref="RenterId"/> instances identify the same renter.
        /// </summary>
        /// <param name="other">The other renter identifier.</param>
        /// <returns><see langword="true"/> when both identifiers refer to the same renter.</returns>
        public bool IsSameAs(RenterId other) => string.Equals(_text, other._text, StringComparison.OrdinalIgnoreCase);

        /// <inheritdoc/>
        public override string ToString() => _text;
    }
}
