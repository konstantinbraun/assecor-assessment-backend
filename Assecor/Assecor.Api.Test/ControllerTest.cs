using Assecor.Api.Controllers;
using Assecor.Api.Factory;
using Assecor.DAL.Interfaces;
using Assecor.DAL.Models;
using Moq;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Xunit;

namespace Assecor.Api.Test
{
    public class ControllerTest
    {
        private readonly List<Person> _persons;
        private readonly ILogger<PersonsController> _logger;
        public ControllerTest()
        {
            _persons = new List<Person>
            {
                new Person
                {
                    Id = 1, Name = "Konstantin", LastName = "Braun", City = "Berlin", ZipCode = "12345",
                    Color = Color.blau
                },
                new Person
                {
                    Id = 2, Name = "Inna", LastName = "Braun", City = "Berlin", ZipCode = "12345",
                    Color = Color.rot
                }
            };

            _logger = new Mock<ILogger<PersonsController>>().Object;
        }

        [Fact]
        public async Task RepositoryNotFound_500()
        {
            var factory = new Mock<IRepositoryFactory>();
            factory.Setup(x => x.GetRepository())
                .Returns(null as IPersonRepository);

            var controller = new PersonsController(factory.Object, _logger);
            var result = await controller.Get();

            Assert.IsType<StatusCodeResult>(result.Result);
            Assert.Equal(500, ((StatusCodeResult)result.Result).StatusCode);
        }

        [Fact]
        public async Task GeAllPersons_200()
        {
            var repository = new Mock<IPersonRepository>();
            repository.Setup(x => x.GetPersonsAsync())
                .Returns(Task.FromResult(_persons));

            var factory = new Mock<IRepositoryFactory>();
            factory.Setup(x => x.GetRepository())
                .Returns(repository.Object);

            var controller = new PersonsController(factory.Object, _logger);
            var result = await controller.Get();

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, ((OkObjectResult)result.Result).StatusCode);
            Assert.IsType<List<Person>>(((OkObjectResult)result.Result).Value);
        }

        [Fact]
        public async Task GePersonById_ReturnPerson_200()
        {
            var repository = new Mock<IPersonRepository>();
            repository.Setup(x => x.GetPersonAsync(It.IsAny<int>()))
                .ReturnsAsync(_persons[0]);

            var factory = new Mock<IRepositoryFactory>();
            factory.Setup(x => x.GetRepository())
                .Returns(repository.Object);

            var controller = new PersonsController(factory.Object, _logger);
            var result = await controller.Get(1);

            Assert.IsType<OkObjectResult>(result.Result);
            Assert.Equal(200, ((OkObjectResult)result.Result).StatusCode);
            Assert.IsType<Person>(((OkObjectResult)result.Result).Value);
        }

        [Fact]
        public async Task GePersonById_ReturnNotFound_404()
        {
            var repository = new Mock<IPersonRepository>();
            repository.Setup(x => x.GetPersonAsync(It.IsAny<int>()))
               .ReturnsAsync(null as Person);

            var factory = new Mock<IRepositoryFactory>();
            factory.Setup(x => x.GetRepository())
                .Returns(repository.Object);

            var controller = new PersonsController(factory.Object, _logger);
            var result = await controller.Get(3);

            Assert.IsType<NotFoundResult>(result.Result);
            Assert.Equal(404, ((NotFoundResult)result.Result).StatusCode);
        }

        [Fact]
        public async Task SavePerson_Return_201()
        {
            var repository = new Mock<IPersonRepository>();
            repository.Setup(x => x.AddPersonAsync(It.IsAny<PersonDto>()))
                .ReturnsAsync(new Person { Name = "name", LastName = "lastname", Color = Color.blau, City = "Berlin", ZipCode = "12345", Id = 11});

            var factory = new Mock<IRepositoryFactory>();
            factory.Setup(x => x.GetRepository())
                .Returns(repository.Object);

            var controller = new PersonsController(factory.Object, _logger);
            var result = await controller.Post(new PersonDto{Name = "name", LastName = "lastname", Color =Color.blau, City = "Berlin", ZipCode = "12345"});

            Assert.IsType<CreatedAtActionResult>(result.Result);
            Assert.Equal(201, ((CreatedAtActionResult)result.Result).StatusCode);
            Assert.Equal(11, ((CreatedAtActionResult)result.Result).RouteValues["id"]);
            Assert.IsType<Person>(((CreatedAtActionResult)result.Result).Value);
        }
    }
}
