using System;
using Assecor.DAL.Models;
using System.Collections.Generic;

namespace Assecor.DAL.Interfaces
{
    public interface IReader
    {
        ILineReader LineReader { get; set; }
        List<Person> GetData(Func<string[]> delGetLines) ;
    }
}