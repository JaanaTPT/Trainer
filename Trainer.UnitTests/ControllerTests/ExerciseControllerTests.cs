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
    public class ExerciseControllerTests
    {
        private readonly Mock<IExerciseService> _exerciseServiceMock;
        private readonly ExercisesController _exerciseController;

        public ExerciseControllerTests()
        {
            _exerciseServiceMock = new Mock<IExerciseService>();
            _exerciseController = new ExercisesController(_exerciseServiceMock.Object);
        }

        [Fact]
        public async Task Index_should_return_list_of_exercises()
        {
            // Arrange
            var page = 1;
            var exercises = GetPagedExerciseList();
            _exerciseServiceMock.Setup(es => es.GetPagedList(page, It.IsAny<int>(), "", "")).
                               ReturnsAsync(() => exercises);

            // Act
            var result = await _exerciseController.Index("", "", page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.True(result.Model is PagedResult<ExerciseModel>);
        }

        [Fact]
        public async Task Index_should_return_default_view()
        {
            // Arrange
            var defaultViewNames = new[] { null, "Index" };
            var page = 1;
            var exercises = GetPagedExerciseList();
            _exerciseServiceMock.Setup(es => es.GetPagedList(page, It.IsAny<int>(), "", ""))
                               .ReturnsAsync(() => exercises);

            // Act
            var result = await _exerciseController.Index("", "", page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.ViewName, defaultViewNames);
        }

        [Fact]
        public async Task Index_should_survive_null_model()
        {
            // Arrange
            var page = 1;
            var exercises = GetPagedExerciseList();
            _exerciseServiceMock.Setup(es => es.GetPagedList(page, It.IsAny<int>(), "", "")).
                               ReturnsAsync(() => exercises);

            // Act
            var result = await _exerciseController.Index("", "") as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_not_found_if_id_is_null()
        {
            // Act
            var result = await _exerciseController.Details(null) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_not_found_if_exercise_is_null()
        {
            // Arrange
            var nonExistentId = -1;
            _exerciseServiceMock.Setup(es => es.GetById(It.IsAny<int>()))
                               .ReturnsAsync(() => null);

            // Act
            var result = await _exerciseController.Details(nonExistentId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_returns_correct_result_when_exercise_is_found()
        {
            // Arrange
            var model = GetExercise();
            var defaultViewNames = new[] { null, "Details" };
            _exerciseServiceMock.Setup(es => es.GetById(It.IsAny<int>()))
                               .ReturnsAsync(() => model);

            // Act
            var result = await _exerciseController.Details(model.ID) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.ViewName, defaultViewNames);
            Assert.NotNull(result.Model);
            Assert.IsType<ExerciseModel>(result.Model);
        }

        [Fact]
        public async Task Edit_should_save_exercise_data()
        {
            // Arrange
            var exercise = GetExerciseEdit();
            var response = new OperationResponse();
            _exerciseServiceMock.Setup(es => es.Save(exercise))
                               .ReturnsAsync(() => response)
                               .Verifiable();

            // Act
            var result = await _exerciseController.EditPost(exercise) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            _exerciseServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_ids_does_not_match()
        {
            // Arrange
            var exerciseIdReal = 1;
            var exerciseIdDampered = 2;
            var exercise = new Exercise();
            exercise.ID = exerciseIdDampered;

            // Act
            var result = await _exerciseController.Edit(exerciseIdReal) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_badresult_when_model_is_null()
        {
            // Arrange
            var exercise = (ExerciseEditModel)null;

            // Act
            var result = await _exerciseController.EditPost(exercise) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_stay_on_form_when_model_is_invalid()
        {
            // Arrange
            var defaultViewNames = new[] { null, "Edit" };
            var exerciseId = 1;
            var exercise = new ExerciseEditModel();
            exercise.ID = exerciseId;
            exercise.Title = "012345678901234567890123456789012345678901234567890123456789";

            // Act
            _exerciseController.ModelState.AddModelError("Id", "ERROR");
            var result = await _exerciseController.EditPost(exercise);
            var typedResult = result as ViewResult;

            // Assert
            Assert.NotNull(typedResult);
            Assert.Contains(typedResult.ViewName, defaultViewNames);
            Assert.False(_exerciseController.ModelState.IsValid);
            _exerciseServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_should_return_not_found_if_id_is_null()
        {
            // Arrange
            var exerciseId = (int?)null;

            // Act
            var result = await _exerciseController.Delete(exerciseId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_not_found_when_exercise_does_not_exist()
        {
            // Arrange
            var exerciseId = -100;
            var exercise = (ExerciseModel)null;
            _exerciseServiceMock.Setup(es => es.GetById(It.IsAny<int>()))
                               .ReturnsAsync(() => exercise);

            // Act
            var result = await _exerciseController.Delete(exerciseId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_show_confirmation_page()
        {
            // Arrange
            var defaultViewNames = new[] { null, "Delete" };
            var exercise = GetExercise();
            _exerciseServiceMock.Setup(es => es.GetById(exercise.ID))
                               .ReturnsAsync(() => exercise);

            // Act
            var result = await _exerciseController.Delete(exercise.ID) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.ViewName, defaultViewNames);
            Assert.Equal(exercise, result.Model);
        }

        [Fact]
        public async Task DeleteConfirmed_should_return_not_found_if_exercise_is_null()
        {
            // Arrange
            var nonExistentId = -1;
            _exerciseServiceMock.Setup(es => es.GetById(It.IsAny<int>()))
                               .ReturnsAsync(() => null);

            // Act
            var result = await _exerciseController.DeleteConfirmed(nonExistentId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteConfirmed_should_delete_exercise()
        {
            // Arrange
            var exercise = GetExercise();
            _exerciseServiceMock.Setup(es => es.GetById(exercise.ID))
                               .ReturnsAsync(() => exercise)
                               .Verifiable();
            _exerciseServiceMock.Setup(es => es.Delete(exercise))
                               .Verifiable();

            // Act
            var result = await _exerciseController.DeleteConfirmed(exercise.ID) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            _exerciseServiceMock.VerifyAll();
        }

        private ExerciseModel GetExercise()
        {
            return GetExerciseList()[0];
        }

        private IList<ExerciseModel> GetExerciseList()
        {
            return new List<ExerciseModel>
            {
                new ExerciseModel { ID = 1, Title = "ExerciseTitle1"
                                    //, MuscleGroup ="MuscleGroup1"
                                    },
                new ExerciseModel { ID = 2, Title = "ExerciseTitle2"
                                    //, MuscleGroup = "MuscleGroup2" 
                                    }
            };
        }

        private PagedResult<ExerciseModel> GetPagedExerciseList()
        {
            return new PagedResult<ExerciseModel>
            {
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 10,
                Results = GetExerciseList(),
                RowCount = 2
            };
        }

        private ExerciseEditModel GetExerciseEdit()
        {
            var model = GetExercise();
            var editModel = new ExerciseEditModel();

            editModel.ID = model.ID;
            editModel.Title = model.Title;
            editModel.MuscleGroup = model.MuscleGroup;

            return editModel;
        }
    }
}
