using LandyBook.Data;
using LandyBook.Models;
using LandyBook.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace LandyBook.Controllers.Admin
{
    [Authorize(Roles = "SuperAdmin")]
    public class AdminRentalsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _userManager;

        public AdminRentalsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<IActionResult> Index()
        {
            IdentityUser user = await _userManager.FindByIdAsync("c64fe004-2784-4733-bc9f-5afddb458225");

            var rentals = await _context.Rental
                .Select(rental => new
                {
                    id = rental.ID,
                    book = _context.Book.Where(book => book.ID == rental.BookId).Select(book => book.Title).FirstOrDefault(),
                    rentDate = rental.RentDate.ToShortDateString(),
                    returnDate = rental.ReturnDate.ToShortDateString(),
                    status = rental.Status,
                    isAccepted = rental.isAccepted,
                    isCancelled = rental.IsCancelled,
                    user = rental.User
                })
                .ToListAsync();

            var rentalsWithUser = rentals.Select(async rental => new
            {
                id = rental.id,
                book = rental.book,
                rentDate = rental.rentDate,
                returnDate = rental.returnDate,
                status = rental.status,
                user = await _userManager.FindByIdAsync(rental.user.Id),
                isAccepted = rental.isAccepted,
                isCancelled = rental.isCancelled
            });

            ViewBag.Rentals = await Task.WhenAll(rentalsWithUser);
            return View();
        }







        public async Task<IActionResult> Accept(int? id)
        {
            var rental = _context.Rental.Where(rental => rental.ID == id).FirstOrDefault();
            if (rental == null)
            {
                return NotFound();
            }
            rental.isAccepted = true;
            rental.Status = "Accepted";
            if (ModelState.IsValid)
            {
                _context.Update(rental);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
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
                return RedirectToAction("Index");
            }
            return RedirectToAction("Index");
        }
    }
}