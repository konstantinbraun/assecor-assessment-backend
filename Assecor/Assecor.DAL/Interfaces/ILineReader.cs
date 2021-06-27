using Assecor.DAL.Models;

namespace Assecor.DAL.Interfaces
{
    public interface ILineReader
    {
        Person GetPerson(int index, string line);
    }
}