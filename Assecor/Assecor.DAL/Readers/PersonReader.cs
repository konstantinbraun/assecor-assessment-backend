using System;
using System.Collections.Generic;
using Assecor.DAL.Interfaces;
using Assecor.DAL.Models;

namespace Assecor.DAL.Readers
{
    public class PersonReader: IReader
    {
        public PersonReader(ILineReader lineReader)
        {
            LineReader = lineReader;
        }

        public ILineReader LineReader { get; set; }

        public List<Person> GetData(Func<string[]> delGetLines)
        {
            var persons = new List<Person>();
            var i = 1;
            string[] lines = delGetLines();

            foreach (var line in lines)
            {
                var person = LineReader.GetPerson(i, line);
                if (person!=null)
                    persons.Add(person);
                i += 1;
            }
            return persons;
        }

    }
}
