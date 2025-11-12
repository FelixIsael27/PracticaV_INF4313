using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Actividad5LengProg3.Data;
using Actividad5LengProg3.Models;

namespace Actividad5LengProg3.Controllers
{
    public class CarrerasController : Controller
    {
        private readonly ApplicationDbContext _context;
        public CarrerasController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _context.Carreras.ToListAsync();
            return View(lista);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(CarreraViewModel carrera)
        {
            if (ModelState.IsValid)
            {
                var exists = await _context.Carreras.AnyAsync(c => c.Codigo == carrera.Codigo);
                if (exists) ModelState.AddModelError(nameof(carrera.Codigo), "Ya existe una carrera con ese código.");
                else
                {
                    _context.Carreras.Add(carrera);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(carrera);
        }

        public async Task<IActionResult> Editar(string codigo)
        {
            if (codigo == null) return NotFound();
            var carrera = await _context.Carreras.FindAsync(codigo);
            if (carrera == null) return NotFound();
            return View(carrera);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(CarreraViewModel carrera)
        {
            if (!ModelState.IsValid) return View(carrera);

            var existente = await _context.Carreras.FindAsync(carrera.Codigo);
            if (existente == null) return NotFound();

            existente.Nombre = carrera.Nombre;
            existente.CantidadCreditos = carrera.CantidadCreditos;
            existente.CantidadMaterias = carrera.CantidadMaterias;

            _context.Carreras.Update(existente);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Eliminar(string codigo)
        {
            if (codigo == null) return NotFound();
            var carrera = await _context.Carreras.FindAsync(codigo);
            if (carrera != null)
            {
                var tieneEstudiantes = await _context.Estudiantes.AnyAsync(e => e.Carrera == codigo);
                if (tieneEstudiantes)
                {
                    TempData["Error"] = "No se puede eliminar: existen estudiantes asignados a esta carrera.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Carreras.Remove(carrera);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
