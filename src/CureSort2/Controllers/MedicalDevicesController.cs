using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CureSort2.Data;
using CureSort2.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using System.IO;
using Microsoft.AspNetCore.Hosting;
using CureSort2.Controllers;

namespace CureSort2.Controllers
{
    public class MedicalDevicesController : Controller
    {
        private readonly CureContext _context;
        private IHostingEnvironment _environment;
        private IMedicalDeviceLogRepository _mdrepository;

        public MedicalDevicesController(CureContext context, IHostingEnvironment environment, IMedicalDeviceLogRepository mdrepository)
        {
            _environment = environment;
            _context = context;
            _mdrepository = mdrepository;
        }

        // GET: MedicalDevices
        public async Task<IActionResult> Index(string currentFilter, string barcode, int? page)
        {

            if (barcode != null)
            {
                page = 1;
            }
            else
            {
                barcode = currentFilter;
            }
            ViewData["CurrentFilter"] = barcode;
            ViewBag.EmptyList = "";

            var cureContext = from m in _context.MedicalDevices.Include(m => m.Bin)
                              where m.ID.Equals("?")
                              select m;


            if (String.IsNullOrEmpty(barcode) && User.IsInRole("Administrator"))
            {
                cureContext = from m in _context.MedicalDevices.Include(m => m.Bin)
                              where m.IsApproved.Equals(false)
                              select m;
            }
            else if(!String.IsNullOrEmpty(barcode) && User.IsInRole("Administrator"))
            {
                cureContext = from m in _context.MedicalDevices.Include(m => m.Bin)
                              where m.Barcode.Contains(barcode) || m.Description.Contains(barcode) || m.Brand.Contains(barcode) || (m.Brand + " " + m.Description).Contains(barcode)
                              select m;

                if(cureContext.Any())
                {

                }
                else
                {
                    ViewBag.EmptyList = "No matches found.";
                }
            }
            else if (String.IsNullOrEmpty(barcode) && !User.IsInRole("Administrator"))
            {
                cureContext = from m in _context.MedicalDevices.Include(m => m.Bin)
                              where m.ID.Equals("?") && m.IsApproved.Equals(true)
                              select m;
                ViewBag.EmptyList = "No matches found.";
            }
            else
            {
                cureContext = from m in _context.MedicalDevices.Include(m => m.Bin)
                              where m.IsApproved.Equals(true) && (m.Barcode.Contains(barcode) || m.Description.Contains(barcode) || m.Brand.Contains(barcode) || (m.Brand + " " + m.Description).Contains(barcode))
                              select m;

                if (cureContext.Any())
                {

                }
                else
                {
                    ViewBag.EmptyList = "No matches found.";
                }
            }

            int pageSize = 100;
            return View(await PaginatedList<MedicalDevice>.CreateAsync(cureContext.AsNoTracking(), page ?? 1, pageSize));
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
        public async Task<IActionResult> Create([Bind("ID,Barcode,BinID,Brand,CreatedBy,Description,IsApproved,Manufacturer,PhotoUrl,Name,Warehouse,DateSubmitted")] MedicalDevice medicalDevice, IFormFile file)
        {

            medicalDevice.DateSubmitted = DateTime.UtcNow;
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

        private MedicalDevice GetMedicalDeviceById(int? id)
        {
            return _context.MedicalDevices.FirstOrDefault(m => m.ID == id);
        }

        private void AddLog(MedicalDevice md1, MedicalDevice md2)
        {
            MedicalDeviceLog mdlog = new MedicalDeviceLog();
            mdlog.New = md2.Description;
            mdlog.Old = md1.Description;
            mdlog.WhatChanged = "Description";
            mdlog.Date = DateTime.UtcNow;
            mdlog.ChangedBy = User.Identity.Name;
            _mdrepository.Add(mdlog);
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
        public async Task<IActionResult> Edit(int id, [Bind("ID,Barcode,BinID,Brand,CreatedBy,Description,IsApproved,Manufacturer,PhotoUrl,Name,Warehouse,DateSubmitted")] MedicalDevice medicalDevice)
        {
            MedicalDevice oldmedicaldevice = GetMedicalDeviceById(id);
            if (id != medicalDevice.ID)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                medicalDevice.CreatedBy = User.Identity.Name;
                medicalDevice.Name = "";
                medicalDevice.Warehouse = "";
                AddLog(oldmedicaldevice, medicalDevice);
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

        private double JaroDistance(string source, string target)
        {
            int m = source.Intersect(target).Count();

            if (m == 0)
            {
               return 0;
            }
            else
            {
                string sourceTargetIntersectAsString = "";
                string targetSourceIntersectAsString = "";
                IEnumerable<char> sourceIntersectTarget = source.Intersect(target);
                IEnumerable<char> targetIntersectSource = target.Intersect(source);
                foreach(char character in sourceIntersectTarget) { sourceTargetIntersectAsString += character; }
                foreach(char character in targetIntersectSource) { targetSourceIntersectAsString += character; }
                double t = LevenshteinDistance(sourceTargetIntersectAsString ,targetSourceIntersectAsString) / 2;
                return ((m / source.Length) + (m / target.Length) + ((m - t) / m)) / 3;
            }
        }

        private int LevenshteinDistance(string source, string target)
        {
            if (source.Length == 0) { return target.Length; }
            if (target.Length == 0) { return source.Length; }

            int distance = 0;

            if (source[source.Length - 1] == target[target.Length - 1]) { distance = 0; }
            else { distance = 1; }

            return Math.Min(Math.Min(LevenshteinDistance(source.Substring(0, source.Length - 1), target) + 1,
                                     LevenshteinDistance(source, target.Substring(0, target.Length - 1))) + 1,
                                     LevenshteinDistance(source.Substring(0, source.Length - 1), target.Substring(0, target.Length - 1)) + distance);
        }
    }
}
