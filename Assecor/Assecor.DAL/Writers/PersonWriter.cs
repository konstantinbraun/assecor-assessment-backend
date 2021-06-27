using Assecor.DAL.Interfaces;
using Assecor.DAL.Models;
using System;

namespace Assecor.DAL.Writers
{
    public class PersonWriter: IWriter
    {
        public PersonWriter(ILineWriter lineWriter)
        {
            LineWriter = lineWriter;
        }

        public ILineWriter LineWriter { get; set; }

        public void SaveData(Func<string, int> delSaveLine, Person person)
        {
            person.Id = delSaveLine(LineWriter.GetLine(person));
        }
    }
}
