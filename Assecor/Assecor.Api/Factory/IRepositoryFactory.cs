using Assecor.DAL.Interfaces;

namespace Assecor.Api.Factory
{
    public interface IRepositoryFactory
    {
        IPersonRepository GetRepository();
    }
}
