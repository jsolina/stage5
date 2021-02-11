using Domain.Contracts;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using TodoAppAPI.Controllers;
using Xunit;

namespace TodoAppApi.Test
{
    public class TaskListUnitTest
    {
        private readonly TaskListController _controller;    
        /*
        public TaskListUnitTest()
        {
            //This -- Testing MVC Controllers in ASP.NET Core
            _mockService = new Mock<ITaskListServices>();

            _controller = new TaskListController(_mockService.Object);

            //_controller = new TaskListController(_services);
            // _services = new TaskListServices(_repoServices);
            //_mockRepo = new Mock<ITaskListRepo>();
            //_controllerRepo = new TaskListController(_mockRepo.Object);
        }

        [Fact]
        public void Get_WhenCalled_ReturnsOkResult()
        {
            // Act
            var okResult = _controller.GetAll();
            // Assert
            Assert.IsType<OkObjectResult>(okResult);
        }

        [Fact]
        public void Create_ActionExecutes_ReturnsObjectResult()
        {
            var employee = new TaskListModel  {taskName = "testName", taskDetails= "testDetails" };

            var result = _controller.Create(employee);
            Assert.IsType<ObjectResult>(result);
        }

        [Fact]
        public void Create_InvalidModelState_CreateEmployeeNeverExecutes()
        {
            _controller.ModelState.AddModelError("taskName", "Name is required");

            var employee = new TaskListModel { taskName="aw", taskDetails="aw" };

            var result = _controller.Create(employee);

            _mockService.Verify(x => x.Create(It.IsAny<TaskListModel>()), Times.Never);
        }
        */
    }
}
