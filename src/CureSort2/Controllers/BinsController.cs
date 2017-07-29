using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CureSort2.Data;
using CureSort2.Models;
using Microsoft.AspNetCore.Authorization;

namespace CureSort2.Controllers
{
    [Authorize(Roles = "Administrator")]
    public class BinsController : Controller
    {
        private readonly CureContext _context;

        public BinsController(CureContext context)
        {
            _context = context;    
        }

        // GET: Bins
        public async Task<IActionResult> Index()
        {
            return View(await _context.Bins.ToListAsync());
        }

        // GET: Bins/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bin = await _context.Bins.SingleOrDefaultAsync(m => m.BinID == id);
            if (bin == null)
            {
                return NotFound();
            }

            return View(bin);
        }

        // GET: Bins/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Bins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("BinID,BinNumber,InUse,Name")] Bin bin)
        {
            if (ModelState.IsValid)
            {
                _context.Add(bin);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(bin);
        }

        // GET: Bins/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bin = await _context.Bins.SingleOrDefaultAsync(m => m.BinID == id);
            if (bin == null)
            {
                return NotFound();
            }
            return View(bin);
        }

        // POST: Bins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("BinID,BinNumber,InUse,Name")] Bin bin)
        {
            if (id != bin.BinID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(bin);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!BinExists(bin.BinID))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }
            return View(bin);
        }

        // GET: Bins/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var bin = await _context.Bins.SingleOrDefaultAsync(m => m.BinID == id);
            if (bin == null)
            {
                return NotFound();
            }

            return View(bin);
        }

        // POST: Bins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var bin = await _context.Bins.SingleOrDefaultAsync(m => m.BinID == id);
            _context.Bins.Remove(bin);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool BinExists(int id)
        {
            return _context.Bins.Any(e => e.BinID == id);
        }
    }
}
