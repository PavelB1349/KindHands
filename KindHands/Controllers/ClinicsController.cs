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
    public class ClinicsController : Controller
    {
        private readonly KindHandsContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ClinicsController(KindHandsContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Clinics
        public async Task<IActionResult> Index()
        {
              return View(await _context.Clinics.ToListAsync());
        }

        // GET: Clinics/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Clinics == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinics
                .FirstOrDefaultAsync(m => m.Id == id);
            
            if (clinic == null)
            {
                return NotFound();
            }

            return View(clinic);
        }

        // GET: Clinics/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Clinics/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Create))]
        public async Task<IActionResult> Create([Bind("Name,City,Address,Description,Phone,Website,Email")] Clinic clinic)
        {
            if (ModelState.IsValid)
            {
                var form = await Request.ReadFormAsync();
                var file = form.Files["Photo"];
                if (file != null)
                {
                    var photoPath = "/uploads/clinics";
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
                        clinic.PhotoPath = uploadPath;
                    }
                }
                _context.Add(clinic);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(clinic);
        }

        // GET: Clinics/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Clinics == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic == null)
            {
                return NotFound();
            }
            return View(clinic);
        }

        // POST: Clinics/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Name,City,Address,Description,Phone,Website,Id,DateCreated")] Clinic clinic)
        {
            if (id != clinic.Id)
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
                        var photoPath = "/uploads/clinics";
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
                            clinic.PhotoPath = uploadPath;
                        }
                    }
                    _context.Update(clinic);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!ClinicExists(clinic.Id))
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
            return View(clinic);
        }

        // GET: Clinics/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Clinics == null)
            {
                return NotFound();
            }

            var clinic = await _context.Clinics
                .FirstOrDefaultAsync(m => m.Id == id);
            if (clinic == null)
            {
                return NotFound();
            }

            return View(clinic);
        }

        // POST: Clinics/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Clinics == null)
            {
                return Problem("Entity set 'KindHandsContext.Clinics'  is null.");
            }
            var clinic = await _context.Clinics.FindAsync(id);
            if (clinic != null)
            {
                _context.Clinics.Remove(clinic);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool ClinicExists(int id)
        {
          return _context.Clinics.Any(e => e.Id == id);
        }
    }
}
