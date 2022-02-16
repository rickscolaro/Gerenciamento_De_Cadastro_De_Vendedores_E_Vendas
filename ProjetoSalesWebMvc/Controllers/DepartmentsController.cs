
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ProjetoSalesWebMvc.Data;
using ProjetoSalesWebMvc.Models;

namespace ProjetoSalesWebMvc.Controllers {

    public class DepartmentsController : Controller {


        private readonly SalesWebMvcContext _context;

        public DepartmentsController(SalesWebMvcContext context) {
            _context = context;
        }

        // GET: Departments
        public async Task<IActionResult> Index() {
            return View(await _context.Department.ToListAsync());
        }

        // GET: Departments/Details/id
        public async Task<IActionResult> Details(int? id) {
            if (id == null) {
                return NotFound();
            }

            var department = await _context.Department
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null) {
                return NotFound();
            }

            return View(department);
        }

        // GET: Departments/Create
        public IActionResult Create() {
            return View();
        }

        // POST: Departments/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name")] Department department) {

            if (ModelState.IsValid) {
                _context.Add(department);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Edit/id
        public async Task<IActionResult> Edit(int? id) {
            if (id == null) {
                return NotFound();
            }

            var department = await _context.Department.FindAsync(id);
            if (department == null) {
                return NotFound();
            }
            return View(department);
        }

        // POST: Departments/Edit/id
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name")] Department department) {

            if (id != department.Id) {
                return NotFound();
            }

            if (ModelState.IsValid) {
                try {
                    _context.Update(department);
                    await _context.SaveChangesAsync();
                } catch (DbUpdateConcurrencyException) {
                    if (!DepartmentExists(department.Id)) {
                        return NotFound();
                    } else {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(department);
        }

        // GET: Departments/Delete/id
        public async Task<IActionResult> Delete(int? id) {

            if (id == null) {
                return NotFound();
            }

            var department = await _context.Department
                .FirstOrDefaultAsync(m => m.Id == id);
            if (department == null) {
                return NotFound();
            }

            return View(department);
        }

        // POST: Departments/Delete/id
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id) {
            
            var department = await _context.Department.FindAsync(id);
            _context.Department.Remove(department);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool DepartmentExists(int id) {
            return _context.Department.Any(e => e.Id == id);
        }















        // public IActionResult Index() {

        //     List<Department> list = new List<Department>();
        //     list.Add(new Department { Id = 1, Name = "Eletronics" });
        //     list.Add(new Department { Id = 2, Name = "Fashion" });

        //     return View(list);
        // }
    }
}