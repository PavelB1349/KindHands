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

namespace KindHands.Controllers
{
    public class AnimalsController : Controller
    {
        private readonly KindHandsContext _context;
        private readonly DefaultUserProvider _defaultUserProvider;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public AnimalsController(KindHandsContext context, DefaultUserProvider defaultUserProvider, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            this._defaultUserProvider = defaultUserProvider;
            _webHostEnvironment = webHostEnvironment;
        }

        // GET: Animals
        public async Task<IActionResult> Index()
        {
            var kindHandsContext = _context.Animals.Include(a => a.User);
            return View(await kindHandsContext.ToListAsync());
        }

        // GET: UserAnimals
        public async Task<IActionResult> UserAnimalsIndex()
        {
            var kindHandsContext = _context.Animals.Include(a => a.User)
                .Where(x => x.UserId == _defaultUserProvider.CurrentUserId())
                ;
            return View(await kindHandsContext.ToListAsync());
        }


        // GET: Animals/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // GET: Animals/Create
        public IActionResult Create()
        {
            ViewData["UserId"] = _defaultUserProvider.CurrentUserId();         
            return View();
        }
     
      
        // POST: Animals/CreateUserAnimal        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ActionName(nameof(Create))]
        public async Task<IActionResult> Create([Bind("Breed,UserId,Kind,Name,Age,Sex,Passport")] Animal animal)
        {
            if (ModelState.IsValid)
            {
                var form = await Request.ReadFormAsync();
                var file = form.Files["Photo"];
                if (file!= null)
                {
                    var photoPath = "/uploads/animals";
                    var fileName = file.FileName;
                    var fileExt = Path.GetExtension(fileName);
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath+photoPath))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + photoPath);
                    }
                    var uploadPath = Path.Combine(photoPath, $"{Guid.NewGuid()}{fileExt}");

                    using (var filestream = new FileStream (_webHostEnvironment.WebRootPath+ uploadPath, FileMode.Create))
                    {
                        await file.CopyToAsync(filestream);
                        animal.PhotoPath = uploadPath;
                    }
                }
                _context.Add(animal);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(UserAnimalsIndex));
            }
           
            return View(animal);
        }

        // GET: Animals/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals.FindAsync(id);
            if (animal == null)
            {
                return NotFound();
            }
       
            return View(animal);
        }

        // POST: Animals/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, /*[Bind("Breed,UserId,Kind,PhotoPath,Id,DateCreated")]*/ Animal animal)
        {
            if (id != animal.Id)
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
                        var photoPath = "/uploads/animals";
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
                            animal.PhotoPath = uploadPath;
                        }
                    }
                    _context.Update(animal);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AnimalExists(animal.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(UserAnimalsIndex));
            }
            ViewData["UserId"] = new SelectList(_context.Users, "Id", "Id", animal.UserId);
            return View(animal);
        }

        // GET: Animals/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.Animals == null)
            {
                return NotFound();
            }

            var animal = await _context.Animals
                .Include(a => a.User)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (animal == null)
            {
                return NotFound();
            }

            return View(animal);
        }

        // POST: Animals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.Animals == null)
            {
                return Problem("Entity set 'KindHandsContext.Animals'  is null.");
            }
            var animal = await _context.Animals.FindAsync(id);
            if (animal != null)
            {
                _context.Animals.Remove(animal);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AnimalExists(int id)
        {
            return _context.Animals.Any(e => e.Id == id);
        }
    }
}
