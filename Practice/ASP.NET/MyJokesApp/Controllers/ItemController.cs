using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using MyJokesApp.Models;
using MyJokesApp.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;


namespace MyJokesApp.Controllers
{
    public class ItemController : Controller
    {
        private readonly AppDbContext _appDbContext;
        public ItemController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<IActionResult> Index(){
            var items = await _appDbContext.Items.Include(x => x.SerialNumber)
                                                    .Include(x => x.Category)
                                                    .ToListAsync();
            return View(items);
        }

        public IActionResult Create(){
            ViewData["Categories"] = new SelectList(_appDbContext.Categories, "Id", "Name");
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create([Bind("Id, Name, Price, CategoryId")] Item item){
            if(ModelState.IsValid){
                _appDbContext.Items.Add(item);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public async Task<IActionResult> Edit(int id){
            var item = await _appDbContext.Items.FirstOrDefaultAsync(item => item.Id == id);
            ViewData["Categories"] = new SelectList(_appDbContext.Categories, "Id", "Name");
            return View(item);
        }

        [HttpPost]
        public async Task<IActionResult> Edit([Bind("Id, Name, Price, CategoryId")] Item item){
            if(ModelState.IsValid){
                _appDbContext.Update(item);
                await _appDbContext.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public async Task<IActionResult> Delete(int id){
            var item = await _appDbContext.Items.FirstOrDefaultAsync(item => item.Id == id);
            return View(item);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id){
            var item = await _appDbContext.Items.FindAsync(id);
            if(item != null){
                _appDbContext.Items.Remove(item);
                await _appDbContext.SaveChangesAsync();
            }
            return RedirectToAction("Index");
        }

    }
}