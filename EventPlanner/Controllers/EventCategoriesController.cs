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
    public class EventCategoriesController : Controller
    {
        private readonly EventPlannerContext _context;

        public EventCategoriesController(EventPlannerContext context)
        {
            _context = context;
        }

        // GET: EventCategories
        public async Task<IActionResult> Index()
        {
            var eventPlannerContext = _context.EventCategory.Include(e => e.Category).Include(e => e.Event);
            return View(await eventPlannerContext.ToListAsync());
        }

        // GET: EventCategories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventCategory = await _context.EventCategory
                .Include(e => e.Category)
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eventCategory == null)
            {
                return NotFound();
            }

            return View(eventCategory);
        }

        // GET: EventCategories/Create
        public IActionResult Create()
        {
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "ID", "ID");
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "ID");
            return View();
        }

        // POST: EventCategories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,CategoryID,EventID")] EventCategory eventCategory)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventCategory);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "ID", "ID", eventCategory.CategoryID);
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "ID", eventCategory.EventID);
            return View(eventCategory);
        }

        // GET: EventCategories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventCategory = await _context.EventCategory.FindAsync(id);
            if (eventCategory == null)
            {
                return NotFound();
            }
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "ID", "ID", eventCategory.CategoryID);
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "ID", eventCategory.EventID);
            return View(eventCategory);
        }

        // POST: EventCategories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,CategoryID,EventID")] EventCategory eventCategory)
        {
            if (id != eventCategory.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventCategory);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventCategoryExists(eventCategory.ID))
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
            ViewData["CategoryID"] = new SelectList(_context.Set<Category>(), "ID", "ID", eventCategory.CategoryID);
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "ID", eventCategory.EventID);
            return View(eventCategory);
        }

        // GET: EventCategories/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventCategory = await _context.EventCategory
                .Include(e => e.Category)
                .Include(e => e.Event)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eventCategory == null)
            {
                return NotFound();
            }

            return View(eventCategory);
        }

        // POST: EventCategories/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventCategory = await _context.EventCategory.FindAsync(id);
            _context.EventCategory.Remove(eventCategory);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventCategoryExists(int id)
        {
            return _context.EventCategory.Any(e => e.ID == id);
        }
    }
}
