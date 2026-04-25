using System.ComponentModel.DataAnnotations;

namespace hack2gether.Models
{
    public class Club
    {
        [Key]
        public int Id { get; set; }   // PRIMARY KEY
        public string name { get; set; }
        public string description { get; set; }
        public int adminId { get; set; }   // <-- REQUIRED
        public User admin { get; set; }
    }
}