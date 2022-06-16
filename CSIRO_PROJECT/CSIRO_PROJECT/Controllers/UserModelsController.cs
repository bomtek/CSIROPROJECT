using CSIRO_PROJECT.Models;
using CSIRO_PROJECT.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CSIRO_PROJECT.Controllers
{
   
    public class UserModelsController : Controller
    {
        private readonly ApplicationDBContext _context;



        public UserModelsController(ApplicationDBContext context)
        {
            _context = context;
        }

        // GET: UserModels

        public async Task<IActionResult> Index()
        {

            return View(await _context.users.ToListAsync());
        }

        // GET: UserModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.users
                .FirstOrDefaultAsync(m => m.userId == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }


        // GET: UserModels/Create
        
        public IActionResult Create()
        {
            return View();
        }

        // POST: UserModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind("userId,userFName,userLName,userAddress,userUni,userCourse,userGPA,userContact,userCL")] UserModel userModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(userModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(userModel);
        }


        // GET: UserModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.users.FindAsync(id);
            if (userModel == null)
            {
                return NotFound();
            }
            return View(userModel);
        }

        // POST: UserModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("userId,userFName,userLName,userAddress,userUni,userCourse,userContact,userCL")] UserModel userModel)
        {
            if (id != userModel.userId)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(userModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserModelExists(userModel.userId))
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
            return View(userModel);
        }

        // GET: UserModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var userModel = await _context.users
                .FirstOrDefaultAsync(m => m.userId == id);
            if (userModel == null)
            {
                return NotFound();
            }

            return View(userModel);
        }

        // POST: UserModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var userModel = await _context.users.FindAsync(id);
            _context.users.Remove(userModel);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserModelExists(int id)
        {
            return _context.users.Any(e => e.userId == id);
        }

        public async Task<IActionResult> eligibleCandidate()
        {


            //var userList =  db.Data.SqlQuery("SELECT * FROM Results").ToList();



            return View(await _context.users.Where(m => m.userGPA <= 3.0).ToListAsync());

        }


    }
}
