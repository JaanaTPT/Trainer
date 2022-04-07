using AutoMapper;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainer.Core.IConfiguration;
using Trainer.Core.Repository.ExerciseRepo;
using Trainer.Core.Repository.TrainingExerciseRepo;
using Trainer.Core.Repository.TrainingRepo;
using Trainer.Data;
using Trainer.Models;
using Trainer.Models.ViewModels;
using Trainer.Services;
using Xunit;

namespace Trainer.UnitTests.ServiceTests
{
    public class TrainingExerciseServiceTests
    {
        private readonly Mock<ITrainingExerciseRepository> _trainingExerciseRepositoryMock;
        private readonly Mock<IExerciseRepository> _exerciseRepositoryMock;
        private readonly Mock<ITrainingRepository> _trainingRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly TrainingExerciseService _trainingExerciseService;

        public TrainingExerciseServiceTests()
        {
            _trainingExerciseRepositoryMock = new Mock<ITrainingExerciseRepository>();
            _exerciseRepositoryMock = new Mock<IExerciseRepository>();
            _trainingRepositoryMock = new Mock<ITrainingRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(Program).Assembly);
            });
            var mapper = mapperConfig.CreateMapper();

            _unitOfWorkMock.SetupGet(uow => uow.TrainingExerciseRepository)
                           .Returns(_trainingExerciseRepositoryMock.Object);

            _trainingExerciseService = new TrainingExerciseService(_unitOfWorkMock.Object, mapper);
        }

        [Fact]
        public async Task List_returns_paged_list_of_trainingExercise_models()
        {
            // Arrange
            int page = 1;
            int pageSize = 10;
            _trainingExerciseRepositoryMock.Setup(pr => pr.GetPagedList(page, pageSize, "", ""))
                                  .ReturnsAsync(() => new PagedResult<TrainingExercise>())
                                  .Verifiable();

            // Act
            var result = await _trainingExerciseService.GetPagedList(page, pageSize, "", "");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PagedResult<TrainingExerciseModel>>(result);
            _trainingExerciseRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task GetById_should_return_null_if_trainingExercise_was_not_found()
        {
            // Arrange
            var nonExistentId = -1;
            var nullTrainingExercise = (TrainingExercise)null;
            _trainingExerciseRepositoryMock.Setup(pr => pr.GetById(nonExistentId))
                                  .ReturnsAsync(() => nullTrainingExercise)
                                  .Verifiable();

            // Act
            var result = await _trainingExerciseService.GetById(nonExistentId);

            // Assert
            Assert.Null(result);
            _trainingExerciseRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task GetById_should_return_trainingExercise()
        {
            // Arrange
            var id = 1;
            var trainingExercise = new TrainingExercise { ID = id };
            _trainingExerciseRepositoryMock.Setup(pr => pr.GetById(id))
                                  .ReturnsAsync(() => trainingExercise)
                                  .Verifiable();

            // Act
            var result = await _trainingExerciseService.GetById(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TrainingExerciseModel>(result);
            _trainingExerciseRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task GetForEdit_should_return_null_if_trainingExercise_was_not_found()
        {
            // Arrange
            var nonExistentId = -1;
            var nullTrainingExercise = (TrainingExercise)null;
            _trainingExerciseRepositoryMock.Setup(pr => pr.GetById(nonExistentId))
                                  .ReturnsAsync(() => nullTrainingExercise)
                                  .Verifiable();

            // Act
            var result = await _trainingExerciseService.GetForEdit(nonExistentId);

            // Assert
            Assert.Null(result);
            _trainingExerciseRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task GetForEdit_should_return_trainingExercise()
        {
            // Arrange
            var id = 1;
            var trainingExercise = new TrainingExercise { ID = id };
            var exercises = GetExercisesPaged();
            var trainings = GetTrainingsPaged();
            _trainingExerciseRepositoryMock.Setup(pr => pr.GetById(id))
                                  .ReturnsAsync(() => trainingExercise)
                                  .Verifiable();
            _exerciseRepositoryMock.Setup(cl => cl.GetPagedList(1, 100, "", ""))
                                       .ReturnsAsync(() => exercises);
            _trainingRepositoryMock.Setup(cl => cl.GetPagedList(1, 100, "", ""))
                                       .ReturnsAsync(() => trainings);

            // Act
            var result = await _trainingExerciseService.GetForEdit(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<TrainingEditModel>(result);
            _trainingExerciseRepositoryMock.VerifyAll();
            _exerciseRepositoryMock.VerifyAll();
            _trainingRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Save_should_survive_null_model()
        {
            // Arrange
            var model = (TrainingExerciseEditModel)null;

            // Act
            var response = await _trainingExerciseService.Save(model);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
        }

        [Fact]
        public async Task Save_should_handle_missing_trainingExercise()
        {
            //Arrange
            var id = 1;
            var trainingExercise = new TrainingExerciseEditModel { ID = id };
            var nullTrainingExercise = (TrainingExercise)null;
            _trainingExerciseRepositoryMock.Setup(pr => pr.GetById(id))
                                  .ReturnsAsync(() => nullTrainingExercise)
                                  .Verifiable();

            //Act
            var response = await _trainingExerciseService.Save(trainingExercise);

            //Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
        }

        [Fact]
        public async Task Save_should_handle_missing_exercise()
        {
            // Arrange
            var id = 1;
            var trainingExercise = new TrainingExercise { ID = id };
            var trainingExerciseModel = new TrainingExerciseEditModel { ID = id, ExerciseID = id };
            var exercise = (Exercise)null;

            _trainingExerciseRepositoryMock.Setup(pr => pr.GetById(id))
                                  .ReturnsAsync(() => trainingExercise)
                                  .Verifiable();
            _exerciseRepositoryMock.Setup(mf => mf.GetById(id))
                                  .ReturnsAsync(() => exercise)
                                  .Verifiable();

            // Act
            var response = await _trainingExerciseService.Save(trainingExerciseModel);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
            _trainingExerciseRepositoryMock.VerifyAll();
            _exerciseRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Save_should_handle_missing_training()
        {
            // Arrange
            var id = 1;
            var trainingExercise = new TrainingExercise { ID = id };
            var trainingExerciseModel = new TrainingExerciseEditModel { ID = id, ExerciseID = id };
            var training = (Training)null;

            _trainingExerciseRepositoryMock.Setup(pr => pr.GetById(id))
                                  .ReturnsAsync(() => trainingExercise)
                                  .Verifiable();
            _trainingRepositoryMock.Setup(mf => mf.GetById(id))
                                  .ReturnsAsync(() => training)
                                  .Verifiable();

            // Act
            var response = await _trainingExerciseService.Save(trainingExerciseModel);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
            _trainingExerciseRepositoryMock.VerifyAll();
            _trainingRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Save_should_save_valid_trainingExercise()
        {
            // Arrange
            var id = 1;
            var trainingExercise = new TrainingExercise { ID = id };
            var trainingExerciseModel = new TrainingExerciseEditModel { ID = id, ExerciseID = id, TrainingID = id };
            var exercise = new Exercise { ID = id };
            var training = new Training { ID = id };

            _trainingExerciseRepositoryMock.Setup(pr => pr.GetById(id))
                                  .ReturnsAsync(() => trainingExercise)
                                  .Verifiable();
            _trainingExerciseRepositoryMock.Setup(pr => pr.Save(It.IsAny<TrainingExercise>()))
                                  .Verifiable();
            _exerciseRepositoryMock.Setup(cl => cl.GetById(id))
                                .ReturnsAsync(() => exercise)
                                .Verifiable();
            _trainingRepositoryMock.Setup(cl => cl.GetById(id))
                                .ReturnsAsync(() => training)
                                .Verifiable();
            _unitOfWorkMock.Setup(uow => uow.CommitAsync())
                           .Verifiable();

            // Act
            var response = await _trainingExerciseService.Save(trainingExerciseModel);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            _trainingExerciseRepositoryMock.VerifyAll();
            _exerciseRepositoryMock.VerifyAll();
            _trainingRepositoryMock.VerifyAll();
            _unitOfWorkMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_should_survive_null_model()
        {
            // Arrange
            var model = (TrainingExerciseModel)null;

            // Act
            var response = await _trainingExerciseService.Delete(model);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
        }

        [Fact]
        public async Task Delete_handles_null_trainingExercise()
        {
            // Arrange
            var id = 1;
            var trainingExerciseModelToDelete = new TrainingExerciseModel { ID = id };
            var trainingExerciseToDelete = (TrainingExercise)null;

            _trainingExerciseRepositoryMock.Setup(pr => pr.GetById(id))
                                  .ReturnsAsync(() => trainingExerciseToDelete)
                                  .Verifiable();

            // Act
            var response = await _trainingExerciseService.Delete(trainingExerciseModelToDelete);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
            _trainingExerciseRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_deletes_trainingExercise()
        {
            // Arrange
            var id = 1;
            var trainingExerciseModelToDelete = new TrainingExerciseModel { ID = id };
            var trainingExerciseToDelete = new TrainingExercise { ID = id };

            _trainingExerciseRepositoryMock.Setup(pr => pr.GetById(id))
                                  .ReturnsAsync(() => trainingExerciseToDelete)
                                  .Verifiable();
            _trainingExerciseRepositoryMock.Setup(pr => pr.Delete(id))
                                  .Verifiable();
            _unitOfWorkMock.Setup(uow => uow.CommitAsync())
                           .Verifiable();

            // Act
            var response = await _trainingExerciseService.Delete(trainingExerciseModelToDelete);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            _trainingExerciseRepositoryMock.VerifyAll();
            _unitOfWorkMock.VerifyAll();
        }

        private PagedResult<Exercise> GetExercisesPaged()
        {
            return new PagedResult<Exercise>
            {
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 10,
                Results = new List<Exercise>
                {
                    new Exercise { ID = 1, Title = "ExerciseTitle1"},
                    new Exercise { ID = 2, Title = "ExerciseTitle2" }
                },
                RowCount = 2
            };
        }

        private PagedResult<Training> GetTrainingsPaged()
        {
            return new PagedResult<Training>
            {
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 10,
                Results = new List<Training>
                {
                    new Training { ID = 1, Date=DateTime.Parse("2021-08-02") },
                    new Training { ID = 2, Date=DateTime.Parse("2021-08-02") }
                },
                RowCount = 2
            };
        }
    }
}
