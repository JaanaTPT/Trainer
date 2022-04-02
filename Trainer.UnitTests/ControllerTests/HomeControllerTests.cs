﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainer.Controllers;
using Trainer.FileAccess;
using Xunit;

namespace Trainer.UnitTests.ControllerTests
{
    public class HomeControllerTests
    {
        private readonly Mock<IFileClient> _fileClientMock;
        private readonly HomeController _homeController;

        public HomeControllerTests()
        {
            _fileClientMock = new Mock<IFileClient>();
            _homeController = new HomeController(_fileClientMock.Object);
        }

        [Fact]
        public async Task Upload_should_survive_null_collection()
        {
            // Arrange
            var files = (IFormFile[])null;

            // Act
            var result = _homeController.Index(files) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Upload_should_save_file()
        {
            // Arrange
            var defaultViewNames = new[] { null, "Delete" };
            var files = new IFormFile[]
            {
                new FakeFormFile { FileName = "test.txt" }
            };
            _fileClientMock.Setup(fc => fc.Save(FileContainerNames.Documents,
                                                It.IsAny<string>(),
                                                It.IsAny<Stream>()))
                           .Verifiable();

            // Act
            var result = _homeController.Index(files) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.ViewName, defaultViewNames);
            _fileClientMock.VerifyAll();
        }

    }
}