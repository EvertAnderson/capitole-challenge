using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using GtMotive.Estimate.Microservice.Domain.Fleet;
using GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Settings;
using Microsoft.Extensions.Options;
using MongoDB.Driver;

namespace GtMotive.Estimate.Microservice.Infrastructure.MongoDb.Fleet
{
    /// <summary>
    /// MongoDB implementation of <see cref="IVehicleRepository"/>.
    /// </summary>
    internal sealed class VehicleRepository : IVehicleRepository
    {
        private const string CollectionName = "vehicles";

        private readonly IMongoCollection<VehicleDocument> _collection;

        public VehicleRepository(MongoService mongoService, IOptions<MongoDbSettings> options)
        {
            ArgumentNullException.ThrowIfNull(mongoService);
            ArgumentNullException.ThrowIfNull(options);

            var database = mongoService.MongoClient.GetDatabase(options.Value.MongoDbDatabaseName);
            _collection = database.GetCollection<VehicleDocument>(CollectionName);
        }

        public async Task AddAsync(Vehicle vehicle, CancellationToken cancellationToken)
        {
            await _collection.InsertOneAsync(VehicleMapper.ToDocument(vehicle), options: null, cancellationToken).ConfigureAwait(false);
        }

        public async Task UpdateAsync(Vehicle vehicle, CancellationToken cancellationToken)
        {
            ArgumentNullException.ThrowIfNull(vehicle);

            var document = VehicleMapper.ToDocument(vehicle);
            await _collection.ReplaceOneAsync(x => x.Id == document.Id, document, cancellationToken: cancellationToken).ConfigureAwait(false);
        }

        public async Task<Vehicle> GetByIdAsync(VehicleId vehicleId, CancellationToken cancellationToken)
        {
            var document = await _collection
                .Find(x => x.Id == vehicleId.Value)
                .SingleOrDefaultAsync(cancellationToken)
                .ConfigureAwait(false);

            return document is null ? null : VehicleMapper.ToDomain(document);
        }

        public async Task<IReadOnlyCollection<Vehicle>> GetAvailableAsync(CancellationToken cancellationToken)
        {
            var availableStatus = VehicleStatus.Available.ToString();

            var documents = await _collection
                .Find(x => x.Status == availableStatus)
                .ToListAsync(cancellationToken)
                .ConfigureAwait(false);

            return documents.Select(VehicleMapper.ToDomain).ToList();
        }

        public async Task<bool> ExistsByLicensePlateAsync(LicensePlate licensePlate, CancellationToken cancellationToken)
        {
            var plateText = licensePlate.ToString();

            return await _collection
                .Find(x => x.LicensePlate == plateText)
                .AnyAsync(cancellationToken)
                .ConfigureAwait(false);
        }

        public async Task<bool> HasActiveRentalAsync(RenterId renterId, CancellationToken cancellationToken)
        {
            var renterIdText = renterId.ToString();
            var rentedStatus = VehicleStatus.Rented.ToString();

            return await _collection
                .Find(x => x.Status == rentedStatus && x.CurrentRenterId == renterIdText)
                .AnyAsync(cancellationToken)
                .ConfigureAwait(false);
        }
    }
}
