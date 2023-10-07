using CRUDWeb.Data;
using CRUDWeb.Models;
using Microsoft.AspNetCore.Mvc;

namespace CRUDWeb.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext _db;
        public CategoryController(ApplicationDbContext db)
        {
            _db = db;
        }
        public IActionResult Index()
        {
            List<Category> objCategoryList = _db.Categories.ToList();
            return View(objCategoryList);
        }
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
		public IActionResult Create(Category category)
		{
            if(category.Name == category.DisplayOrder.ToString())
            {
                ModelState.AddModelError("name", "The Display Order cannot exactly match the Name.");
            }
            if (ModelState.IsValid)
            {
            _db.Categories.Add(category);
            _db.SaveChanges();
				TempData["success"] = "Category added successfully.";
				//return RedirectToAction("Index","Category");
				return RedirectToAction("Index");
            }
            return View();
		}

		public IActionResult Edit(int? id)
		{
            if(id == null || id == 0) 
            {
                return NotFound();
            }
            Category categoryFromDb = _db.Categories.Find(id);
            //Category categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
            //Category categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}

		[HttpPost]
		public IActionResult Edit(Category category)
		{
			if (ModelState.IsValid)
			{
				_db.Categories.Update(category);
				_db.SaveChanges();
				TempData["success"] = "Category updated successfully.";
				//return RedirectToAction("Index","Category");
				return RedirectToAction("Index");
			}
			return View();
		}

		public IActionResult Delete(int? id)
		{
			if (id == null || id == 0)
			{
				return NotFound();
			}
			Category categoryFromDb = _db.Categories.Find(id);
			//Category categoryFromDb1 = _db.Categories.FirstOrDefault(u => u.Id == id);
			//Category categoryFromDb2 = _db.Categories.Where(u => u.Id == id).FirstOrDefault();
			if (categoryFromDb == null)
			{
				return NotFound();
			}
			return View(categoryFromDb);
		}

		[HttpPost,ActionName("Delete")]
		public IActionResult DeletePOST(int? id)
		{
			var obj = _db.Categories.Find(id);
			if(obj == null)
			{
				NotFound();
			}
			_db.Categories.Remove(obj);
			_db.SaveChanges();
			TempData["success"] = "Category deleted successfully.";
			return RedirectToAction("Index");
		}
	}
}
