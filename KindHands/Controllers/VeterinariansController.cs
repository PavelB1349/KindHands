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
    public class VeterinariansController : Controller
    {
        private readonly KindHandsContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public VeterinariansController(KindHandsContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Veterinarians
        public async Task<IActionResult> Index()
        {
            var kindHandsContext = _context.Veterinarians.Include(v => v.Clinic);
            return View(await kindHandsContext.ToListAsync());
        }

        // GET: Veterinarians/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Veterinarians == null)
            {
                return NotFound();
            }

            var veterinarian = await _context.Veterinarians
                .Include(v => v.Clinic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veterinarian == null)
            {
                return NotFound();
            }

            return View(veterinarian);
        }

        // GET: Veterinarians/Create
        public IActionResult Create()
        {
            ViewData["ClinicId"] = new SelectList(_context.Clinics, "Id", "Name");
            return View();
        }

        // POST: Veterinarians/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FirstName,LastName,PatronumicName,Speciality,City,Phone,Email,ClinicId")] Veterinarian veterinarian)
        {
            if (ModelState.IsValid)
            {
                var form = await Request.ReadFormAsync();
                var file = form.Files["Photo"];
                if (file != null)
                {
                    var photoPath = "/uploads/veterinarians";
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
                        veterinarian.PhotoPath = uploadPath;
                    }
                }
                _context.Add(veterinarian);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
           
            return View(veterinarian);
        }

        // GET: Veterinarians/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Veterinarians == null)
            {
                return NotFound();
            }

            var veterinarian = await _context.Veterinarians.FindAsync(id);
            if (veterinarian == null)
            {
                return NotFound();
            }
            ViewData["ClinicId"] = new SelectList(_context.Clinics, "Id", "Id", veterinarian.ClinicId);
            ViewData["ClinicName"] = new SelectList(_context.Clinics, "Id", "Name", veterinarian.ClinicId);
            return View(veterinarian);
        }

        // POST: Veterinarians/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("FirstName,LastName,PatronumicName,Speciality,City,Phone,Email,ClinicId,Id,DateCreated")] Veterinarian veterinarian)
        {
            if (id != veterinarian.Id)
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
                        var photoPath = "/uploads/veterinarians";
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
                            veterinarian.PhotoPath = uploadPath;
                        }
                    }
                    _context.Update(veterinarian);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VeterinarianExists(veterinarian.Id))
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
            ViewData["ClinicId"] = new SelectList(_context.Clinics, "Id", "Id", veterinarian.ClinicId);
            ViewData["ClinicName"] = new SelectList(_context.Clinics, "Id", "Name", veterinarian.ClinicId);

            return View(veterinarian);
        }

        // GET: Veterinarians/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Veterinarians == null)
            {
                return NotFound();
            }

            var veterinarian = await _context.Veterinarians
                .Include(v => v.Clinic)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (veterinarian == null)
            {
                return NotFound();
            }

            return View(veterinarian);
        }

        // POST: Veterinarians/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Veterinarians == null)
            {
                return Problem("Entity set 'KindHandsContext.Veterinarians'  is null.");
            }
            var veterinarian = await _context.Veterinarians.FindAsync(id);
            if (veterinarian != null)
            {
                _context.Veterinarians.Remove(veterinarian);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VeterinarianExists(int id)
        {
          return _context.Veterinarians.Any(e => e.Id == id);
        }
    }
}
