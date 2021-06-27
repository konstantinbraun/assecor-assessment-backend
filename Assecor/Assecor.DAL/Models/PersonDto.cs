using System.Text.Json.Serialization;

namespace Assecor.DAL.Models
{
    public class PersonDto
    {
        public string Name { get; set; }
        [JsonPropertyName("lastname")]
        public string LastName { get; set; }
        [JsonPropertyName("zipcode")]
        public string ZipCode { get; set; }
        public string City { get; set; }
        public Color Color { get; set; }
    }
}
