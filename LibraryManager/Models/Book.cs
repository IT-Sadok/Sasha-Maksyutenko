using System;
using System.Collections.Generic;
using System.Text;

namespace LibraryManager.Models
{
    public class Book
    {
        public string Id { get; set; } = string.Empty;
        public string? Title { get; set; }
        public string? Author { get; set; }
        public int Year { get; set; }
        public bool IsAvailable { get; set; } = true;
        public string Status => IsAvailable ? "Free" : "Available";
    }
}
