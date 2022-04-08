using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainer.Core.IConfiguration;
using Trainer.Core.Repository.ClientRepo;
using Trainer.Core.Repository.TrainingRepo;
using Trainer.Data;
using Trainer.Models;
using Trainer.Models.ViewModels;
using Trainer.Services;
using Xunit;

namespace Trainer.UnitTests.ServiceTests
{
    public class TrainingServiceTests
    {
        private readonly Mock<ITrainingRepository> _trainingRepositoryMock;
        private readonly Mock<IClientRepository> _clientRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly TrainingService _trainingService;

        public TrainingServiceTests()
        {
            _trainingRepositoryMock = new Mock<ITrainingRepository>();
            _clientRepositoryMock = new Mock<IClientRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(Program).Assembly);
            });
            var mapper = mapperConfig.CreateMapper();

            _unitOfWorkMock.SetupGet(uow => uow.TrainingRepository)
                           .Returns(_trainingRepositoryMock.Object);

            _unitOfWorkMock.SetupGet(uow => uow.ClientRepository)
                           .Returns(_clientRepositoryMock.Object);

            _trainingService = new TrainingService(_unitOfWorkMock.Object, mapper);
        }

        [Fact]
        public async Task List_returns_paged_list_of_training_models()
        {
            // Arrange
            int page = 1;
            int pageSize = 10;
            _trainingRepositoryMock.Setup(pr => pr.GetPagedList(page, pageSize, "", ""))
                                  .ReturnsAsync(() => new PagedResult<Training>())
                                  .Verifiable();

            // Act
            var result = await _trainingService.GetPagedList(page, pageSize, "", "");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PagedResult<TrainingModel>>(result);
            _trainingRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task GetById_should_return_null_if_training_was_not_found()
        {
            // Arrange
            var nonExistentId = -1;
            var nullTraining = (Training)null;
            _trainingRepositoryMock.Setup(pr => pr.GetById(nonExistentId))
                                  .ReturnsAsync(() => nullTraining)
                                  .Verifiable();

            // Act
            var result = await _trainingService.GetById(nonExistentId);

            // Assert
            Assert.Null(result);
            _trainingRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task GetById_should_return_training()
        {
            // Arrange
            var id = 1;
            var training = new Training { ID = id };
            _trainingRepositoryMock.Setup(pr => pr.GetById(id))
                                  .ReturnsAsync(() => training)
                                  .Verifiable();

            // Act
            var result = await _trainingService.GetById(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TrainingModel>(result);
            _trainingRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task GetForEdit_should_return_null_if_training_was_not_found()
        {
            // Arrange
            var nonExistentId = -1;
            var nullTraining = (Training)null;
            _trainingRepositoryMock.Setup(pr => pr.GetById(nonExistentId))
                                  .ReturnsAsync(() => nullTraining)
                                  .Verifiable();

            // Act
            var result = await _trainingService.GetForEdit(nonExistentId);

            // Assert
            Assert.Null(result);
            _trainingRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task GetForEdit_should_return_training()
        {
            // Arrange
            var id = 1;
            var training = new Training { ID = id };
            var clients = GetClientsPaged();
            _trainingRepositoryMock.Setup(pr => pr.GetById(id))
                                  .ReturnsAsync(() => training)
                                  .Verifiable();
            _clientRepositoryMock.Setup(cl => cl.GetPagedList(1, 100, It.IsAny<string>(), It.IsAny<string>()))
                                       .ReturnsAsync(() => clients);

            // Act
            var result = await _trainingService.GetForEdit(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TrainingEditModel>(result);
            _trainingRepositoryMock.VerifyAll();
            _clientRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Save_should_survive_null_model()
        {
            // Arrange
            var model = (TrainingEditModel)null;

            // Act
            var response = await _trainingService.Save(model);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
        }

        [Fact]
        public async Task Save_should_handle_missing_training()
        {
            //Arrange
            var id = 1;
            var training = new TrainingEditModel { ID = id };
            var nullTraining = (Training)null;
            _trainingRepositoryMock.Setup(pr => pr.GetById(id))
                                  .ReturnsAsync(() => nullTraining)
                                  .Verifiable();

            //Act
            var response = await _trainingService.Save(training);

            //Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
        }

        [Fact]
        public async Task Save_should_handle_missing_client()
        {
            // Arrange
            var id = 1;
            var training = new Training { ID = id };
            var trainingModel = new TrainingEditModel { ID = id, ClientID = id };
            var client = (Client)null;

            _trainingRepositoryMock.Setup(pr => pr.GetById(id))
                                  .ReturnsAsync(() => training)
                                  .Verifiable();
            _clientRepositoryMock.Setup(mf => mf.GetById(id))
                                  .ReturnsAsync(() => client)
                                  .Verifiable();

            // Act
            var response = await _trainingService.Save(trainingModel);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
            _trainingRepositoryMock.VerifyAll();
            _clientRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Save_should_save_valid_training()
        {
            // Arrange
            var id = 1;
            var training = new Training { ID = id };
            var trainingModel = new TrainingEditModel { ID = id, ClientID = id };
            var client = new Client { ID = id };

            _trainingRepositoryMock.Setup(pr => pr.GetById(id))
                                  .ReturnsAsync(() => training)
                                  .Verifiable();
            _trainingRepositoryMock.Setup(pr => pr.Save(It.IsAny<Training>()))
                                  .Verifiable();
            _clientRepositoryMock.Setup(cl => cl.GetById(id))
                                 .ReturnsAsync(() => client)
                                 .Verifiable();
            _unitOfWorkMock.Setup(uow => uow.CommitAsync())
                           .Verifiable();

            // Act
            var response = await _trainingService.Save(trainingModel);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            _trainingRepositoryMock.VerifyAll();
            _clientRepositoryMock.VerifyAll();
            _unitOfWorkMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_should_survive_null_model()
        {
            // Arrange
            var model = (TrainingModel)null;

            // Act
            var response = await _trainingService.Delete(model);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
        }

        [Fact]
        public async Task Delete_handles_null_training()
        {
            // Arrange
            var id = 1;
            var trainingModelToDelete = new TrainingModel { ID = id };
            var trainingToDelete = (Training)null;

            _trainingRepositoryMock.Setup(pr => pr.GetById(id))
                                  .ReturnsAsync(() => trainingToDelete)
                                  .Verifiable();

            // Act
            var response = await _trainingService.Delete(trainingModelToDelete);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
            _trainingRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_deletes_training()
        {
            // Arrange
            var id = 1;
            var trainingModelToDelete = new TrainingModel { ID = id };
            var trainingToDelete = new Training { ID = id };

            _trainingRepositoryMock.Setup(pr => pr.GetById(id))
                                  .ReturnsAsync(() => trainingToDelete)
                                  .Verifiable();
            _trainingRepositoryMock.Setup(pr => pr.Delete(id))
                                  .Verifiable();
            _unitOfWorkMock.Setup(uow => uow.CommitAsync())
                           .Verifiable();

            // Act
            var response = await _trainingService.Delete(trainingModelToDelete);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            _trainingRepositoryMock.VerifyAll();
            _unitOfWorkMock.VerifyAll();
        }

        private PagedResult<Client> GetClientsPaged()
        {
            return new PagedResult<Client>
            {
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 10,
                Results = new List<Client>
                {
                    new Client { ID = 1, FirstName = "Firstname1", LastName = "Lastname1" },
                    new Client { ID = 2, FirstName = "Firstname2", LastName = "Lastname2" }
                },
                RowCount = 2
            };
        }
    }
}
