using MachinTest11.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MachinTest11.Controllers
{
    public class CategoryController : Controller
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // GET: Category
        public ActionResult Index(int page = 1)
        {
            int pageSize = 2; 
            var categories = db.Categories
                               .OrderBy(c => c.CategoryId) 
                               .Skip((page - 1) * pageSize) 
                               .Take(pageSize) 
                               .ToList();

            var totalCategories = db.Categories.Count(); 

            ViewBag.TotalPages = (int)Math.Ceiling((double)totalCategories / pageSize); 
            ViewBag.CurrentPage = page; 

            return View(categories); 
        }


        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public ActionResult Create(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Edit(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            return View(category);
        }

        [HttpPost]
        public ActionResult Edit(Category category)
        {
            if (ModelState.IsValid)
            {
                db.Entry(category).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(category);
        }

        public ActionResult Delete(int id)
        {
            var category = db.Categories.Find(id);
            if (category == null)
            {
                return HttpNotFound();
            }
            db.Categories.Remove(category);
            db.SaveChanges();
            return RedirectToAction("Index");
        }
    }
}