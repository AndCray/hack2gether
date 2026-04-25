using System;
using System.ComponentModel.DataAnnotations;

namespace hack2gether.Models
{
    public class Attendance
    {
        [Key]
        public int Id { get; set; }   // PRIMARY KEY

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int StudentId { get; set; }
        public User Student { get; set; }

        public DateTime Timestamp { get; set; }
    }
}