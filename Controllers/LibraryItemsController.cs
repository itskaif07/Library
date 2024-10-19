using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Library.Data;
using Library.Models;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Mvc.Infrastructure;

namespace Library.Controllers
{
    public class LibraryItemsController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment _hostEnvironment;

        public LibraryItemsController(ApplicationDbContext context, IWebHostEnvironment hostEnvironment)
        {
            _context = context;
            _hostEnvironment = hostEnvironment;
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
                return RedirectToAction(nameof(CreatedBooks));
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,BookTitle,BookContent,BookCoverFile,BookCoverImage,AuthorName,Description")] LibraryItem libraryItem)
        {
            if (id != libraryItem.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    LibraryItem existingItem = await _context.libraryItems.AsNoTracking().FirstOrDefaultAsync(x => x.Id == id);

                    // Handle file upload
                    if (libraryItem.BookCoverFile != null && libraryItem.BookCoverFile.Length > 0)
                    {
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", libraryItem.BookCoverFile.FileName);

                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await libraryItem.BookCoverFile.CopyToAsync(stream);
                        }

                        libraryItem.BookCoverImage = "/images/" + libraryItem.BookCoverFile.FileName;
                    }
                    else if (string.IsNullOrEmpty(libraryItem.BookCoverImage))
                    {
                        // If the image was removed
                        libraryItem.BookCoverImage = null;
                    }
                    else
                    {
                        // Preserve existing cover image if no new image is uploaded
                        libraryItem.BookCoverImage = existingItem.BookCoverImage;
                    }

                    _context.Entry(libraryItem).State = EntityState.Modified;
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
                return RedirectToAction(nameof(CreatedBooks));
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
            return RedirectToAction(nameof(CreatedBooks));
        }

        [HttpGet]

        public async Task<IActionResult> CreatedBooks()
        {
            return View(await _context.libraryItems.ToListAsync());
        }

        private bool LibraryItemExists(int id)
        {
            return _context.libraryItems.Any(e => e.Id == id);
        }
    }
}
