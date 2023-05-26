using System;
using System.ComponentModel;

namespace LandyBook.Models
{
    public class RentalDTO
    {

        public int Id { get; set; }

        [DisplayName("Rent Date")]
        public DateTime RentDate { get; set; }

        [DisplayName("Return Date")]
        public DateTime ReturnDate { get; set; }

    }
}

