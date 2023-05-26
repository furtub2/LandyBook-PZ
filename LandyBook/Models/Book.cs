using System;
using System.ComponentModel;

namespace LandyBook.Models
{
    public class Book
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }

        [DefaultValue(true)]
        public bool IsAvailable { get; set; }
    }
}

