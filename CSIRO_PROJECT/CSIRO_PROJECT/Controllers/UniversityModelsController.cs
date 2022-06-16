using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using CSIRO_PROJECT.Models;
using CSIRO_PROJECT.ViewModels;

namespace CSIRO_PROJECT.Controllers
{
    public class UniversityModelsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public UniversityModelsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: UniversityModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.universities.ToListAsync());
        }

        // GET: UniversityModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var universityModel = await _context.universities
                .FirstOrDefaultAsync(m => m.uniId == id);
            if (universityModel == null)
            {
                return NotFound();
            }

            return View(universityModel);
        }

        // GET: UniversityModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: UniversityModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("uniId,uniName")] UniversityModel universityModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(universityModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(universityModel);
        }

        // GET: UniversityModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var universityModel = await _context.universities.FindAsync(id);
            if (universityModel == null)
            {
                return NotFound();
            }
            return View(universityModel);
        }

        // POST: UniversityModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("uniId,uniName")] UniversityModel universityModel)
        {
            if (id != universityModel.uniId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(universityModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UniversityModelExists(universityModel.uniId))
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
            return View(universityModel);
        }

        // GET: UniversityModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var universityModel = await _context.universities
                .FirstOrDefaultAsync(m => m.uniId == id);
            if (universityModel == null)
            {
                return NotFound();
            }

            return View(universityModel);
        }

        // POST: UniversityModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var universityModel = await _context.universities.FindAsync(id);
            _context.universities.Remove(universityModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UniversityModelExists(int id)
        {
            return _context.universities.Any(e => e.uniId == id);
        }
    }
}
