using CPW219_CRUD_Troubleshooting.Models;
using Microsoft.AspNetCore.Mvc;

namespace CPW219_CRUD_Troubleshooting.Controllers
{
    public class StudentsController : Controller
    {
        private readonly SchoolContext _context;

        public StudentsController(SchoolContext dbContext)
        {
            _context = dbContext;
        }

        public IActionResult Index()
        {
            List<Student> products = StudentDb.GetStudents(_context);
            return View(products);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(Student p)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Add(p);
                _context.SaveChanges();
                ViewData["Message"] = $"{p.Name} was added!";
                return View();
            }

            //Show web page with errors
            return View(p);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Student? studentToEdit = await _context.Students.FindAsync(id);
            if(studentToEdit == null)
            {
                return NotFound();
            }
            return View(studentToEdit);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Student p)
        {
            if (ModelState.IsValid)
            {
                _context.Students.Update(p);
                await _context.SaveChangesAsync();
                TempData["Message"] = $"{p.Name} was updated successfully!";
            }
            //return view with errors
            return View(p);
        }

        public async Task<IActionResult> Delete(int id)
        {
            Student studentToDelete = await _context.Students.FindAsync(id);
            if (studentToDelete == null)
            {
                return NotFound();
            }
            return View(studentToDelete);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirm(int id)
        {
            Student studentToDelete = await _context.Students.FindAsync(id);
            if (studentToDelete != null)
            {
                _context.Remove(studentToDelete);
                await _context.SaveChangesAsync();
                TempData["Message"] = studentToDelete.Name + " was deleted successfully!";
                return RedirectToAction("Index");
            }
            TempData["Message"] = studentToDelete.Name + " has already been deleted!";
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
            Student? studentDetails = await _context.Students.FindAsync(id);

            if (studentDetails == null)
            {
                return NotFound();
            }
            return View(studentDetails);
        }
    }
}
