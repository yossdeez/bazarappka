using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using bazarappka.Models;
using Microsoft.Data.SqlClient;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace bazarappka.Controllers
{
    public class AutaInfoesController : Controller
    {
        private readonly BazarContext _context;

        public AutaInfoesController(BazarContext context)
        {
            _context = context;
        }

        // GET: AutaInfoes
        public async Task<IActionResult> Index()
        {
            return View(await _context.AutaInfos.ToListAsync());
        }

        [HttpGet]
        public JsonResult IsLicensePlateAvailable(string licensePlate, int? id)
        {
            if (string.IsNullOrWhiteSpace(licensePlate))
                return Json(true);

            bool exists = _context.AutaInfos
                .Any(c => c.LicensePlate.ToLower() == licensePlate.ToLower() && c.Id != id);
            
            return Json(!exists);
        }


        // GET: AutaInfoes/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autaInfo = await _context.AutaInfos
                .FirstOrDefaultAsync(m => m.Id == id);
            if (autaInfo == null)
            {
                return NotFound();
            }

            return View(autaInfo);
        }

        // GET: AutaInfoes/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: AutaInfoes/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Manufacturer,Model,Year,Mileage,Fuel,Body,LicensePlate,Condition,ListedSince,OtherDetails")] AutaInfo autaInfo)
        {
            if (ModelState.IsValid)
            {
                autaInfo.ListedSince = DateTime.Now;
                _context.Add(autaInfo);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(autaInfo);
        }

        // GET: AutaInfoes/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var autaInfo = await _context.AutaInfos.FindAsync(id);
            if (autaInfo == null)
            {
                return NotFound();
            }
            return View(autaInfo);
        }

        // POST: AutaInfoes/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Manufacturer,Model,Year,Mileage,Fuel,Body,LicensePlate,Condition,OtherDetails")] AutaInfo autaInfo)
        {
            if (id != autaInfo.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    var listedSince = await _context.AutaInfos
                        .Where(a => a.Id == id)
                        .Select(a => a.ListedSince)
                        .FirstOrDefaultAsync();

                    autaInfo.ListedSince = listedSince;

                    _context.Update(autaInfo);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!_context.AutaInfos.Any(e => e.Id == autaInfo.Id))
                        return NotFound();
                    else
                        throw;
                }

                return RedirectToAction(nameof(Index));
            }

            return View(autaInfo);
        }

        // POST: AutaInfoes/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var autaInfo = await _context.AutaInfos.FindAsync(id);
            if (autaInfo != null)
            {
                _context.AutaInfos.Remove(autaInfo);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool AutaInfoExists(int id)
        {
            return _context.AutaInfos.Any(e => e.Id == id);
        }
        public TException GetInnerException<TException>(Exception exception)
        where TException : Exception
        {
            Exception innerException = exception;
            while (innerException != null)
            {
                if (innerException is TException result)
                {
                    return result;
                }
                innerException = innerException.InnerException;
            }
            return null;
        }
    }
}

