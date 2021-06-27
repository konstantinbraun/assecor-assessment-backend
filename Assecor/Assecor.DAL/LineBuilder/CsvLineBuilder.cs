using System;
using Assecor.DAL.Interfaces;
using Assecor.DAL.Models;

namespace Assecor.DAL.LineBuilder
{
    public class CsvLineBuilder : ILineReader, ILineWriter
    {
        private readonly string _delimiter;
        private int _previousIndex;

        public CsvLineBuilder()
        {
            _delimiter = ",";
        }

        private string _line= string.Empty;
        public Person GetPerson(int index, string line)
        {
            _line = $"{_line}{line}";

            if (line.TrimEnd().EndsWith(_delimiter))
            {
                if(_previousIndex==0)
                    _previousIndex = index;
                return null;
            }

            var fields = _line.Split(_delimiter);
            _line = string.Empty;

            if (_previousIndex > 0)
            {
                index = _previousIndex;
                _previousIndex = 0;
            }

            var zip = fields[2].Trim().Split(" ")[0].Trim();
            var city = fields[2].Trim().Substring(zip.Length).Trim();

            return new Person() { Id= index, Name= fields[0].Trim(), LastName = fields[1].Trim(), City =city, ZipCode = zip, Color = Enum.Parse<Color> (fields[3].Trim())};
        }

        public string GetLine(Person person)
        {
            return $"{Environment.NewLine}{person.LastName}{_delimiter} {person.Name}{_delimiter} {person.ZipCode} {person.City}{_delimiter} {(int)person.Color}";
        }
    }
}
