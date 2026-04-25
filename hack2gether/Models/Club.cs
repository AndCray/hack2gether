using System.ComponentModel.DataAnnotations;

namespace hack2gether.Models
{
    public class Club
    {
        [Key]
        public int Id { get; set; }   // PRIMARY KEY

        public string Name { get; set; }
        public string Description { get; set; }
    }
}