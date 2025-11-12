using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Actividad5LengProg3.Data;
using Actividad5LengProg3.Models;

namespace Actividad5LengProg3.Controllers
{
    public class RecintosController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RecintosController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _context.Recintos.ToListAsync();
            return View(lista);
        }

        public IActionResult Crear()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Crear(RecintoViewModel recinto)
        {
            if (ModelState.IsValid)
            {
                // Evitar duplicados por Codigo
                var exists = await _context.Recintos.AnyAsync(r => r.Codigo == recinto.Codigo);
                if (exists) ModelState.AddModelError(nameof(recinto.Codigo), "Ya existe un recinto con ese código.");
                else
                {
                    _context.Recintos.Add(recinto);
                    await _context.SaveChangesAsync();
                    return RedirectToAction(nameof(Index));
                }
            }
            return View(recinto);
        }

        public async Task<IActionResult> Editar(string codigo)
        {
            if (codigo == null) return NotFound();
            var recinto = await _context.Recintos.FindAsync(codigo);
            if (recinto == null) return NotFound();
            return View(recinto);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(RecintoViewModel recinto)
        {
            if (!ModelState.IsValid) return View(recinto);

            var existente = await _context.Recintos.FindAsync(recinto.Codigo);
            if (existente == null) return NotFound();

            existente.Nombre = recinto.Nombre;
            existente.Direccion = recinto.Direccion;

            _context.Recintos.Update(existente);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Eliminar(string codigo)
        {
            if (codigo == null) return NotFound();
            var recinto = await _context.Recintos.FindAsync(codigo);
            if (recinto != null)
            {
                // Verificar si existen estudiantes relacionados (opcional)
                var tieneEstudiantes = await _context.Estudiantes.AnyAsync(e => e.Recinto == codigo);
                if (tieneEstudiantes)
                {
                    TempData["Error"] = "No se puede eliminar: existen estudiantes asignados a este recinto.";
                    return RedirectToAction(nameof(Index));
                }

                _context.Recintos.Remove(recinto);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }

        public async Task<IActionResult> Detalle(string codigo)
        {
            if (codigo == null) return NotFound();
            var recinto = await _context.Recintos
                .Include(r => r.Estudiantes)
                .FirstOrDefaultAsync(r => r.Codigo == codigo);
            if (recinto == null) return NotFound();
            return View(recinto);
        }
    }
}
