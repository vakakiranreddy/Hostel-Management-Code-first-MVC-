using HostelManagement.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace HostelManagement.Controllers
{
    public class AllFaculty : Controller
    {
        public readonly StudentContext _context;

        public AllFaculty(StudentContext context)   
        {
            _context = context;
        }

        public IActionResult Index()
        {
            IEnumerable<Faculty> FacultyList = _context.AllFaculty.ToList();
            return View(FacultyList);
        }
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var faculty = await _context.AllFaculty.FindAsync(id);
            if (faculty == null)
            {
                return NotFound();
            }
            return View(faculty);
        }
       

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,RoomNumber,Department")] Faculty fac)
        {
            if (id != fac.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(fac);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!FacultyExists(fac.Id))
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
            return View(fac);
        }
        public IActionResult Create()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Faculty fac)
        {
             
           _context.AllFaculty.Add(fac);
            await _context.SaveChangesAsync();
            return RedirectToAction("Index");
            
           
        }
        private bool FacultyExists(int id)
        {
            return _context.AllFaculty.Any(f => f.Id == id);
        }
        public async Task<IActionResult> Delete(int? id)
        { 

            Faculty fac = await _context.AllFaculty
                .FirstOrDefaultAsync(m => m.Id == id);
           

            return View(fac);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var fac = await _context.AllFaculty.FindAsync(id);
            if (fac != null)
            {
                _context.AllFaculty.Remove(fac);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        // GET: Students/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var fac = await _context.AllFaculty
                .FirstOrDefaultAsync(m => m.Id == id);
            if (fac == null)
            {
                return NotFound();
            }

            return View(fac);
        }


    }
}
