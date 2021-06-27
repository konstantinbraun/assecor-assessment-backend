using System;
using Assecor.DAL.Interfaces;
using Assecor.DAL.Models;
using Microsoft.Extensions.Configuration;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Assecor.DAL.Repositories
{
    public class FilePersonRepository: IPersonRepository
    {
        private readonly List<Person> _persons;
        private readonly IWriter _fileWriter;
        private readonly string _fileName;

        public FilePersonRepository(IReader fileReader, IWriter fileWriter, IConfiguration configuration)
        {
            _fileWriter = fileWriter;
            _fileName = configuration["FileName"];
            try
            {
                _persons = fileReader.GetData(()=> File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), _fileName)));
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                _persons = new List<Person>();
            }

        }

        public async Task<List<Person>> GetPersonsAsync()
        {
            return await Task.FromResult(_persons);
        }

        public async Task<Person> GetPersonAsync(int id)
        {
            return await Task.FromResult(_persons.Single(x => x.Id == id));
        }

        public async Task<List<Person>> GetPersonsByColorAsync(Color color)
        {
            return await Task.FromResult(_persons.Where(x=>x.Color == color).ToList());
        }

        public async Task<Person> AddPersonAsync(PersonDto personDto)
        {
            var person = new Person
            {
                City = personDto.City, ZipCode = personDto.ZipCode, LastName = personDto.LastName,
                Name = personDto.Name, Color = personDto.Color
            };
            _fileWriter.SaveData(line =>
            {
                File.AppendAllText(Path.Combine(Directory.GetCurrentDirectory(), _fileName), line);
                var lines = File.ReadAllLines(Path.Combine(Directory.GetCurrentDirectory(), _fileName));
                return lines.Length;
            }, person);

            _persons.Add(person);
            return await Task.FromResult(person);
        }
    }
}
