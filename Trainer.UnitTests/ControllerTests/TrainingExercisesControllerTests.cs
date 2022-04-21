using Microsoft.AspNetCore.Mvc;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Trainer.Controllers;
using Trainer.Data;
using Trainer.Models;
using Trainer.Models.ViewModels;
using Trainer.Services;
using Xunit;

namespace Trainer.UnitTests.ControllerTests
{
    public class TrainingExercisesControllerTests
    {
        private readonly Mock<ITrainingExerciseService> _trainingExerciseServiceMock;
        private readonly TrainingExercisesController _trainingExercisesController;

        public TrainingExercisesControllerTests()
        {
            _trainingExerciseServiceMock = new Mock<ITrainingExerciseService>();
            _trainingExercisesController = new TrainingExercisesController(_trainingExerciseServiceMock.Object);
        }


        [Fact]
        public async Task Index_should_return_list_of_trainingExercises()
        {
            // Arrange
            var page = 1;
            var trainingExercises = GetPagedTrainingExerciseList();
            _trainingExerciseServiceMock.Setup(tes => tes.GetPagedList(page, It.IsAny<int>(), "", "")).
                               ReturnsAsync(() => trainingExercises);

            // Act
            var result = await _trainingExercisesController.Index("", "", page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.True(result.Model is PagedResult<TrainingExerciseModel>);
        }

        [Fact]
        public async Task Index_should_return_default_view()
        {
            // Arrange
            var defaultViewNames = new[] { null, "Index" };
            var page = 1;
            var trainingExercises = GetPagedTrainingExerciseList();
            _trainingExerciseServiceMock.Setup(tes => tes.GetPagedList(page, It.IsAny<int>(), "", ""))
                               .ReturnsAsync(() => trainingExercises);

            // Act
            var result = await _trainingExercisesController.Index("", "", page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.ViewName, defaultViewNames);
        }

        [Fact]
        public async Task Index_should_survive_null_model()
        {
            // Arrange
            var page = 1;
            var trainingExercises = GetPagedTrainingExerciseList();
            _trainingExerciseServiceMock.Setup(tes => tes.GetPagedList(page, It.IsAny<int>(), "", "")).
                               ReturnsAsync(() => trainingExercises);

            // Act
            var result = await _trainingExercisesController.Index("", "") as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_not_found_if_id_is_null()
        {
            // Act
            var result = await _trainingExercisesController.Details(null) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_not_found_if_trainingExercise_is_null()
        {
            // Arrange
            var nonExistentId = -1;
            _trainingExerciseServiceMock.Setup(tes => tes.GetById(It.IsAny<int>()))
                               .ReturnsAsync(() => null);

            // Act
            var result = await _trainingExercisesController.Details(nonExistentId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_returns_correct_result_when_trainingExercise_is_found()
        {
            // Arrange
            var model = GetTrainingExercise();
            var defaultViewNames = new[] { null, "Details" };
            _trainingExerciseServiceMock.Setup(tes => tes.GetById(It.IsAny<int>()))
                               .ReturnsAsync(() => model);

            // Act
            var result = await _trainingExercisesController.Details(model.ID) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.ViewName, defaultViewNames);
            Assert.NotNull(result.Model);
            Assert.IsType<TrainingExerciseModel>(result.Model);
        }

        [Fact]
        public async Task Edit_should_save_trainingExercise_data()
        {
            // Arrange
            var trainingExercise = GetTrainingExerciseEdit();
            var response = new OperationResponse();
            _trainingExerciseServiceMock.Setup(tes => tes.Save(trainingExercise))
                               .ReturnsAsync(() => response)
                               .Verifiable();

            // Act
            var result = await _trainingExercisesController.EditPost(trainingExercise) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            _trainingExerciseServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_ids_does_not_match()
        {
            // Arrange
            var trainingExerciseIdReal = 1;
            var trainingExerciseIdDampered = 2;
            var trainingExercise = new TrainingExercise();
            trainingExercise.ID = trainingExerciseIdDampered;

            // Act
            var result = await _trainingExercisesController.Edit(trainingExerciseIdReal) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_badresult_when_model_is_null()
        {
            // Arrange
            var trainingExercise = (TrainingExerciseEditModel)null;

            // Act
            var result = await _trainingExercisesController.EditPost(trainingExercise) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_stay_on_form_when_model_is_invalid()
        {
            // Arrange
            var defaultViewNames = new[] { null, "Edit" };
            var trainingExerciseId = 1;
            var trainingExercise = new TrainingExerciseEditModel();
            trainingExercise.ID = trainingExerciseId;
            trainingExercise.Comments = "012345678901234567890123456789012345678901234567890123456789";

            // Act
            _trainingExercisesController.ModelState.AddModelError("Id", "ERROR");
            var result = await _trainingExercisesController.EditPost(trainingExercise);
            var typedResult = result as ViewResult;

            // Assert
            Assert.NotNull(typedResult);
            Assert.Contains(typedResult.ViewName, defaultViewNames);
            Assert.False(_trainingExercisesController.ModelState.IsValid);
            _trainingExerciseServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_should_return_not_found_if_id_is_null()
        {
            // Arrange
            var trainingExerciseId = (int?)null;

            // Act
            var result = await _trainingExercisesController.Delete(trainingExerciseId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_not_found_when_trainingExercise_does_not_exist()
        {
            // Arrange
            var trainingExerciseId = -100;
            var trainingExercise = (TrainingExerciseModel)null;
            _trainingExerciseServiceMock.Setup(tes => tes.GetById(It.IsAny<int>()))
                               .ReturnsAsync(() => trainingExercise);

            // Act
            var result = await _trainingExercisesController.Delete(trainingExerciseId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_show_confirmation_page()
        {
            // Arrange
            var defaultViewNames = new[] { null, "Delete" };
            var trainingExercise = GetTrainingExercise();
            _trainingExerciseServiceMock.Setup(tes => tes.GetById(trainingExercise.ID))
                               .ReturnsAsync(() => trainingExercise);

            // Act
            var result = await _trainingExercisesController.Delete(trainingExercise.ID) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.ViewName, defaultViewNames);
            Assert.Equal(trainingExercise, result.Model);
        }

        [Fact]
        public async Task DeleteConfirmed_should_return_not_found_if_trainingExercise_is_null()
        {
            // Arrange
            var nonExistentId = -1;
            _trainingExerciseServiceMock.Setup(tes => tes.GetById(It.IsAny<int>()))
                               .ReturnsAsync(() => null);

            // Act
            var result = await _trainingExercisesController.DeleteConfirmed(nonExistentId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteConfirmed_should_delete_trainingExercise()
        {
            // Arrange
            var trainingExercise = GetTrainingExercise();
            _trainingExerciseServiceMock.Setup(tes => tes.GetById(trainingExercise.ID))
                               .ReturnsAsync(() => trainingExercise)
                               .Verifiable();
            _trainingExerciseServiceMock.Setup(tes => tes.Delete(trainingExercise))
                               .Verifiable();

            // Act
            var result = await _trainingExercisesController.DeleteConfirmed(trainingExercise.ID) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            _trainingExerciseServiceMock.VerifyAll();
        }

        private TrainingExerciseModel GetTrainingExercise()
        {
            return GetTrainingExerciseList()[0];
        }

        private IList<TrainingExerciseModel> GetTrainingExerciseList()
        {
            return new List<TrainingExerciseModel>
            {
                new TrainingExerciseModel { ID = 1, TrainingID = 1,  ExerciseID = 1, },
                new TrainingExerciseModel { ID = 2, TrainingID = 2,  ExerciseID = 1, }
            };
        }

        private PagedResult<TrainingExerciseModel> GetPagedTrainingExerciseList()
        {
            return new PagedResult<TrainingExerciseModel>
            {
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 10,
                Results = GetTrainingExerciseList(),
                RowCount = 2
            };
        }

        private TrainingExerciseEditModel GetTrainingExerciseEdit()
        {
            var model = GetTrainingExercise();
            var editModel = new TrainingExerciseEditModel();

            editModel.ID = model.ID;
            editModel.TrainingID = model.TrainingID;
            editModel.ExerciseID = model.ExerciseID;

            return editModel;
        }
    }
}
