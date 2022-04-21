using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
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
    public class TrainingsControllerTests
    {
        private readonly Mock<ITrainingService> _trainingServiceMock;
        private readonly TrainingsController _trainingsController;

        public TrainingsControllerTests()
        {
            _trainingServiceMock = new Mock<ITrainingService>();
            _trainingsController = new TrainingsController(_trainingServiceMock.Object);
        }


        [Fact]
        public async Task Index_should_return_list_of_trainings()
        {
            // Arrange
            var page = 1;
            var trainings = GetPagedClientList();
            _trainingServiceMock.Setup(ts => ts.GetPagedList(page, It.IsAny<int>(), "", "")).
                               ReturnsAsync(() => trainings);

            // Act
            var result = await _trainingsController.Index("", "", page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.NotNull(result.Model);
            Assert.True(result.Model is PagedResult<TrainingModel>);
        }

        [Fact]
        public async Task Index_should_return_default_view()
        {
            // Arrange
            var defaultViewNames = new[] { null, "Index" };
            var page = 1;
            var trainings = GetPagedClientList();
            _trainingServiceMock.Setup(ts => ts.GetPagedList(page, It.IsAny<int>(), "", ""))
                               .ReturnsAsync(() => trainings);

            // Act
            var result = await _trainingsController.Index("", "", page) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.ViewName, defaultViewNames);
        }

        [Fact]
        public async Task Index_should_survive_null_model()
        {
            // Arrange
            var page = 1;
            var trainings = GetPagedClientList();
            _trainingServiceMock.Setup(ts => ts.GetPagedList(page, It.IsAny<int>(), "", "")).
                               ReturnsAsync(() => trainings);

            // Act
            var result = await _trainingsController.Index("", "") as ViewResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_not_found_if_id_is_null()
        {
            // Act
            var result = await _trainingsController.Details(null) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_should_return_not_found_if_training_is_null()
        {
            // Arrange
            var nonExistentId = -1;
            _trainingServiceMock.Setup(ts => ts.GetById(It.IsAny<int>()))
                               .ReturnsAsync(() => null);

            // Act
            var result = await _trainingsController.Details(nonExistentId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Details_returns_correct_result_when_training_is_found()
        {
            // Arrange
            var model = GetTraining();
            var defaultViewNames = new[] { null, "Details" };
            _trainingServiceMock.Setup(ts => ts.GetById(It.IsAny<int>()))
                               .ReturnsAsync(() => model);

            // Act
            var result = await _trainingsController.Details(model.ID) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.ViewName, defaultViewNames);
            Assert.NotNull(result.Model);
            Assert.IsType<TrainingModel>(result.Model);
        }

        [Fact]
        public async Task Edit_should_save_training_data()
        {
            // Arrange
            var training = GetTrainingEdit();
            var response = new OperationResponse();
            _trainingServiceMock.Setup(ts => ts.Save(training))
                               .ReturnsAsync(() => response)
                               .Verifiable();

            // Act
            var result = await _trainingsController.EditPost(training) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            _trainingServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Edit_should_return_notfound_when_ids_does_not_match()
        {
            // Arrange
            var trainingIdReal = 1;
            var trainingIdDampered = 2;
            var training = new Training();
            training.ID = trainingIdDampered;

            // Act
            var result = await _trainingsController.Edit(trainingIdReal) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_return_badresult_when_model_is_null()
        {
            // Arrange
            var training = (TrainingEditModel)null;

            // Act
            var result = await _trainingsController.EditPost(training) as BadRequestResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Edit_should_stay_on_form_when_model_is_invalid()
        {
            // Arrange
            var defaultViewNames = new[] { null, "Edit" };
            var trainingId = 1;
            var training = new TrainingEditModel();
            training.ID = trainingId;
            training.ClientID = 999999999;

            // Act
            _trainingsController.ModelState.AddModelError("Id", "ERROR");
            var result = await _trainingsController.EditPost(training);
            var typedResult = result as ViewResult;

            // Assert
            Assert.NotNull(typedResult);
            Assert.Contains(typedResult.ViewName, defaultViewNames);
            Assert.False(_trainingsController.ModelState.IsValid);
            _trainingServiceMock.VerifyAll();
        }

        [Fact]
        public async Task Delete_should_return_not_found_if_id_is_null()
        {
            // Arrange
            var trainingId = (int?)null;

            // Act
            var result = await _trainingsController.Delete(trainingId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_return_not_found_when_training_does_not_exist()
        {
            // Arrange
            var trainingId = -100;
            var training = (TrainingModel)null;
            _trainingServiceMock.Setup(ts => ts.GetById(It.IsAny<int>()))
                               .ReturnsAsync(() => training);

            // Act
            var result = await _trainingsController.Delete(trainingId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task Delete_should_show_confirmation_page()
        {
            // Arrange
            var defaultViewNames = new[] { null, "Delete" };
            var training = GetTraining();
            _trainingServiceMock.Setup(ts => ts.GetById(training.ID))
                               .ReturnsAsync(() => training);

            // Act
            var result = await _trainingsController.Delete(training.ID) as ViewResult;

            // Assert
            Assert.NotNull(result);
            Assert.Contains(result.ViewName, defaultViewNames);
            Assert.Equal(training, result.Model);
        }

        [Fact]
        public async Task DeleteConfirmed_should_return_not_found_if_training_is_null()
        {
            // Arrange
            var nonExistentId = -1;
            _trainingServiceMock.Setup(ts => ts.GetById(It.IsAny<int>()))
                               .ReturnsAsync(() => null);

            // Act
            var result = await _trainingsController.DeleteConfirmed(nonExistentId) as NotFoundResult;

            // Assert
            Assert.NotNull(result);
        }

        [Fact]
        public async Task DeleteConfirmed_should_delete_training()
        {
            // Arrange
            var training = GetTraining();
            _trainingServiceMock.Setup(ts => ts.GetById(training.ID))
                               .ReturnsAsync(() => training)
                               .Verifiable();
            _trainingServiceMock.Setup(ts => ts.Delete(training))
                               .Verifiable();

            // Act
            var result = await _trainingsController.DeleteConfirmed(training.ID) as RedirectToActionResult;

            // Assert
            Assert.NotNull(result);
            _trainingServiceMock.VerifyAll();
        }

        private TrainingModel GetTraining()
        {
            return GetTrainingList()[0];
        }

        private IList<TrainingModel> GetTrainingList()
        {
            return new List<TrainingModel>
            {
                new TrainingModel { ID = 1, Date=DateTime.Parse("2021-08-02")},
                new TrainingModel { ID = 2, Date=DateTime.Parse("2021-08-03")}
            };
        }

        private PagedResult<TrainingModel> GetPagedClientList()
        {
            return new PagedResult<TrainingModel>
            {
                CurrentPage = 1,
                PageCount = 1,
                PageSize = 10,
                Results = GetTrainingList(),
                RowCount = 2
            };
        }

        private TrainingEditModel GetTrainingEdit()
        {
            var model = GetTraining();
            var editModel = new TrainingEditModel();

            editModel.ID = model.ID;
            editModel.Date = model.Date;

            return editModel;
        }
    }
}
