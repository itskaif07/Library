using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Models;

namespace Library.Controllers
{
    public class LibraryItemsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public LibraryItemsController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: LibraryItems
        public async Task<IActionResult> Index()
        {
            return View(await _context.libraryItems.ToListAsync());
        }

        // GET: LibraryItems/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryItem = await _context.libraryItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libraryItem == null)
            {
                return NotFound();
            }

            return View(libraryItem);
        }

        // GET: LibraryItems/Create
        public IActionResult Create()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,BookTitle,BookContent,BookCoverImage, BookCoverFile, AuthorName,Description")] LibraryItem libraryItem)
        {
            if (libraryItem.BookCoverFile != null && libraryItem.BookCoverFile.Length > 0)
            {
                
                var folderPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images");

                
                if (!Directory.Exists(folderPath))
                {
                    Directory.CreateDirectory(folderPath);
                }

                
                var fileName = Path.GetFileName(libraryItem.BookCoverFile.FileName);
                var filePath = Path.Combine(folderPath, fileName);

                // Save the file to the folder
                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await libraryItem.BookCoverFile.CopyToAsync(stream);
                }

                
                libraryItem.BookCoverImage = "/images/" + fileName;
            }

            if (ModelState.IsValid)
            {
                _context.Add(libraryItem);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(libraryItem);
        }


        // GET: LibraryItems/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryItem = await _context.libraryItems.FindAsync(id);
            if (libraryItem == null)
            {
                return NotFound();
            }
            return View(libraryItem);
        }

        // POST: LibraryItems/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookTitle,BookContent,BookCoverImage, BookCoverFile, AuthorName,Description")] LibraryItem libraryItem)
        {
            if (id != libraryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(libraryItem);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!LibraryItemExists(libraryItem.Id))
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
            return View(libraryItem);
        }

        // GET: LibraryItems/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libraryItem = await _context.libraryItems
                .FirstOrDefaultAsync(m => m.Id == id);
            if (libraryItem == null)
            {
                return NotFound();
            }

            return View(libraryItem);
        }

        // POST: LibraryItems/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var libraryItem = await _context.libraryItems.FindAsync(id);
            if (libraryItem != null)
            {
                _context.libraryItems.Remove(libraryItem);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool LibraryItemExists(int id)
        {
            return _context.libraryItems.Any(e => e.Id == id);
        }
    }
}
