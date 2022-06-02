using System.Net;
using Book_Store.Data;
using Book_Store.Models;
using Microsoft.AspNetCore.Mvc;

namespace livraria.Controllers
{     
    public class BookStoreController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BookStoreController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var books = _context.Books?.ToList();
            return View(books);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> Create(Book book)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Books?.Add(book);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Index");
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError(string.Empty, $"Algo deu errado {ex.Message}");
                }
            }
            ModelState.AddModelError(string.Empty, $"Algo deu errado, modelo inválido");
            return View(book);
        }   

        [Route("BookStore/Delete/{id}")]      
        public async Task<IActionResult> Delete(int id)
        {
            var book = _context.Books?.Find(id);

            if (book == null) {
                return new StatusCodeResult(StatusCodes.Status404NotFound);
            }

            _context.Books?.Remove(book);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}