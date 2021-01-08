using System.Threading.Tasks;
using FlowerStoreAPI.Dtos;
using FlowerStoreAPI.Models;
using FlowerStoreAPI.Test.Integration.Utils;
using FluentAssertions;
using Newtonsoft.Json;
using Snapshooter;
using Snapshooter.Xunit;
using Xunit;

namespace FlowerStoreAPI.Tests.Integration.FlowerTests
{
    // In this test we test all the methods of the garages controller.
    // This is both pretty artificial (as the API doesn't know things as "controllers", only endpoints)
    // and easy for our examples. An alternative is splitting the tests over "retrieval" and "crud" tests,
    // or even a single test per method.
    public class FlowersTests : IClassFixture<CustomWebApplicationFactory<Startup>>
    {
        private readonly CustomWebApplicationFactory<Startup> _factory;

        public FlowerTests(CustomWebApplicationFactory<Startup> factory)
        {
            _factory = factory;
        }

        // A Task<T> is about the same as an Promise<T>, and we will talk about that in later lessons.
        // For now it's sufficient to know (but not entirely correct) ...
        //  - a method is async when you mark it as such (public async ... )
        //  - a method is async if you have an await in there somewhere
        //  - an async method always returns Task or Task<T> and needs to be awaited "further up the chain"
        [Fact]
        public async Task GetFlowerEndPointReturnsNoDataWhenDbIsEmpty()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });
            var response = await client.GetAsync("/flower");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task GetFlowerEndPointReturnsSomeDataWhenDbIsNotEmpty()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.Flower.Add(new Flower() {Id = 1, Name = "test flower 1"});
                db.Flower.Add(new Flower() {Id = 2, Name = "test flower 2"});
            });
            var response = await client.GetAsync("/flower");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task GetFlowerById404IfDoesntExist()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });
            var response = await client.GetAsync("/flower/1");
            response.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task GetFlowerByIdReturnFlowerIfExists()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.Flower.Add(new Flower() {Id = 1, Name = "test flower 1"});
            });
            var response = await client.GetAsync("/flower/1");
            response.EnsureSuccessStatusCode(); // Status Code 200-299
            Snapshot.Match(await response.Content.ReadAsStringAsync());
        }

        [Fact]
        public async Task DeleteFlowerByIdReturns404IfDoesntExist()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.Flower.Add(new Flower() {Id = 1, Name = "test flower 1"});
            });
            var response = await client.DeleteAsync("/flower/2");
            response.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task DeleteFlowerByIdReturnsDeletesIfExists()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.Flower.Add(new Flower() {Id = 1, Name = "test flower 1"});
            });
            var beforeDeleteResponse = await client.GetAsync("/flower/1");
            beforeDeleteResponse.EnsureSuccessStatusCode();
            var deleteResponse = await client.DeleteAsync("/flower/1");
            deleteResponse.EnsureSuccessStatusCode();
            var afterDeleteResponse = await client.GetAsync("/flower/1");
            afterDeleteResponse.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task InsertFlowerReturnsCorrectData()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new FlowerUpsertInput
                {
                    Name = "hey"
                }
            };
            var createResponse = await client.PostAsync("/flower", ContentHelper.GetStringContent(request.Body));
            createResponse.EnsureSuccessStatusCode();
            var body = JsonConvert.DeserializeObject<FlowerWebOutput>(await createResponse.Content.ReadAsStringAsync());
            body.Should().NotBeNull();
            body.Name.Should().Be("hey");
            var getResponse = await client.GetAsync($"/flower/{body.Id}");
            getResponse.EnsureSuccessStatusCode();
        }

        [Fact]
        public async Task InsertFlowerThrowsErrorOnEmptyName()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new FlowerUpsertInput
                {
                    Name = string.Empty
                }
            };
            var createResponse = await client.PostAsync("/flower", ContentHelper.GetStringContent(request.Body));
            createResponse.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task InsertFlowerThrowsErrorOnGiganticName()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new FlowerUpsertInput
                {
                    Name = new string('c', 10001)
                }
            };
            var createResponse = await client.PostAsync("/flower", ContentHelper.GetStringContent(request.Body));
            createResponse.StatusCode.Should().Be(400);
        }

        [Fact]
        public async Task UpdateFlowerReturns404NonExisting()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) => { });

            var request = new
            {
                Body = new FlowerUpsertInput
                {
                    Name = "hey"
                }
            };
            var patchResponse = await client.PatchAsync("/flower/1", ContentHelper.GetStringContent(request.Body));
            patchResponse.StatusCode.Should().Be(404);
        }

        [Fact]
        public async Task UpdateFlowerReturnsAnUpdatedResult()
        {
            var client = _factory.CreateClient();
            _factory.ResetAndSeedDatabase((db) =>
            {
                db.Flower.Add(new Flower() {Id = 1, Name = "test flower 1"});
            });
            var request = new
            {
                Body = new FlowerUpsertInput
                {
                    Name = "hey"
                }
            };
            var patchResponse = await client.PatchAsync("/flower/1", ContentHelper.GetStringContent(request.Body));
            patchResponse.EnsureSuccessStatusCode();
            var body = JsonConvert.DeserializeObject<FlowerWebOutput>(await patchResponse.Content.ReadAsStringAsync());
            body.Should().NotBeNull();
            body.Name.Should().Be("hey");
            var getResponse = await client.GetAsync($"/flower/{body.Id}");
            getResponse.EnsureSuccessStatusCode();
            Snapshot.Match(getResponse.Content.ReadAsStringAsync(), new SnapshotNameExtension("_Content"));
            Snapshot.Match(getResponse, new SnapshotNameExtension("_Full"));
        }
    }
}