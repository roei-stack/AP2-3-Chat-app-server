using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using BorisKnowsAllApi.Data;
using Domain;

namespace BorisKnowsAllApi.Controllers
{
    public class OurMessagesDBController : Controller
    {
        private readonly BorisKnowsAllApiContext _context;

        public OurMessagesDBController(BorisKnowsAllApiContext context)
        {
            _context = context;
        }

        // GET: OurMessagesDB
        public async Task<IActionResult> Index()
        {
              return View(await _context.OurMessage.ToListAsync());
        }

        // GET: OurMessagesDB/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null || _context.OurMessage == null)
            {
                return NotFound();
            }

            var ourMessage = await _context.OurMessage
                .FirstOrDefaultAsync(m => m.key == id);
            if (ourMessage == null)
            {
                return NotFound();
            }

            return View(ourMessage);
        }

        // GET: OurMessagesDB/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: OurMessagesDB/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("key,id,content,sent,created")] OurMessage ourMessage)
        {
            if (ModelState.IsValid)
            {
                _context.Add(ourMessage);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(ourMessage);
        }

        // GET: OurMessagesDB/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null || _context.OurMessage == null)
            {
                return NotFound();
            }

            var ourMessage = await _context.OurMessage.FindAsync(id);
            if (ourMessage == null)
            {
                return NotFound();
            }
            return View(ourMessage);
        }

        // POST: OurMessagesDB/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("key,id,content,sent,created")] OurMessage ourMessage)
        {
            if (id != ourMessage.key)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(ourMessage);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!OurMessageExists(ourMessage.key))
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
            return View(ourMessage);
        }

        // GET: OurMessagesDB/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null || _context.OurMessage == null)
            {
                return NotFound();
            }

            var ourMessage = await _context.OurMessage
                .FirstOrDefaultAsync(m => m.key == id);
            if (ourMessage == null)
            {
                return NotFound();
            }

            return View(ourMessage);
        }

        // POST: OurMessagesDB/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            if (_context.OurMessage == null)
            {
                return Problem("Entity set 'BorisKnowsAllApiContext.OurMessage'  is null.");
            }
            var ourMessage = await _context.OurMessage.FindAsync(id);
            if (ourMessage != null)
            {
                _context.OurMessage.Remove(ourMessage);
            }
            
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool OurMessageExists(int id)
        {
          return _context.OurMessage.Any(e => e.key == id);
        }
    }
}
