using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Application;
using MellonThinClient.Data;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace MellonThinClient.Controllers
{
    public class ToDosController : Controller
    {
        readonly IToDosRepository _toDos;

        public ToDosController(IToDosRepository toDosRepository)
        {
            this._toDos = toDosRepository;
        }

        // GET: ToDos
        public async Task<IActionResult> Index()
        {
            return View(await _toDos.GetAsync());
        }

        // GET: ToDos/Details/5
        public async Task<IActionResult> Details(int id)
        {
            ToDo? toDo = await _toDos.GetByIdAsync(id);

            if (toDo is null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // GET: ToDos/Create
        public ActionResult Create()
        {
            return View(new ToDo());
        }

        // POST: ToDos/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ToDo toDo)
        {
            await _toDos.CreateAsync(toDo);

            return RedirectToAction(nameof(Index));
        }

        // GET: ToDos/Edit/5
        public async Task<IActionResult> Edit(int id)
        {
            ToDo? toDo = await _toDos.GetByIdAsync(id);

            if (toDo is null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // POST: ToDos/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, ToDo toDo)
        {
            // TODO: Add update logic here
            await _toDos.UpdateAsync(toDo);

            return RedirectToAction(nameof(Index));
        }

        // GET: ToDos/Delete/5
        public async Task<IActionResult> Delete(int id)
        {
            ToDo? toDo = await _toDos.GetByIdAsync(id);

            if (toDo is null)
            {
                return NotFound();
            }

            return View(toDo);
        }

        // POST: ToDos/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _toDos.DeleteAsync(id);

            return RedirectToAction(nameof(Index));
        }
    }
}