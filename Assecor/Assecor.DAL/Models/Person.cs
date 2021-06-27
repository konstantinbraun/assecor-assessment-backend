namespace Assecor.DAL.Models
{
    public class Person:PersonDto
    {
        public  int Id { get; set; }
    }
    public enum Color
    {
        blau = 1,
        grün,
        violett,
        rot,
        gelb,
        türkis,
        weiß
    }
}
