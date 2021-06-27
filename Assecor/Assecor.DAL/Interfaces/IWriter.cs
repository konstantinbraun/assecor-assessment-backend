using System;
using Assecor.DAL.Models;

namespace Assecor.DAL.Interfaces
{
    public interface IWriter
    {
        ILineWriter LineWriter { get; set; }
        void SaveData(Func<string, int> delSaveLine, Person person);
    }
}
