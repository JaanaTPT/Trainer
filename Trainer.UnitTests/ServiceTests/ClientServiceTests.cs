//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using Trainer.Core.IConfiguration;
//using Trainer.Core.Repository.ClientRepo;
//using Trainer.Data;
//using Trainer.Models;
//using Trainer.Services;
//using Xunit;

//namespace Trainer.UnitTests.ServiceTests
//{
//    public class ClientServiceTests
//    {
//        private readonly Mock<IClientRepository> _clientRepositoryMock;
//        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
//        private readonly ClientService _clientService;

//        public ClientServiceTests()
//        {
//            _clientRepositoryMock = new Mock<IClientRepository>();
//            _unitOfWorkMock = new Mock<IUnitOfWork>();

//            _unitOfWorkMock.SetupGet(uow => uow.ClientRepository)
//                           .Returns(_clientRepositoryMock.Object);

//            _clientService = new ClientService(_unitOfWorkMock.Object);
//        }

//        [Fact]
//        public async Task List_returns_paged_list_of_clients()
//        {
//            // Arrange
//            int page = 1;
//            int pageSize = 10;
//            _clientRepositoryMock.Setup(pr => pr.GetPagedList(page, pageSize, "", ""))
//                                  .ReturnsAsync(() => new PagedResult<Client>())
//                                  .Verifiable();

//            // Act
//            var result = await _clientService.GetPagedList(page, pageSize, "", "");

//            // Assert
//            Assert.NotNull(result);
//            Assert.IsType<PagedResult<Client>>(result);
//            _clientRepositoryMock.VerifyAll();
//        }

//        [Fact]
//        public async Task GetById_should_return_null_if_client_was_not_found()
//        {
//            // Arrange
//            var nonExistentId = -1;
//            var nullClient = (Client)null;
//            _clientRepositoryMock.Setup(pr => pr.GetById(nonExistentId))
//                                  .ReturnsAsync(() => nullClient)
//                                  .Verifiable();

//            // Act
//            var result = await _clientService.GetById(nonExistentId);

//            // Assert
//            Assert.Null(result);
//            _clientRepositoryMock.VerifyAll();
//        }

//        [Fact]
//        public async Task GetById_should_return_client()
//        {
//            // Arrange
//            var id = 1;
//            var client = new Client { ID = id };
//            _clientRepositoryMock.Setup(pr => pr.GetById(id))
//                                  .ReturnsAsync(() => client)
//                                  .Verifiable();

//            // Act
//            var result = await _clientService.GetById(id);

//            // Assert
//            Assert.NotNull(result);
//            Assert.IsType<Client>(result);
//            _clientRepositoryMock.VerifyAll();
//        }

//        //[Fact]
//        //public async Task Save_should_handle_missing_client()
//        //{
//        //    // Arrange
//        //    var id = 1;
//        //    var client = new Client { ID = id };
//        //    var nullClient = (Client)null;
//        //    _clientRepositoryMock.Setup(pr => pr.GetById(id))
//        //                          .ReturnsAsync(() => nullClient)
//        //                          .Verifiable();

//        //    // Act
//        //    var response = await _clientService.Save(client);

//        //    // Assert
//        //    Assert.NotNull(response);
//        //    Assert.False(response.Success);
//        //}

//        //[Fact]
//        //public async Task Save_should_save_valid_client()
//        //{
//        //    // Arrange
//        //    var id = 1;
//        //    var client = new Client { ID = id };

//        //    _clientRepositoryMock.Setup(pr => pr.GetById(id))
//        //                          .ReturnsAsync(() => client)
//        //                          .Verifiable();
//        //    _clientRepositoryMock.Setup(pr => pr.Save(It.IsAny<Client>()))
//        //                          .Verifiable();
//        //    _unitOfWorkMock.Setup(uow => uow.CommitAsync())
//        //                   .Verifiable();

//        //    // Act
//        //    var response = await _clientService.Save(client);

//        //    // Assert
//        //    Assert.NotNull(response);
//        //    Assert.True(response.Success);
//        //    _clientRepositoryMock.VerifyAll();
//        //    _unitOfWorkMock.VerifyAll();
//        //}


//        //[Fact]
//        //public async Task Delete_handles_null_client()
//        //{
//        //    // Arrange
//        //    var id = 1;
//        //    var productToDelete = (Client)null;

//        //    _clientRepositoryMock.Setup(pr => pr.GetById(id))
//        //                          .ReturnsAsync(() => productToDelete)
//        //                          .Verifiable();

//        //    // Act
//        //    var response = await _clientService.Delete(productToDelete);

//        //    // Assert
//        //    Assert.NotNull(response);
//        //    Assert.False(response.Success);
//        //    _clientRepositoryMock.VerifyAll();
//        //}

//        //[Fact]
//        //public async Task Delete_deletes_client()
//        //{
//        //    // Arrange
//        //    var id = 1;
//        //    var clientToDelete = new Client { ID = id };

//        //    _clientRepositoryMock.Setup(pr => pr.GetById(id))
//        //                          .ReturnsAsync(() => clientToDelete)
//        //                          .Verifiable();
//        //    _clientRepositoryMock.Setup(pr => pr.Delete(clientToDelete))
//        //                          .Verifiable();
//        //    _unitOfWorkMock.Setup(uow => uow.CommitAsync())
//        //                   .Verifiable();

//        //    // Act
//        //    var response = await _clientService.Delete(clientToDelete);

//        //    // Assert
//        //    Assert.NotNull(response);
//        //    Assert.True(response.Success);
//        //    _clientRepositoryMock.VerifyAll();
//        //    _unitOfWorkMock.VerifyAll();
//        //}

//    }
//}
