using System.Collections.Generic;
using System.Threading.Tasks;
using Assecor.DAL.Models;

namespace Assecor.DAL.Interfaces
{
    public interface IPersonRepository
    {
        Task<List<Person>> GetPersonsAsync();
        Task<Person> GetPersonAsync(int id);
        Task<List<Person>> GetPersonsByColorAsync(Color color);
        Task<Person> AddPersonAsync(PersonDto person);
    }
}
