using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CureSort2.Data;
using CureSort2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Routing;

namespace CureSort2.Controllers
{
    public class FlagsController : Controller
    {
        private readonly CureContext _context;
        private readonly ApplicationDbContext _appcontext;

        public FlagsController(CureContext context, ApplicationDbContext appcontext)
        {
            _context = context;
            _appcontext = appcontext;
        }

        // GET: Flags
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Index(string sortOrder, string searchString)
        {
            ViewData["DateSubmittedSortParm"] = String.IsNullOrEmpty(sortOrder) ? "datesub_desc" : "";
            ViewData["ActiveSortParm"] = sortOrder == "IsActive" ? "isactive_desc" : "IsActive";
            ViewData["ReviewedBySortParm"] = sortOrder == "ReviewedBy" ? "reviewedby_desc" : "ReviewedBy";
            ViewData["CurrentFilter"] = searchString;


            var flags = from f in _context.Flags.Include(f => f.MedicalDevice)
                           select f;

            if(!String.IsNullOrEmpty(searchString))
            {
                flags = flags.Where(f => f.Comments.Contains(searchString) || f.Name.Contains(searchString) ||
                f.Problem.Contains(searchString) || f.Warehouse.Contains(searchString) ||
                f.ReviewedBy.Contains(searchString));
            }

            switch (sortOrder)
            {
                case "datesub_desc":
                    flags = flags.OrderBy(f => f.DateSubmitted);
                    break;
                case "IsActive":
                    flags = flags.OrderBy(f => f.IsActive);
                    break;
                case "isactive_desc":
                    flags = flags.OrderByDescending(f => f.IsActive);
                    break;
                case "ReviewedBy":
                    flags = flags.OrderBy(f => f.IsActive);
                    break;
                case "reviewedby_desc":
                    flags = flags.OrderByDescending(f => f.IsActive);
                    break;
                default:
                    flags = flags.OrderByDescending(f => f.DateSubmitted);
                    break;
            }

            return View(await flags.ToListAsync());
        }

        // GET: Flags/Details/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flag = await _context.Flags.SingleOrDefaultAsync(m => m.FlagID == id);
            if (flag == null)
            {
                return NotFound();
            }

            return View(flag);
        }

        // GET: Flags/Create
        public IActionResult Create()
        {

            return View();
        }

        // POST: Flags/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("FlagID,Comments,IsActive,MedicalDeviceID,Name,Problem,Warehouse,ReviewedBy,DateSubmitted,DateReviewed")] Flag flag, int id)
        {
            flag.MedicalDeviceID = id;
            flag.ReviewedBy = "";
            flag.IsActive = true;
            flag.DateSubmitted = DateTime.UtcNow;
            flag.DateReviewed = DateTime.MinValue;

            if(flagExists(id).Any())
            {
                ViewBag.Error = "A problem has already been reported for this item and it is under review.";
            }
            else if (ModelState.IsValid || id != 0)
            {
                _context.Add(flag);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index", new RouteValueDictionary(
    new { controller = "medicaldevices", action = "Index" }));
            }
            else
            {
                ViewBag.Error = "No Medical Device ID found or state is invalid.";
            }
            return View(flag);
        }

        // GET: Flags/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flag = await _context.Flags.SingleOrDefaultAsync(m => m.FlagID == id);
            if (flag == null)
            {
                return NotFound();
            }
            ViewData["MedicalDeviceID"] = new SelectList(_context.MedicalDevices, "ID", "ID", flag.MedicalDeviceID);
            return View(flag);
        }


        public IQueryable<Flag> flagExists(int id)
        {
            var flagQuery = from b in _context.Flags
                            where b.MedicalDeviceID == id && b.IsActive == true
                            select b;
            return flagQuery;
        }

        // POST: Flags/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("FlagID,Comments,IsActive,MedicalDeviceID,Name,Problem,Warehouse,ReviewedBy,DateSubmitted,DateReviewed")] Flag flag)
        {
            if (id != flag.FlagID)
            {
                return NotFound();
            }
            flag.DateReviewed = DateTime.UtcNow;
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(flag);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FlagExists(flag.FlagID))
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
            ViewData["MedicalDeviceID"] = new SelectList(_context.MedicalDevices, "ID", "ID", flag.MedicalDeviceID);
            return View(flag);
        }

        // GET: Flags/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var flag = await _context.Flags.SingleOrDefaultAsync(m => m.FlagID == id);
            if (flag == null)
            {
                return NotFound();
            }

            return View(flag);
        }

        // POST: Flags/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var flag = await _context.Flags.SingleOrDefaultAsync(m => m.FlagID == id);
            _context.Flags.Remove(flag);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool FlagExists(int id)
        {
            return _context.Flags.Any(e => e.FlagID == id);
        }
    }
}
