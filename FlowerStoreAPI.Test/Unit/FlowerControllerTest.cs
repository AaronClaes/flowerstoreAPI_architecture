using System;
using System.Collections.Generic;
using FlowerStoreAPI.Controllers;
using FlowerStoreAPI.Models;
using FlowerStoreAPI.Dtos;
using FlowerStoreAPI.Repositories;
using FluentAssertions;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using Snapshooter.Xunit;
using Xunit;

namespace FlowerStoreAPI.Test.Unit.FlowerControllerTest
{

    public class FlowersControllertest : IDisposable
    {   
        private readonly Mock<ILogger<FlowersController>> _loggerMock;
        private readonly Mock<ILogger<IFlowerRepo>> _SqlFlowerRepoMock;
        private readonly FlowersController _FlowersController;

        public FlowersControllerTests()
        {
            // In our tests we choose to ignore whatever logging is being done. We still need to mock it to avoid 
            // null reference exceptions; loose mocks just handle whatever you throw at them.
            _loggerMock = new Mock<ILogger<FlowersController>>(MockBehavior.Loose);
            _SqlFlowerRepoMock = new Mock<IFlowerRepo>(MockBehavior.Strict);
            _FlowersController = new FlowersController(_SqlFlowerRepoMock.Object, _loggerMock.Object);
        }

        public void Dispose()
        {
            _loggerMock.VerifyAll();
            _SqlFlowerRepoMock.VerifyAll();

            _loggerMock.Reset();
            _SqlFlowerRepoMock.Reset();
        }

        [Fact]
         public void TestGetAllFlowers()
        {
            var returnSet = new[]
            {
                new Flower
                {
                    Id = 1,
                    Name = "test flower 1",
                    Color= "test color flower 1",
                    Price = 3
                },
                new Flower
                {
                    Id = 2,
                    Name = "test flower 2",
                    Color= "test color flower 2",
                    Price = 4
                },
                new Flower
                {
                    Id = 3,
                    Name = "test flower 3",
                    Color = "test color flower 3",
                    Price = 5
                },
            };
            // Arrange
            _SqlFlowerRepoMock.Setup(x => x.GetAllFlowers()).Returns(returnSet).Verifiable();

            // Act
            var flowerResponse = _FlowersController.GetAllFlowers();

            // Assert
            flowerResponse.Should().BeOfType<OkObjectResult>();

            // verify via a snapshot (https://swisslife-oss.github.io/snapshooter/)
            // used a lot in jest (for JS)
            Snapshot.Match(flowerResponse);
        }


        [Fact]
        public void TestGetFlowerById()
        {
            var flower = new Flower()
            {
                Id = 1,
                Name = "test flower",
                Color = "test color flower",
                Price = 5
            };
            _SqlFlowerRepoMock.Setup(x => x.GetFlowerById(1)).Returns(flower).Verifiable();
            var flowerResponse = _flowersController.GetFlowerById(1);
            flowerResponse.Should().BeOfType<OkObjectResult>();
            Snapshot.Match(flowerResponse);
        }   


        [Fact]
        public void TestGetFlowerByIdNotFound()
        {
            _SqlFlowerRepoMock.Setup(x => x.GetFlowerById(1)).Returns(null as flower).Verifiable();
            var flowerResponse = _flowersController.GetFlowerById(1);
            flowerResponse.Should().BeOfType<NotFoundResult>();
            Snapshot.Match(flowerResponse);
        }


        [Fact]
        public void TestCreateFlower()
        {
            var flower = new Flower()
            {
                Id = 1,
                Name = "test flower",
                Color = "test flower color",
                Price = 4
            };            
            _SqlFlowerRepoMock.Setup(x => x.Insert("test flower", "test flower color", 4)).Returns(flower).Verifiable();
            var flowerResponse = _flowersController.CreateFlower(new FlowerCreateDto()
            {
                Name = "test flower",
                Color = "test flower color",
                Price = 4
            });
            flowerResponse.Should().BeOfType<CreatedResult>();
            Snapshot.Match(flowerResponse);
        }

        [Fact]
        public void TestUpdateFlower()
        {
            var flower = new Flower()
            {
                Id = 1,
                Name = "flower",
                Color = "flower color",
                Price = 4
            };            
            _SqlFlowerRepoMock.Setup(x => x.Update("test flower", "test flower color", 4)).Returns(flower).Verifiable();
            var flowerResponse = _FlowersController.UpdateFlower(1, new FlowerUpsertInput()
            {
                Id = 1,
                Name = "flower",
                Color = "flower color",
                Price = 4
            });
            flowerResponse.Should().BeOfType<AcceptedResult>();
            Snapshot.Match(flowerResponse);
        }

        [Fact]
        public void TestUpdateFlowerNotFound()
        {
   
            _SqlFlowerRepoMock
                .Setup(x => x.Update("flower", "flower color", 4))
                .Throws<NotFoundException>()
                .Verifiable();
            var flowerResponse = _FlowersController.UpdateFlower(1, new FlowerUpsertInput()
            {
                Id = 1,
                Name = "flower",
                Color = "flower color",
                Price = 4
            });
            flowerResponse.Should().BeOfType<NotFoundResult>();
            Snapshot.Match(flowerResponse);
        }

    }
}
