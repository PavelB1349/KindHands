using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KindHands.Data;
using KindHands.Models;

namespace KindHands.Controllers
{
    public class SheltersController : Controller
    {
        private readonly KindHandsContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public SheltersController(KindHandsContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Shelters
        public async Task<IActionResult> Index()
        {
              return View(await _context.Shelter.ToListAsync());
        }

        // GET: Shelters/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Shelter == null)
            {
                return NotFound();
            }

            var shelter = await _context.Shelter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shelter == null)
            {
                return NotFound();
            }

            return View(shelter);
        }

        // GET: Shelters/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Shelters/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Name,City,Address,Phone,Email,Description,Id,DateCreated")] Shelter shelter)
        {
            if (ModelState.IsValid)
            {
                var form = await Request.ReadFormAsync();
                var file = form.Files["Photo"];
                if (file != null)
                {
                    var photoPath = "/uploads/shelters";
                    var fileName = file.FileName;
                    var fileExt = Path.GetExtension(fileName);
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath + photoPath))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + photoPath);
                    }
                    var uploadPath = Path.Combine(photoPath, $"{Guid.NewGuid()}{fileExt}");

                    using (var filestream = new FileStream(_webHostEnvironment.WebRootPath + uploadPath, FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                        shelter.PhotoPath = uploadPath;
                    }
                }
                _context.Add(shelter);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(shelter);
        }

        // GET: Shelters/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Shelter == null)
            {
                return NotFound();
            }

            var shelter = await _context.Shelter.FindAsync(id);
            if (shelter == null)
            {
                return NotFound();
            }
            return View(shelter);
        }

        // POST: Shelters/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,City,Address,Phone,Email,Description,Id,DateCreated")] Shelter shelter)
        {
            if (id != shelter.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var form = await Request.ReadFormAsync();
                    var file = form.Files["Photo"];
                    if (file != null)
                    {
                        var photoPath = "/uploads/shelters";
                        var fileName = file.FileName;
                        var fileExt = Path.GetExtension(fileName);
                        if (!Directory.Exists(_webHostEnvironment.WebRootPath + photoPath))
                        {
                            Directory.CreateDirectory(_webHostEnvironment.WebRootPath + photoPath);
                        }
                        var uploadPath = Path.Combine(photoPath, $"{Guid.NewGuid()}{fileExt}");

                        using (var filestream = new FileStream(_webHostEnvironment.WebRootPath + uploadPath, FileMode.Create))
                        {
                            await file.CopyToAsync(filestream);
                            shelter.PhotoPath = uploadPath;
                        }
                    }
                    _context.Update(shelter);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ShelterExists(shelter.Id))
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
            return View(shelter);
        }

        // GET: Shelters/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Shelter == null)
            {
                return NotFound();
            }

            var shelter = await _context.Shelter
                .FirstOrDefaultAsync(m => m.Id == id);
            if (shelter == null)
            {
                return NotFound();
            }

            return View(shelter);
        }

        // POST: Shelters/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Shelter == null)
            {
                return Problem("Entity set 'KindHandsContext.Shelter'  is null.");
            }
            var shelter = await _context.Shelter.FindAsync(id);
            if (shelter != null)
            {
                _context.Shelter.Remove(shelter);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ShelterExists(int id)
        {
          return _context.Shelter.Any(e => e.Id == id);
        }
    }
}
