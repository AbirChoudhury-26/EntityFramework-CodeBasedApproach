using EFCodeApproach.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace EFCodeApproach.Controllers
{
    public class HomeController : Controller
    {
        private readonly StudentDbContext studentDb;

        //private readonly ILogger<HomeController> _logger;

        //public HomeController(ILogger<HomeController> logger)
        //{
        //    _logger = logger;
        //}

        public HomeController(StudentDbContext studentDb)
        {
            
            this.studentDb = studentDb;
        }



        

        // Action Method for Display User Details
        public async Task<IActionResult> Index()
        {
            var stdData = await studentDb.Students.ToListAsync();
            return View(stdData);
        }

        // Action Method to create user details like Name,Age ,Gender etc.. via user Input that will be given.
		public IActionResult Create()
		{
			return View();
		}

        [HttpPost]
		public async Task <IActionResult> Create(Student std)
		{
			
			var existingStudent = await studentDb.Students.FirstOrDefaultAsync(s => s.Name == std.Name);

			if (existingStudent != null)
			{
				// A student with the same name already exists, display an error message
				ModelState.AddModelError("Name", "A student with this name already exists.");
				return View(std);
			}

			// ModelState.IsValid property is used to check whether the model state is valid or not.
			// It is typically used in controller actions to determine if there are any validation errors associated with the model being submitted in an HTTP request.

			if (ModelState.IsValid)
            {
				await studentDb.Students.AddAsync(std);
                await studentDb.SaveChangesAsync();
                TempData["insert_success "] = "Inserted Successfully...";
              return  RedirectToAction("Index","Home");
            }
			return View(std);
		}

		public async Task<IActionResult> Details(int?Id)
		{
            if(Id==null & studentDb.Students ==null)
            {
                return NotFound();
            }

            var stdData = await studentDb.Students.FirstOrDefaultAsync(x => x.Id == Id);
             if(stdData == null)
            {
                return NotFound();
            }
			return View(stdData);
		}

        public async Task<IActionResult> Edit(int? Id)
        {
            if (Id == null & studentDb.Students == null)
            {
                return NotFound();
            }

            var stdData = await studentDb.Students.FindAsync(Id);
            if (stdData == null)
            {
                return NotFound();
            }
            return View(stdData);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int? Id,Student std)
        {
           
             if(Id!=std.Id)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                studentDb.Students.Update(std);
                await studentDb.SaveChangesAsync();
				TempData["update_success "] = "Updated Successfully...";
				return RedirectToAction("Index", "Home");
            }

            return View(std);
        }

        public async Task<IActionResult> Delete(int? Id)
        {
            if (Id == null & studentDb.Students == null)
            {
                return NotFound();
            }

            var stdData = await studentDb.Students.FirstOrDefaultAsync(x => x.Id == Id);
            if (stdData == null)
            {
                return NotFound();
            }
            return View(stdData);
        }


        [HttpPost,ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int? Id, Student std)
        {

            var stdData = await studentDb.Students.FindAsync(Id);
            if (stdData!=null)
            {
                studentDb.Students.Remove(stdData);
            }
            await studentDb.SaveChangesAsync();
			TempData["delete_success "] = "Deleted  Successfully...";
			return RedirectToAction("Index", "Home");
            
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}