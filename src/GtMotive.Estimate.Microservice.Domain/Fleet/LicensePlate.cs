namespace GtMotive.Estimate.Microservice.Domain.Fleet
{
    /// <summary>
    /// Value Object representing a vehicle license plate.
    /// </summary>
    public readonly struct LicensePlate
    {
        private readonly string _text;

        /// <summary>
        /// Initializes a new instance of the <see cref="LicensePlate"/> struct.
        /// </summary>
        /// <param name="value">The plate text.</param>
        public LicensePlate(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                throw new LicensePlateShouldNotBeEmptyException("The license plate should not be empty.");
            }

            _text = value.Trim().ToUpperInvariant();
        }

        /// <inheritdoc/>
        public override string ToString() => _text;
    }
}
