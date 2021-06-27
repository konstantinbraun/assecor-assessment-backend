using System;
using Assecor.DAL.Interfaces;
using Assecor.DAL.Models;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using Assecor.Api.Factory;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;

namespace Assecor.Api.Controllers
{
    [Route("api/persons")]
    [ApiController]
    public class PersonsController : ControllerBase
    {
        private readonly IPersonRepository _repository;
        private readonly ILogger<PersonsController> _logger;

        public PersonsController(IRepositoryFactory factory, ILogger<PersonsController> logger)
        {
            _logger = logger;
            try
            {
                _repository = factory.GetRepository();
            }
            catch (Exception e)
            {
               logger.LogError(e.Message);
            }
        }

        [HttpGet]
        public async Task<ActionResult<List<Person>>> Get()
        {
            if (_repository == null)
                return StatusCode(StatusCodes.Status500InternalServerError);
            try
            {
                return Ok(await _repository.GetPersonsAsync());
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Person>> Get(int id)
        {
            try
            {
                var person = await _repository.GetPersonAsync(id);

                if (person == null)
                    return NotFound();

                return Ok(person);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpGet("color/{color}")]
        public async Task<ActionResult<List<Person>>> Get(Color color)
        {
            try
            {
                return await _repository.GetPersonsByColorAsync(color);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        [HttpPost]
        public async Task<ActionResult<Person>> Post([FromBody] PersonDto person)
        {
            try
            {
                var savedPerson = await _repository.AddPersonAsync(person);
                return CreatedAtAction(nameof(Get), new { id = savedPerson.Id }, savedPerson);
            }
            catch (Exception e)
            {
                _logger.LogError(e.Message);
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }
    }
}
