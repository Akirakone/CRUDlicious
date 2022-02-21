using Microsoft.EntityFrameworkCore;
using Monsters.Models;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace Monsters.Controllers
{
    public class HomeController : Controllers
    {
        private MyContext _context;

        // here we can "inject" our context service into the constructor
        public HomeController(MyContext context)
        {
            _context = context;
        }

        [HttpGet("dashboard")]
        public IActionResult Index()
        {
            ViewBag.AllDishes = _context.Dishes.OrderByDescending(d => d.CreatedAt).ToList();
            return View();
         
        }
        [HttpGet("Createform")]
        public IActionResult Add()
        {
            return View();
        }

        [HttpPost("newDish")]
        public IActionResult addDish(Dish newDish)
        {
            if (ModelState.IsValid)
            {
                _context.Add(newDish);
                _context.SaveChanges();
                return RedirectToAction("Index");
            }
            else
            {
                return View("Add");
            }
        }

        
          [HttpGet("editDish/{dId}")]
        public IActionResult Edit(int dId)
        {
            Dish oneDish = _context.Dishes.FirstOrDefault(d => d.DishId == dId);
            return View(oneDish);
        }
            [HttpPost("updateDish/{dId}")]
        public IActionResult Update(int dId,Dish edited )
        {
            edited.DishId =  dId;
            if (ModelState.IsValid)
            {
                
                Dish original = _context.Dishes.FirstOrDefault(d => d.DishId == dId);
                original.Name = edited.Name;
                original.Chef = edited.Chef;
                original.Tastiness = edited.Tastiness;
                original.Calories = edited.Calories;
                original.Description = edited.Description;
                original.UpdatedAt = DateTime.Now;

                _context.SaveChanges();
                return View("ShowDish",original);
            } else{
                return View("Edit", edited);
            } 

        }
            [HttpGet("delete/{dId}")]
        public IActionResult DeleteDish(int dId)
        {
            Dish toDelete = _context.Dishes.SingleOrDefault(d => d.DishId == dId);
            _context.Dishes.Remove(toDelete);
            _context.SaveChanges();
            return RedirectToAction("Index");
        }
        }
    }
}