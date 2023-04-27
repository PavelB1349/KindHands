using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using KindHands.Data;
using KindHands.Models;
using Microsoft.AspNetCore.Authorization;
using KindHands.Services;
using Microsoft.AspNetCore.SignalR;

namespace KindHands.Controllers
{
    public class AdvertisementsController : Controller
    {
        private readonly KindHandsContext _context;
        private readonly DefaultUserProvider _defaultUserProvider;


        public AdvertisementsController(KindHandsContext context, DefaultUserProvider defaultUserProvider)
        {
            _context = context;
            this._defaultUserProvider = defaultUserProvider;

        }

        // GET: Advertisements

        public async Task<IActionResult> Index()
        {
            var kindHandsContext = _context.Advertisements.Include(a => a.Animal)
             .Include(a => a.User);
            
            return View(await kindHandsContext.ToListAsync());
        }

        // GET: UserAdvertisements
        [Authorize]
        public async Task<IActionResult> UserAdvertisementIndex()
        {
            var kindHandsContext = _context.Advertisements.Include(a => a.Animal)
             .Include(a => a.User)
            //выводим только с id пользователя
            .Where(x => x.UserId == _defaultUserProvider.CurrentUserId())
            ;
            return View(await kindHandsContext.ToListAsync());
        }

        // GET: Advertisements/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Advertisements == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .Include(a => a.Animal)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // GET: Advertisements/Create
        [Authorize]
        public IActionResult Create()
        {
            
            ViewData["AnimalName"] = new SelectList(_context.Animals.Where(x => x.UserId == _defaultUserProvider.CurrentUserId()), "Id", "Name");
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id");
            ViewData["UserId"] = _defaultUserProvider.CurrentUserId();
            return View();
        }

        // POST: Advertisements/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Description,UserId,AnimalId")] Advertisement advertisement)
        {
            if (ModelState.IsValid)
            {
                _context.Add(advertisement);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            //ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id", advertisement.AnimalId);
            //ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", advertisement.UserId);
            return View(advertisement);
        }

        // GET: Advertisements/Edit/5
        //[Authorize(Roles = "ROLE_ADMIN, ROLE_MODERATOR")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Advertisements == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements.FindAsync(id);
            if (advertisement == null)
            {
                return NotFound();
            }
            ViewData["AnimalName"] = new SelectList(_context.Animals.Where(x => x.UserId == _defaultUserProvider.CurrentUserId()), "Id", "Name");
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id", advertisement.AnimalId);
            ViewData["UserId"] = _defaultUserProvider.CurrentUserId();
            return View(advertisement);
        }

        // POST: Advertisements/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        
        // Сделать по ИД 

        //[Authorize(Roles = "ROLE_ADMIN, ROLE_MODERATOR")]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Description,UserId,AnimalId,Id,DateCreated")] Advertisement advertisement)
        {
            if (id != advertisement.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(advertisement);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AdvertisementExists(advertisement.Id))
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
            ViewData["AnimalId"] = new SelectList(_context.Animals, "Id", "Id", advertisement.AnimalId);
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", advertisement.UserId);
            return View(advertisement);
        }

        // GET: Advertisements/Delete/5
        //[Authorize(Roles = "ROLE_ADMIN, ROLE_MODERATOR")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Advertisements == null)
            {
                return NotFound();
            }

            var advertisement = await _context.Advertisements
                .Include(a => a.Animal)
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (advertisement == null)
            {
                return NotFound();
            }

            return View(advertisement);
        }

        // POST: Advertisements/Delete/5
        //[Authorize(Roles = "ROLE_ADMIN, ROLE_MODERATOR")]
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Advertisements == null)
            {
                return Problem("Entity set 'KindHandsContext.Advertisements'  is null.");
            }
            var advertisement = await _context.Advertisements.FindAsync(id);
            if (advertisement != null)
            {
                _context.Advertisements.Remove(advertisement);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AdvertisementExists(int id)
        {
          return _context.Advertisements.Any(e => e.Id == id);
        }
    }
}
