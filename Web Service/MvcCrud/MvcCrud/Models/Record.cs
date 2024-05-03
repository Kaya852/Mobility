using System.ComponentModel.DataAnnotations;



namespace MvcCrud.Models
{
    public class Record
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public string? Surname { get; set; }
        public int Age { get; set; }
    }
}
