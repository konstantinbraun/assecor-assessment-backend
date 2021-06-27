using Assecor.DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace Assecor.DAL.Data
{
    public class PersonContext: DbContext
    {
        public PersonContext(DbContextOptions<PersonContext> options) : base(options)
        {
        }
        public DbSet<Person> Persons { get; set; }
    }
}
