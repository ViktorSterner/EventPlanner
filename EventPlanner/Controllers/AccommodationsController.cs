using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using EventPlanner.Models;

namespace EventPlanner.Controllers
{
    public class AccommodationsController : Controller
    {
        private readonly EventPlannerContext _context;

        public AccommodationsController(EventPlannerContext context)
        {
            _context = context;
        }

        // GET: Accommodations
        public async Task<IActionResult> Index()
        {
            var eventPlannerContext = _context.Accommodation.Include(a => a.Location);
            return View(await eventPlannerContext.ToListAsync());
        }

        // GET: Accommodations/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accommodation = await _context.Accommodation
                .Include(a => a.Location)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (accommodation == null)
            {
                return NotFound();
            }

            return View(accommodation);
        }

        // GET: Accommodations/Create
        public IActionResult Create()
        {
            ViewData["LocationID"] = new SelectList(_context.Set<Location>(), "ID", "ID");
            return View();
        }

        // POST: Accommodations/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,LocationID,Name,Adress,Capacity,Type,Price")] Accommodation accommodation)
        {
            if (ModelState.IsValid)
            {
                _context.Add(accommodation);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["LocationID"] = new SelectList(_context.Set<Location>(), "ID", "ID", accommodation.LocationID);
            return View(accommodation);
        }

        // GET: Accommodations/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accommodation = await _context.Accommodation.FindAsync(id);
            if (accommodation == null)
            {
                return NotFound();
            }
            ViewData["LocationID"] = new SelectList(_context.Set<Location>(), "ID", "ID", accommodation.LocationID);
            return View(accommodation);
        }

        // POST: Accommodations/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,LocationID,Name,Adress,Capacity,Type,Price")] Accommodation accommodation)
        {
            if (id != accommodation.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(accommodation);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!AccommodationExists(accommodation.ID))
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
            ViewData["LocationID"] = new SelectList(_context.Set<Location>(), "ID", "ID", accommodation.LocationID);
            return View(accommodation);
        }

        // GET: Accommodations/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var accommodation = await _context.Accommodation
                .Include(a => a.Location)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (accommodation == null)
            {
                return NotFound();
            }

            return View(accommodation);
        }

        // POST: Accommodations/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var accommodation = await _context.Accommodation.FindAsync(id);
            _context.Accommodation.Remove(accommodation);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AccommodationExists(int id)
        {
            return _context.Accommodation.Any(e => e.ID == id);
        }
    }
}
