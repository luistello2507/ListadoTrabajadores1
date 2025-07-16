using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using ListadoTrabajadores1.Models;

namespace ListadoTrabajadores1.Controllers
{
    public class TrabajadoresController : Controller
    {
        private readonly BdtrabajadoresContext _context;

        public TrabajadoresController(BdtrabajadoresContext context)
        {
            _context = context;
        }

        // GET: Trabajadores
        public async Task<IActionResult> Index()
        {
            var bdtrabajadoresContext = _context.Trabajadores.Include(t => t.IdDepartamentoNavigation).Include(t => t.IdDistritoNavigation).Include(t => t.IdProvinciaNavigation);
            return View(await bdtrabajadoresContext.ToListAsync());
        }

        // GET: Trabajadores/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores
                .Include(t => t.IdDepartamentoNavigation)
                .Include(t => t.IdDistritoNavigation)
                .Include(t => t.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajadore == null)
            {
                return NotFound();
            }

            return View(trabajadore);
        }

        // GET: Trabajadores/Create
        public IActionResult Create()
        {
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento");
            ViewData["IdDistrito"] = new SelectList(_context.Distritos, "Id", "NombreDistrito");
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "Id", "NombreProvincia");
            return View();
        }

        // POST: Trabajadores/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajadore trabajadore)
        {
            if (ModelState.IsValid)
            {
                _context.Add(trabajadore);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento", trabajadore.IdDepartamento);
            ViewData["IdDistrito"] = new SelectList(_context.Distritos, "Id", "NombreDistrito", trabajadore.IdDistrito);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "Id", "NombreProvincia", trabajadore.IdProvincia);
            return View(trabajadore);
        }

        // GET: Trabajadores/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores.FindAsync(id);
            if (trabajadore == null)
            {
                return NotFound();
            }
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDepartamento", trabajadore.IdDepartamento);
            ViewData["IdDistrito"] = new SelectList(_context.Distritos, "Id", "NombreDistrito", trabajadore.IdDistrito);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "Id", "NombreProvincia", trabajadore.IdProvincia);
            return View(trabajadore);
        }

        // POST: Trabajadores/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,TipoDocumento,NumeroDocumento,Nombres,Sexo,IdDepartamento,IdProvincia,IdDistrito")] Trabajadore trabajadore)
        {
            if (id != trabajadore.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(trabajadore);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!TrabajadoreExists(trabajadore.Id))
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
            ViewData["IdDepartamento"] = new SelectList(_context.Departamentos, "Id", "NombreDeparmento", trabajadore.IdDepartamento);
            ViewData["IdDistrito"] = new SelectList(_context.Distritos, "Id", "NombreDistrito", trabajadore.IdDistrito);
            ViewData["IdProvincia"] = new SelectList(_context.Provincia, "Id", "NombreProvincia", trabajadore.IdProvincia);
            return View(trabajadore);
        }

        // GET: Trabajadores/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var trabajadore = await _context.Trabajadores
                .Include(t => t.IdDepartamentoNavigation)
                .Include(t => t.IdDistritoNavigation)
                .Include(t => t.IdProvinciaNavigation)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (trabajadore == null)
            {
                return NotFound();
            }

            return View(trabajadore);
        }

        // POST: Trabajadores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var trabajadore = await _context.Trabajadores.FindAsync(id);
            if (trabajadore != null)
            {
                _context.Trabajadores.Remove(trabajadore);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool TrabajadoreExists(int id)
        {
            return _context.Trabajadores.Any(e => e.Id == id);
        }
    }
}
