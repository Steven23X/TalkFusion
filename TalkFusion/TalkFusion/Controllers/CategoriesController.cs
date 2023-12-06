﻿using Microsoft.AspNetCore.Mvc;
using TalkFusion.Data;
using TalkFusion.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Scaffolding.Metadata;

namespace TalkFusion.Controllers
{
    public class CategoriesController : Controller
    {

        private readonly ApplicationDbContext db;

        public CategoriesController (ApplicationDbContext context)
        {
            this.db = context;
        }

        public IActionResult Index()
        {
            var categories = from category in db.Categories
                             select category;

            ViewBag.Categories = categories;

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }

            return View();
        }


        public IActionResult Show(int id)
        {
            Category shownCategory = db.Categories.Include(c=>c.Groups).FirstOrDefault(c => c.Id == id);

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }

            return View(shownCategory);
        }

        [HttpPost]
        public IActionResult Edit(int id, Category requestedCategory)
        {
            Category oldCategory= db.Categories.Find(id);

            try
            {
                oldCategory.CategoryName=requestedCategory.CategoryName;
                db.SaveChanges();

                TempData["message"] = "The category named: " + oldCategory.CategoryName + " was successfully edited.";
                return RedirectToAction("Index");
            }
            catch(Exception e)
            {
                ViewBag.Category= requestedCategory;
                
                return RedirectToAction("Show", new { id = oldCategory.Id });
            }
        }

        public  IActionResult New()
        {
            

            if (TempData.ContainsKey("message"))
            {
                ViewBag.Message = TempData["message"];
            }


                return View();  
        }

        [HttpPost]
        public IActionResult New(Category requestedCategory)
        {
            try { 
                db.Categories.Add(requestedCategory);
                db.SaveChanges();

                TempData["message"] = "Category named: " + requestedCategory.CategoryName + " has been successfully created.";

                return RedirectToAction("Index");
            }
            catch (Exception e) 
            {
                return View(requestedCategory);
            }
        }


        [HttpPost]
        public IActionResult Delete(int id) {
            Category oldCategory = db.Categories.Find(id);

            TempData["message"] = "The category named: " + oldCategory.CategoryName + " has been successfully deleted.";

            db.Categories.Remove(oldCategory);
            db.SaveChanges();
            
            return RedirectToAction("Index");
        }
    }
}