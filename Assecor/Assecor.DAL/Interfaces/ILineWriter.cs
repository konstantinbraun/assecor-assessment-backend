using Assecor.DAL.Models;

namespace Assecor.DAL.Interfaces
{
    public interface ILineWriter
    {
        string GetLine(Person person);
    }
}
