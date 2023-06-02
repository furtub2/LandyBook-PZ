using System;
using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace LandyBook.Models
{
    public class Rental
    {
        public int ID { get; set; }
        public int BookId { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }


        [Display(Name = "Rental date")]
        public DateTime RentDate { get; set; }

        [Display(Name = "Return date")]
        public DateTime ReturnDate { get; set; }

        public bool isAccepted { get; set; }
        public string Status { get; set; }
        public bool IsCancelled { get; set; }
    }
}

