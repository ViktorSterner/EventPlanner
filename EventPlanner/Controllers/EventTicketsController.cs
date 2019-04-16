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
    public class EventTicketsController : Controller
    {
        private readonly EventPlannerContext _context;

        public EventTicketsController(EventPlannerContext context)
        {
            _context = context;
        }

        // GET: EventTickets
        public async Task<IActionResult> Index()
        {
            var eventPlannerContext = _context.EventTicket.Include(e => e.Event).Include(e => e.User);
            return View(await eventPlannerContext.ToListAsync());
        }

        // GET: EventTickets/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventTicket = await _context.EventTicket
                .Include(e => e.Event)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eventTicket == null)
            {
                return NotFound();
            }

            return View(eventTicket);
        }

        // GET: EventTickets/Create
        public IActionResult Create()
        {
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "ID");
            ViewData["UserID"] = new SelectList(_context.User, "ID", "ID");
            return View();
        }

        // POST: EventTickets/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,UserID,EventID,Active,PurchaseTime,Price")] EventTicket eventTicket)
        {
            if (ModelState.IsValid)
            {
                _context.Add(eventTicket);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "ID", eventTicket.EventID);
            ViewData["UserID"] = new SelectList(_context.User, "ID", "ID", eventTicket.UserID);
            return View(eventTicket);
        }

        // GET: EventTickets/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventTicket = await _context.EventTicket.FindAsync(id);
            if (eventTicket == null)
            {
                return NotFound();
            }
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "ID", eventTicket.EventID);
            ViewData["UserID"] = new SelectList(_context.User, "ID", "ID", eventTicket.UserID);
            return View(eventTicket);
        }

        // POST: EventTickets/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("ID,UserID,EventID,Active,PurchaseTime,Price")] EventTicket eventTicket)
        {
            if (id != eventTicket.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(eventTicket);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!EventTicketExists(eventTicket.ID))
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
            ViewData["EventID"] = new SelectList(_context.Event, "ID", "ID", eventTicket.EventID);
            ViewData["UserID"] = new SelectList(_context.User, "ID", "ID", eventTicket.UserID);
            return View(eventTicket);
        }

        // GET: EventTickets/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var eventTicket = await _context.EventTicket
                .Include(e => e.Event)
                .Include(e => e.User)
                .FirstOrDefaultAsync(m => m.ID == id);
            if (eventTicket == null)
            {
                return NotFound();
            }

            return View(eventTicket);
        }

        // POST: EventTickets/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var eventTicket = await _context.EventTicket.FindAsync(id);
            _context.EventTicket.Remove(eventTicket);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool EventTicketExists(int id)
        {
            return _context.EventTicket.Any(e => e.ID == id);
        }
    }
}
