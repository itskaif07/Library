using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Models;
using System.Net.Http;

namespace Library.Controllers
{
    public class ApisController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly HttpClient _httpClient;

        public ApisController(ApplicationDbContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        // GET: Apis
        public async Task<IActionResult> Index()
        {
            return View(await _context.Apis.ToListAsync());
        }

        // GET: Apis/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (string.IsNullOrWhiteSpace(id))
            {
                return NotFound();
            }

            var response = await _httpClient.GetAsync($"https://www.googleapis.com/books/v1/volumes/{id}");
            if (response.IsSuccessStatusCode)
            {
                var bookDetails = await response.Content.ReadFromJsonAsync<Api>();

                if (bookDetails != null) // Ensure bookDetails is not null
                {
                    return View(bookDetails); // Pass the Api model to the view
                }
            }

            return NotFound(); // Return NotFound if response fails or bookDetails is null
        }






        // GET: Apis/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ExternalId,Title,Authors,Description,BookCover")] Api api)
        {
            if (ModelState.IsValid)
            {
                _context.Add(api);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(api);
        }

        // GET: Apis/Edit/5
        public async Task<IActionResult> Edit(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var api = await _context.Apis.FindAsync(id);
            if (api == null)
            {
                return NotFound();
            }
            return View(api);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(string id, [Bind("ExternalId,Title,Authors,Description,BookCover")] Api api)
        {
            if (id != api.ExternalId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(api);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ApiExists(api.ExternalId))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(api);
        }

        // GET: Apis/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var api = await _context.Apis
                .FirstOrDefaultAsync(m => m.ExternalId == id);
            if (api == null)
            {
                return NotFound();
            }

            return View(api);
        }

        // POST: Apis/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var api = await _context.Apis.FindAsync(id);
            if (api != null)
            {
                _context.Apis.Remove(api);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ApiExists(string id)
        {
            return _context.Apis.Any(e => e.ExternalId == id);
        }
    }
}
