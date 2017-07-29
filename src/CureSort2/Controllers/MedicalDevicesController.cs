using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CureSort2.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace CureSort2.Controllers
{
    public class MedicalDevicesController : Controller
    {
        private readonly CureContext _context;
        private IHostingEnvironment _environment;

        public MedicalDevicesController(CureContext context, IHostingEnvironment environment)
        {
            _environment = environment;
            _context = context;    
        }

        // GET: MedicalDevices
        public async Task<IActionResult> Index(string barcode)
        {
                var cureContext = from m in _context.MedicalDevices.Include(m => m.Bin)
                                  select m;

            if (User.IsInRole("Administrator") && String.IsNullOrEmpty(barcode))
            {
                    cureContext = cureContext.Where(m => (m.IsApproved.Equals(false)));
            }
            else if (User.IsInRole("Administrator") && !String.IsNullOrEmpty(barcode))
            {
                cureContext = cureContext.Where(m => m.IsApproved.Equals(false) && m.Barcode.Contains(barcode) || m.Description.Contains(barcode) || m.Brand.Contains(barcode));
            }
            else if (String.IsNullOrEmpty(barcode))
            {
                cureContext = cureContext.Where(m => (m.IsApproved.Equals(true)));
            }
            else
            {
                cureContext = cureContext.Where(m => m.IsApproved.Equals(true) && m.Barcode.Contains(barcode) || m.Description.Contains(barcode) || m.Brand.Contains(barcode));
            }

            if (barcode == null && !User.IsInRole("Administrator"))
            {
                cureContext = from m in _context.MedicalDevices.Include(m => m.Bin)
                                  where m.ID.Equals("?")
                                  select m;
            }

            return View(await cureContext.ToListAsync());
        }

        // GET: MedicalDevices/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalDevice = await _context.MedicalDevices.Include(c => c.Bin).SingleOrDefaultAsync(m => m.ID == id);
            if (medicalDevice == null)
            {
                return NotFound();
            }

            return View(medicalDevice);
        }

        // GET: MedicalDevices/Create
        public IActionResult Create()
        {
            PopulateBinsDropDownList();
            return View();
        }

        // POST: MedicalDevices/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("ID,Barcode,BinID,Brand,CreatedBy,Description,IsApproved,Manufacturer,PhotoUrl")] MedicalDevice medicalDevice, IFormFile file)
        {
            if (ModelState.IsValid)
            {
                if (file != null)
                {
                    var uploads = Path.Combine(_environment.WebRootPath, "uploads");
                    if (file.Length > 0)
                    {
                        using (var fileStream = new FileStream(Path.Combine(uploads, file.FileName), FileMode.Create))
                        {
                            await file.CopyToAsync(fileStream);
                        }
                        medicalDevice.PhotoUrl = "uploads" + '/' + file.FileName;
                    }
                }
                medicalDevice.CreatedBy = User.Identity.Name;
                medicalDevice.IsApproved = false;
                _context.Add(medicalDevice);
                await _context.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            PopulateBinsDropDownList(medicalDevice.BinID);
            return View(medicalDevice);
        }

        // GET: MedicalDevices/Edit/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalDevice = await _context.MedicalDevices.SingleOrDefaultAsync(m => m.ID == id);
            if (medicalDevice == null)
            {
                return NotFound();
            }
            PopulateBinsDropDownList(medicalDevice.BinID);
            return View(medicalDevice);
        }

        // POST: MedicalDevices/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Edit(int id, [Bind("ID,Barcode,BinID,Brand,CreatedBy,Description,IsApproved,Manufacturer,PhotoUrl")] MedicalDevice medicalDevice)
        {
            if (id != medicalDevice.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                medicalDevice.CreatedBy = User.Identity.Name;
                try
                {
                    _context.Update(medicalDevice);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!MedicalDeviceExists(medicalDevice.ID))
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
            PopulateBinsDropDownList(medicalDevice.BinID);
            return View(medicalDevice);
        }

        private void PopulateBinsDropDownList(object selectedBin = null)
        {
            var binsQuery = from b in _context.Bins
                                   orderby b.BinNumber
                                   select b;
            ViewBag.BinID = new SelectList(binsQuery.AsNoTracking(), "BinID", "BinNumber", selectedBin);
        }

        // GET: MedicalDevices/Delete/5
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var medicalDevice = await _context.MedicalDevices.SingleOrDefaultAsync(m => m.ID == id);
            if (medicalDevice == null)
            {
                return NotFound();
            }

            return View(medicalDevice);
        }


        // POST: MedicalDevices/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = "Administrator")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var medicalDevice = await _context.MedicalDevices.SingleOrDefaultAsync(m => m.ID == id);
            _context.MedicalDevices.Remove(medicalDevice);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
        }

        private bool MedicalDeviceExists(int id)
        {
            return _context.MedicalDevices.Any(e => e.ID == id);
        }
    }
}
