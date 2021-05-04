using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using RepositoryPatternDemo.Models;

namespace RepositoryPatternDemo.Controllers
{
    public class NeighbourhoodsController : Controller
    {
        private readonly RepositoryPatternDemoDBContext _context;

        public NeighbourhoodsController(RepositoryPatternDemoDBContext context)
        {
            _context = context;
        }

        // GET: Neighbourhoods
        public async Task<IActionResult> Index()
        {
            return View(await _context.Neighbourhoods.ToListAsync());
        }

        // GET: Neighbourhoods/Details/5
        public async Task<IActionResult> Details(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var neighbourhood = await _context.Neighbourhoods
                .FirstOrDefaultAsync(m => m.Neighbourhood1 == id);
            if (neighbourhood == null)
            {
                return NotFound();
            }

            return View(neighbourhood);
        }

        // GET: Neighbourhoods/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Neighbourhoods/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("NeighbourhoodGroup,Neighbourhood1")] Neighbourhood neighbourhood)
        {
            if (ModelState.IsValid)
            {
                _context.Add(neighbourhood);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(neighbourhood);
        }
        

        // GET: Neighbourhoods/Delete/5
        public async Task<IActionResult> Delete(string id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var neighbourhood = await _context.Neighbourhoods
                .FirstOrDefaultAsync(m => m.Neighbourhood1 == id);
            if (neighbourhood == null)
            {
                return NotFound();
            }

            return View(neighbourhood);
        }

        // POST: Neighbourhoods/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(string id)
        {
            var neighbourhood = await _context.Neighbourhoods.FindAsync(id);
            _context.Neighbourhoods.Remove(neighbourhood);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool NeighbourhoodExists(string id)
        {
            return _context.Neighbourhoods.Any(e => e.Neighbourhood1 == id);
        }
    }
}
