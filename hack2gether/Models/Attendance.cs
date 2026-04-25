using System;
using System.ComponentModel.DataAnnotations;

namespace hack2gether.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        public int EventId { get; set; }
        public Event Event { get; set; }

        public int StudentId { get; set; }
        public User Student { get; set; }

        public DateTime Timestamp { get; set; }
        public int PointsAwarded { get; set; }
    }
}