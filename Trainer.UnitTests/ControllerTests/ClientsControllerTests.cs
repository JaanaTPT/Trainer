using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trainer.Controllers;
using Trainer.Services;
using Trainer.Models;
using Microsoft.AspNetCore.Mvc;
using Xunit;
using Trainer.Data;

namespace Trainer.UnitTests.ControllerTests
{
    public class ClientsControllerTests
    {
        private readonly Mock<IClientService> _clientServiceMock;
        private readonly ClientsController _clientsController;

        public ClientsControllerTests()
        {
            _clientServiceMock = new Mock<IClientService>();
            _clientsController = new ClientsController(_clientServiceMock.Object);
        }


        [Fact]
        public async Task Index_should_return_list_of_clients()
        {
            // Arrange
            var page = 1;
            var clients = GetPagedProductList();
            _clientServiceMock.Setup(cs => cs.GetPagedList(page, It.IsAny<int>())).
                               ReturnsAsync(() => clients);

            // Act
            var result = await _clientsController.Index("FirstName", "Name", page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.True(result.Model is PagedResult<Client>);
        }

        [Fact]
        public async Task Index_should_return_default_view()
        {
            // Arrange
            var defaultViewNames = new[] { null, "Index" };
            var page = 1;
            var clients = GetPagedProductList();
            _clientServiceMock.Setup(cs => cs.GetPagedList(page, It.IsAny<int>()))
                               .ReturnsAsync(() => clients);

            // Act
            var result = await _clientsController.Index("FirstName", "Name", page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.ViewName, defaultViewNames);
        }

        [Fact]
        public async Task Index_should_survive_null_model()
        {
            // Arrange
            var page = 1;
            var clients = GetPagedProductList();
            _clientServiceMock.Setup(cs => cs.GetPagedList(page, It.IsAny<int>())).
                               ReturnsAsync(() => clients);

            // Act
            var result = await _clientsController.Index("FirstName", "Name") as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_not_found_if_id_is_null()
        {
            // Act
            var result = await _clientsController.Details(null) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_not_found_if_product_is_null()
        {
            // Arrange
            var nonExistentId = -1;
            _clientServiceMock.Setup(cs => cs.GetById(It.IsAny<int>()))
                               .ReturnsAsync(() => null);

            // Act
            var result = await _clientsController.Details(nonExistentId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_returns_correct_result_when_product_is_found()
        {
            // Arrange
            var model = GetClient();
            var defaultViewNames = new[] { null, "Details" };
            _clientServiceMock.Setup(ps => ps.GetById(It.IsAny<int>()))
                               .ReturnsAsync(() => model);

            // Act
            var result = await _clientsController.Details(model.ID) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.ViewName, defaultViewNames);
            Assert.NotNull(result.Model);
            Assert.IsType<Client>(result.Model);
        }

        [Fact]
        public async Task Edit_should_stay_on_form_when_model_is_invalid()
        {
            // Arrange
            var defaultViewNames = new[] { null, "Edit" };
            var clientId = 1;
            var client = new Client();
            client.ID = clientId;
            client.FirstName = "012345678901234567890123456789012345678901234567890123456789";

            _clientServiceMock.Setup(s => s.GetById(It.IsAny<int>()))
                               .Verifiable();

            // Act
            _clientsController.ModelState.AddModelError("Id", "ERROR");
            var result = await _clientsController.EditPost(clientId);
            var typedResult = result as ViewResult;

            // Assert
            Assert.NotNull(typedResult);
            Assert.Contains(typedResult.ViewName, defaultViewNames);
            Assert.False(_clientsController.ModelState.IsValid);
            _clientServiceMock.VerifyAll();
        }

        ////seda saab siis teha, kui OperationResponse on tehtud
        //[Fact]
        //public async Task Edit_should_save_client_data()
        //{
        //    // Arrange
        //    var product = GetClientEdit();
        //    var response = new OperationResponse();
        //    _clientServiceMock.Setup(ps => ps.Save(product))
        //                       .ReturnsAsync(() => response)
        //                       .Verifiable();

        //    // Act
        //    var result = await _clientsController.Edit(product.Id, product) as RedirectToActionResult;

        //    // Assert
        //    Assert.NotNull(result);
        //    _clientServiceMock.VerifyAll();
        //}

        private Client GetClient()
        {
            return GetClientList()[0];
        }

        private IList<Client> GetClientList()
        {
            return new List<Client>
            {
                new Client { ID = 1, FirstName = "ClientFirstName1", LastName = "ClientLastName1" },
                new Client { ID = 2, FirstName = "ClientFirstName2", LastName = "ClientLastName2" }
            };
        }

        private PagedResult<Client> GetPagedProductList()
        {
            return new PagedResult<Client>
            {
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 10,
                Results = GetClientList(),
                RowCount = 2
            };
        }

        private Client GetClientEdit()
        {
            var model = GetClient();
            var editModel = new Client();

            editModel.ID = model.ID;
            editModel.FirstName = model.FirstName;
            editModel.LastName = model.LastName;

            return editModel;
        }
    }
}
