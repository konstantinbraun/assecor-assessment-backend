using Assecor.DAL.Data;
using Assecor.DAL.Interfaces;
using Assecor.DAL.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Assecor.DAL.Repositories
{
    public class DbPersonRepository : IPersonRepository
    {
        private readonly PersonContext _dbContext;
        public DbPersonRepository(PersonContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<List<Person>> GetPersonsAsync()
        {
            return await _dbContext.Persons.ToListAsync();
        }

        public async Task<Person> GetPersonAsync(int id)
        {
            return await _dbContext.Persons.SingleOrDefaultAsync(x=> x.Id == id);
        }

        public async Task<List<Person>> GetPersonsByColorAsync(Color color)
        {
            return await _dbContext.Persons.Where(x=>x.Color == color).ToListAsync();
        }

        public async Task<Person> AddPersonAsync(PersonDto personDto)
        {
            var person = new Person
            {
                City = personDto.City,
                ZipCode = personDto.ZipCode,
                LastName = personDto.LastName,
                Name = personDto.Name,
                Color = personDto.Color
            };

            _dbContext.Persons.Add(person);
            await _dbContext.SaveChangesAsync();
            return person;
        }
    }
}
