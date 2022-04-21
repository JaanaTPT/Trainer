using AutoMapper;
using Moq;
using System.Threading.Tasks;
using Trainer.Core.IConfiguration;
using Trainer.Core.Repository.ExerciseRepo;
using Trainer.Data;
using Trainer.Models;
using Trainer.Models.ViewModels;
using Trainer.Services;
using Xunit;

namespace Trainer.UnitTests.ServiceTests
{
    public class ExerciseServiceTests
    {
        private readonly Mock<IExerciseRepository> _exerciseRepositoryMock;
        private readonly Mock<IUnitOfWork> _unitOfWorkMock;
        private readonly ExerciseService _exerciseService;

        public ExerciseServiceTests()
        {
            _exerciseRepositoryMock = new Mock<IExerciseRepository>();
            _unitOfWorkMock = new Mock<IUnitOfWork>();

            var mapperConfig = new MapperConfiguration(cfg =>
            {
                cfg.AddMaps(typeof(Program).Assembly);
            });
            var mapper = mapperConfig.CreateMapper();

            _unitOfWorkMock.SetupGet(uow => uow.ExerciseRepository)
                           .Returns(_exerciseRepositoryMock.Object);

            _exerciseService = new ExerciseService(_unitOfWorkMock.Object, mapper);
        }

        [Fact]
        public async Task List_returns_paged_list_of_exercise_models()
        {
            // Arrange
            int page = 1;
            int pageSize = 10;
            _exerciseRepositoryMock.Setup(er => er.GetPagedList(page, pageSize, "", ""))
                                  .ReturnsAsync(() => new PagedResult<Exercise>())
                                  .Verifiable();

            // Act
            var result = await _exerciseService.GetPagedList(page, pageSize, "", "");

            // Assert
            Assert.NotNull(result);
            Assert.IsType<PagedResult<ExerciseModel>>(result);
            _exerciseRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task GetById_should_return_null_if_exercise_was_not_found()
        {
            // Arrange
            var nonExistentId = -1;
            var nullExercise = (Exercise)null;
            _exerciseRepositoryMock.Setup(er => er.GetById(nonExistentId))
                                  .ReturnsAsync(() => nullExercise)
                                  .Verifiable();

            // Act
            var result = await _exerciseService.GetById(nonExistentId);

            // Assert
            Assert.Null(result);
            _exerciseRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task GetById_should_return_exercise()
        {
            // Arrange
            var id = 1;
            var exercise = new Exercise { ID = id };
            _exerciseRepositoryMock.Setup(er => er.GetById(id))
                                  .ReturnsAsync(() => exercise)
                                  .Verifiable();

            // Act
            var result = await _exerciseService.GetById(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ExerciseModel>(result);
            _exerciseRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task GetForEdit_should_return_null_if_exercise_was_not_found()
        {
            // Arrange
            var nonExistentId = -1;
            var nullExercise = (Exercise)null;
            _exerciseRepositoryMock.Setup(er => er.GetById(nonExistentId))
                                  .ReturnsAsync(() => nullExercise)
                                  .Verifiable();

            // Act
            var result = await _exerciseService.GetForEdit(nonExistentId);

            // Assert
            Assert.Null(result);
            _exerciseRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task GetForEdit_should_return_exercise()
        {
            // Arrange
            var id = 1;
            var exercise = new Exercise { ID = id };
            _exerciseRepositoryMock.Setup(er => er.GetById(id))
                                  .ReturnsAsync(() => exercise)
                                  .Verifiable();

            // Act
            var result = await _exerciseService.GetForEdit(id);

            // Assert
            Assert.NotNull(result);
            Assert.IsType<ExerciseEditModel>(result);
            _exerciseRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Save_should_survive_null_model()
        {
            // Arrange
            var model = (ExerciseEditModel)null;

            // Act
            var response = await _exerciseService.Save(model);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
        }

        [Fact]
        public async Task Save_should_handle_missing_exercise()
        {
            //Arrange
            var id = 1;
            var exercise = new ExerciseEditModel { ID = id };
            var nullExercise = (Exercise)null;
            _exerciseRepositoryMock.Setup(er => er.GetById(id))
                                  .ReturnsAsync(() => nullExercise)
                                  .Verifiable();

            //Act
            var response = await _exerciseService.Save(exercise);

            //Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
        }

        [Fact]
        public async Task Save_should_save_valid_exercise()
        {
            // Arrange
            var id = 1;
            var exercise = new Exercise { ID = id };
            var exerciseModel = new ExerciseEditModel { ID = id };

            _exerciseRepositoryMock.Setup(er => er.GetById(id))
                                  .ReturnsAsync(() => exercise)
                                  .Verifiable();
            _exerciseRepositoryMock.Setup(er => er.Save(It.IsAny<Exercise>()))
                                  .Verifiable();
            _unitOfWorkMock.Setup(uow => uow.CommitAsync())
                           .Verifiable();

            // Act
            var response = await _exerciseService.Save(exerciseModel);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            _exerciseRepositoryMock.VerifyAll();
            _unitOfWorkMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_should_survive_null_model()
        {
            // Arrange
            var model = (ExerciseModel)null;

            // Act
            var response = await _exerciseService.Delete(model);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
        }

        [Fact]
        public async Task Delete_handles_null_exercise()
        {
            // Arrange
            var id = 1;
            var exerciseModelToDelete = new ExerciseModel { ID = id };
            var exerciseToDelete = (Exercise)null;

            _exerciseRepositoryMock.Setup(er => er.GetById(id))
                                  .ReturnsAsync(() => exerciseToDelete)
                                  .Verifiable();

            // Act
            var response = await _exerciseService.Delete(exerciseModelToDelete);

            // Assert
            Assert.NotNull(response);
            Assert.False(response.Success);
            _exerciseRepositoryMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_deletes_exercise()
        {
            // Arrange
            var id = 1;
            var exerciseModelToDelete = new ExerciseModel { ID = id };
            var exerciseToDelete = new Exercise { ID = id };

            _exerciseRepositoryMock.Setup(er => er.GetById(id))
                                  .ReturnsAsync(() => exerciseToDelete)
                                  .Verifiable();
            _exerciseRepositoryMock.Setup(er => er.Delete(id))
                                  .Verifiable();
            _unitOfWorkMock.Setup(uow => uow.CommitAsync())
                           .Verifiable();

            // Act
            var response = await _exerciseService.Delete(exerciseModelToDelete);

            // Assert
            Assert.NotNull(response);
            Assert.True(response.Success);
            _exerciseRepositoryMock.VerifyAll();
            _unitOfWorkMock.VerifyAll();
        }
    }
}
