using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trainer.Core.Repository.ClientRepo;
using Trainer.Controllers;
using Xunit;
using Trainer.Core.Repository;
using Trainer.Models;
using Trainer.Core.Repository.ExerciseRepo;

namespace Trainer.UnitTests.ControllerTests
{
    public class ClientControllerTests
    {
        private readonly Mock<IBaseRepository<Client>> _clientRepositoryMock;
        private readonly ClientsController _clientsController;

        public ClientControllerTests()
        {
            _clientRepositoryMock = new Mock<IBaseRepository<Client>>();
            //_clientsController = new ClientsController(_clientRepositoryMock.Object);
        }
        

        [Fact]
        public void Test1()
        {

        }
    }
}
