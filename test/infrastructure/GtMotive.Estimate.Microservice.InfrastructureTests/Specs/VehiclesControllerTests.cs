using System;
using System.Net;
using System.Net.Http.Json;
using System.Text.Json;
using System.Threading.Tasks;
using FluentAssertions;
using GtMotive.Estimate.Microservice.Api.Fleet.CreateVehicle;
using GtMotive.Estimate.Microservice.InfrastructureTests.Infrastructure;
using Xunit;

namespace GtMotive.Estimate.Microservice.InfrastructureTests.Specs
{
    public sealed class VehiclesControllerTests(GenericInfrastructureTestServerFixture fixture) : InfrastructureTestBase(fixture)
    {
        [Fact]
        public async Task Post_WithAValidVehicle_ReturnsCreatedWithTheNewVehicle()
        {
            using var client = Fixture.Server.CreateClient();

            var request = new CreateVehicleRequest
            {
                Brand = "Seat",
                Model = "Leon",
                LicensePlate = $"IT{Guid.NewGuid():N}"[..8],
                ManufactureDate = DateOnly.FromDateTime(DateTime.UtcNow),
            };

            using var response = await client.PostAsJsonAsync("/api/vehicles", request);

            response.StatusCode.Should().Be(HttpStatusCode.Created);

            using var body = JsonDocument.Parse(await response.Content.ReadAsStringAsync());
            body.RootElement.TryGetProperty("brand", out var brand).Should().BeTrue();
            brand.GetString().Should().Be("Seat");
            body.RootElement.TryGetProperty("model", out var model).Should().BeTrue();
            model.GetString().Should().Be("Leon");
            body.RootElement.TryGetProperty("status", out var status).Should().BeTrue();
            status.GetString().Should().Be("Available");
        }

        [Fact]
        public async Task Post_WithADuplicateLicensePlate_ReturnsConflict()
        {
            using var client = Fixture.Server.CreateClient();

            var request = new CreateVehicleRequest
            {
                Brand = "Renault",
                Model = "Clio",
                LicensePlate = $"IT{Guid.NewGuid():N}"[..8],
                ManufactureDate = DateOnly.FromDateTime(DateTime.UtcNow),
            };

            using var firstResponse = await client.PostAsJsonAsync("/api/vehicles", request);
            firstResponse.StatusCode.Should().Be(HttpStatusCode.Created);

            using var secondResponse = await client.PostAsJsonAsync("/api/vehicles", request);

            secondResponse.StatusCode.Should().Be(HttpStatusCode.Conflict);
        }
    }
}
