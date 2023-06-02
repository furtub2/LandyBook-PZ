using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LandyBook.Data;
using LandyBook.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LandyBook.Controllers.User
{
    [Authorize]
    public class RentalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public RentalsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SelectDate([Bind("Id, RentDate, ReturnDate")] RentalDTO rentalDTO)
        {
            if (ModelState.IsValid)
            {
                return RedirectToAction("SelectBook", rentalDTO);
            }
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> SelectBook(RentalDTO rentalDTO)
        {
            ViewBag.RentDate = rentalDTO.RentDate;
            ViewBag.ReturnDate = rentalDTO.ReturnDate;

            var rentedBooks = _context.Rental
                .Where(rental => !rental.IsCancelled &&
                    ((rentalDTO.RentDate <= rental.RentDate && rentalDTO.ReturnDate >= rental.RentDate)
                    || (rentalDTO.RentDate <= rental.ReturnDate && rentalDTO.ReturnDate >= rental.ReturnDate)
                    || (rentalDTO.RentDate >= rental.RentDate && rentalDTO.ReturnDate <= rental.ReturnDate)))
                .Select(rental => rental.BookId)
                .Distinct()
                .ToList();
            foreach (var book in rentedBooks)
            {
                Console.WriteLine(book);
            }

            var availableBooks = _context.Book
                .Where(book => !rentedBooks.Contains(book.ID) && book.IsAvailable)
                .ToList();

            return View(availableBooks);
        }

        public async Task<IActionResult> RentBook(int bookID, DateTime RentDate, DateTime ReturnDate)
        {
            IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);

            TimeSpan timeSpan = ReturnDate - RentDate;
            Rental rental = new Rental();
            rental.BookId = bookID;
            rental.UserId = user.Id;
            rental.RentDate = RentDate;
            rental.ReturnDate = ReturnDate;
            rental.Status = "Pending";
            rental.isAccepted = false;
            rental.IsCancelled = false;
            if (ModelState.IsValid)
            {
                _context.Add(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction("MyRentals");
            }
            return RedirectToAction("MyRentals");
        }

        public async Task<IActionResult> MyRentals()
        {
            IdentityUser user = await _userManager.FindByNameAsync(User.Identity.Name);
            ViewBag.UserRentals = _context.Rental
                .Where(rental => rental.User == user)
                .Select(Rental => new
                {
                    id = Rental.ID,
                    book = _context.Book.Where(book => book.ID == Rental.BookId).Select(book => book.Title).FirstOrDefault().ToString(),
                    rentDate = Rental.RentDate.ToShortDateString(),
                    returnDate = Rental.ReturnDate.ToShortDateString(),
                    status = Rental.Status
                }).ToList();
            return View();
        }

        public async Task<IActionResult> Cancel(int? id)
        {
            var rental = _context.Rental.Where(rental => rental.ID == id).FirstOrDefault();
            if (rental == null)
            {
                return NotFound();
            }
            rental.IsCancelled = true;
            rental.Status = "Canceled";
            if (ModelState.IsValid)
            {
                _context.Update(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction("MyRentals");
            }
            return RedirectToAction("MyRentals");
        }

        public async Task<IActionResult> Remove(int? id)
        {
            var rental = _context.Rental.Where(rental => rental.ID == id).FirstOrDefault();

            if (rental == null)
            {
                return NotFound();
            }

            rental.Status = "Removed";

            if (ModelState.IsValid)
            {
                _context.Update(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction("MyRentals");
            }
            return RedirectToAction("MyRentals");
        }
    }
}

