using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KindHands.Data;
using KindHands.Models;
using KindHands.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;

namespace KindHands.Controllers
{
    public class VolunterAnnouncementsController : Controller
    {
        private readonly KindHandsContext _context;
        private readonly DefaultUserProvider _defaultUserProvider;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public VolunterAnnouncementsController(KindHandsContext context, DefaultUserProvider defaultUserProvider, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this._defaultUserProvider = defaultUserProvider;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: VolunterAnnouncements
        public async Task<IActionResult> Index()
        {
            var kindHandsContext = _context.VolunterAnnouncements.Include(v => v.User);
            return View(await kindHandsContext.ToListAsync());
        }

        // GET: VolunterAnnouncements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.VolunterAnnouncements == null)
            {
                return NotFound();
            }

            var volunterAnnouncement = await _context.VolunterAnnouncements
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volunterAnnouncement == null)
            {
                return NotFound();
            }

            return View(volunterAnnouncement);
        }

        // GET: VolunterAnnouncements/Create
        [Authorize]
        public IActionResult Create()
        {
            ViewData["UserId"] = _defaultUserProvider.CurrentUserId();
            return View();
        }

        // POST: VolunterAnnouncements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,City,Phone,Email,UserId,PhotoPath")] VolunterAnnouncement volunterAnnouncement)
        {
            if (ModelState.IsValid)
            {
                var form = await Request.ReadFormAsync();
                var file = form.Files["Photo"];
                if (file != null)
                {
                    var photoPath = "/uploads/volunterAnnouncements";
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
                        volunterAnnouncement.PhotoPath = uploadPath;
                    }
                }
                _context.Add(volunterAnnouncement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", volunterAnnouncement.UserId);
            return View(volunterAnnouncement);
        }

        // GET: VolunterAnnouncements/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.VolunterAnnouncements == null)
            {
                return NotFound();
            }

            var volunterAnnouncement = await _context.VolunterAnnouncements.FindAsync(id);
            if (volunterAnnouncement == null)
            {
                return NotFound();
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", volunterAnnouncement.UserId);
            return View(volunterAnnouncement);
        }

        // POST: VolunterAnnouncements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,City,Phone,Email,UserId,Id,DateCreated")] VolunterAnnouncement volunterAnnouncement)
        {
            if (id != volunterAnnouncement.Id)
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
                        var photoPath = "/uploads/volunterAnnouncements";
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
                            volunterAnnouncement.PhotoPath = uploadPath;
                        }
                    }
                    _context.Update(volunterAnnouncement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!VolunterAnnouncementExists(volunterAnnouncement.Id))
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
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", volunterAnnouncement.UserId);
            return View(volunterAnnouncement);
        }

        // GET: VolunterAnnouncements/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.VolunterAnnouncements == null)
            {
                return NotFound();
            }

            var volunterAnnouncement = await _context.VolunterAnnouncements
                .Include(v => v.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (volunterAnnouncement == null)
            {
                return NotFound();
            }

            return View(volunterAnnouncement);
        }

        // POST: VolunterAnnouncements/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.VolunterAnnouncements == null)
            {
                return Problem("Entity set 'KindHandsContext.VolunterAnnouncements'  is null.");
            }
            var volunterAnnouncement = await _context.VolunterAnnouncements.FindAsync(id);
            if (volunterAnnouncement != null)
            {
                _context.VolunterAnnouncements.Remove(volunterAnnouncement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool VolunterAnnouncementExists(int id)
        {
          return _context.VolunterAnnouncements.Any(e => e.Id == id);
        }
    }
}
