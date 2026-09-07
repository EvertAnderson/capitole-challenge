using System;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Fleet
{
    /// <summary>
    /// MongoDB persistence model for a vehicle. Kept separate from the domain <c>Vehicle</c>
    /// aggregate so the domain stays free of persistence concerns.
    /// </summary>
    internal sealed class VehicleDocument
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }

        public string Brand { get; set; }

        public string Model { get; set; }

        public string LicensePlate { get; set; }

        public DateTime ManufactureDate { get; set; }

        public string Status { get; set; }

        public string CurrentRenterId { get; set; }

        public DateTime? RentedAtUtc { get; set; }

        public DateTime? LastReturnedAtUtc { get; set; }
    }
}
