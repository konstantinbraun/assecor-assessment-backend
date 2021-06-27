using Assecor.DAL.LineBuilder;
using Assecor.DAL.Models;
using Assecor.DAL.Readers;
using System.Collections.Generic;
using Xunit;

namespace Assecor.DAL.Test
{
    public class AccessTest
    {
        private List<string> _lines;
        public AccessTest()
        {
            _lines = new List<string>
            {
                "Müller, Hans, 67742 Lauterecken, 1",
                "Petersen, Peter, 18439 Stralsund, 2",
                "Johnson, Johnny, 88888 made up, 3",
                "Millenium, Milly, 77777 made up too, 4",
                "Müller, Jonas, 32323 Hansstadt, 5",
                "Fujitsu, Tastatur, 42342 Japan, 6",
                "Andersson, Anders, 32132 Schweden - ☀, 2",
                "Bart, Bertram, ",
                "12313 Wasweißich, 1 ",
                "Gerber, Gerda, 76535 Woanders, 3 ",
                "Klaussen, Klaus, 43246 Hierach, 2"
            };

        }
        [Theory]
        [InlineData(0, 1, "Müller", "Hans", "67742", "Lauterecken", Color.blau)]
        [InlineData(1, 2, "Petersen", "Peter", "18439", "Stralsund", Color.grün)]
        [InlineData(3, 4, "Millenium", "Milly", "77777", "made up too", Color.rot)]
        [InlineData(7, 8, "Bart", "Bertram", "12313", "Wasweißich", Color.blau)]
        [InlineData(8, 10, "Gerber", "Gerda", "76535", "Woanders", Color.violett)]
        [InlineData(9, 11, "Klaussen", "Klaus", "43246", "Hierach", Color.grün)]
        public void GettingData(int index, int id, string name, string lastName, string zipCode, string city, Color color)
        {
            var fileReader = new PersonReader(new CsvLineBuilder());
            var items = fileReader.GetData(() => _lines.ToArray());

            Assert.Equal(10, items.Count);

            Assert.Equal(id, items[index].Id);
            Assert.Equal(name, items[index].Name);
            Assert.Equal(lastName, items[index].LastName);
            Assert.Equal(zipCode, items[index].ZipCode);
            Assert.Equal(city, items[index].City);
            Assert.Equal(color, items[index].Color);
        }

        [Theory]
        [InlineData("Konstantin", "Braun", "12345", "Berlin", Color.grün)]
        public void SaveData(string name, string lastName, string zipCode, string city, Color color)
        {
            var fileWriter = new Writers.PersonWriter(new CsvLineBuilder());
            fileWriter.SaveData(str =>
            {
                _lines.Add(str);
                return 1;
            }, new Person { Name = name, LastName = lastName, ZipCode = zipCode, City = city, Color = color });

            Assert.Equal(12, _lines.Count);
            Assert.Equal($"\r\n{lastName}, {name}, {zipCode} {city}, {(int)color}", _lines[11]);
        }
    }
}
