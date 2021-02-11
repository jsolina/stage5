using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using TodoAppAPI.Controllers;
using Xunit;

namespace TodoAppApi.Test
{
    public class ItemListUnitTest
    {
   
        private readonly ItemListController _controller;

        public ItemListUnitTest()
        {
            /*
            //This -- Testing MVC Controllers in ASP.NET Core
            _mockRepo = new Mock<IItemListServices>();
            _controller = new ItemListController(_mockRepo.Object);
            */
        }

        [Fact]
        public void GetAll_ActionExecutes_ReturnsViewForIndex()
        {
            /*
            // Act
            var result = _controller.GetAll();
            // Assert
            Assert.IsType<OkObjectResult>(result);
            */
        }
    }
}
