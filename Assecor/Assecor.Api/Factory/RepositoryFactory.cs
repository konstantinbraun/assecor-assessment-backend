using System;
using Assecor.DAL.Data;
using Assecor.DAL.Interfaces;
using Assecor.DAL.Repositories;
using Microsoft.Extensions.Configuration;

namespace Assecor.Api.Factory
{
    public class RepositoryFactory : IRepositoryFactory
    {
        private readonly PersonContext _context;
        private readonly IReader _fileReader;
        private readonly IWriter _fileWriter;
        private readonly IConfiguration _configuration;
        public RepositoryFactory(PersonContext context, IReader fileReader, IWriter fileWriter, IConfiguration configuration)
        {
            _context = context;
            _configuration = configuration;
            _fileWriter = fileWriter;
            _fileReader = fileReader;
        }
        public IPersonRepository GetRepository()
        {
            switch (_configuration["Repository"])
            {
                case "Db": return new DbPersonRepository(_context);
                case "File": return new FilePersonRepository(_fileReader, _fileWriter, _configuration);
                default: throw new NotImplementedException();
            }
        }
    }
}
