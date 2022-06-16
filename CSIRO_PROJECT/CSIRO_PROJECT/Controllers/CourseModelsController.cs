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
    public class CourseModelsController : Controller
    {
        private readonly ApplicationDBContext _context;

        public CourseModelsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: CourseModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.courses.ToListAsync());
        }

        // GET: CourseModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseModel = await _context.courses
                .FirstOrDefaultAsync(m => m.courseId == id);
            if (courseModel == null)
            {
                return NotFound();
            }

            return View(courseModel);
        }

        // GET: CourseModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: CourseModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("courseId,courseName")] CourseModel courseModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(courseModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(courseModel);
        }

        // GET: CourseModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseModel = await _context.courses.FindAsync(id);
            if (courseModel == null)
            {
                return NotFound();
            }
            return View(courseModel);
        }

        // POST: CourseModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("courseId,courseName")] CourseModel courseModel)
        {
            if (id != courseModel.courseId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(courseModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!CourseModelExists(courseModel.courseId))
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
            return View(courseModel);
        }

        // GET: CourseModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var courseModel = await _context.courses
                .FirstOrDefaultAsync(m => m.courseId == id);
            if (courseModel == null)
            {
                return NotFound();
            }

            return View(courseModel);
        }

        // POST: CourseModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var courseModel = await _context.courses.FindAsync(id);
            _context.courses.Remove(courseModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool CourseModelExists(int id)
        {
            return _context.courses.Any(e => e.courseId == id);
        }
    }
}
