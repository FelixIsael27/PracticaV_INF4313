using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Actividad5LengProg3.Data;
using Actividad5LengProg3.Models;

namespace Actividad5LengProg3.Controllers
{
    public class EstudiantesController : Controller
    {
        private readonly ApplicationDbContext _context;
        public EstudiantesController(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<IActionResult> Index()
        {
            await CargarSelectsAsync();
            return View(new EstudianteViewModel());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Registrar(EstudianteViewModel estudiante)
        {
            if (ModelState.IsValid)
            {
                var existe = await _context.Estudiantes.AnyAsync(e => e.Matricula == estudiante.Matricula);
                if (existe)
                {
                    ModelState.AddModelError(nameof(estudiante.Matricula), "Ya existe un estudiante con esa matrícula.");
                    await CargarSelectsAsync();
                    return View("Index", estudiante);
                }

                _context.Estudiantes.Add(estudiante);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Lista));
            }
            await CargarSelectsAsync();
            return View("Index", estudiante);
        }

        public async Task<IActionResult> Lista()
        {
            var estudiantes = await _context.Estudiantes
                .Include(e => e.CarreraCod)
                .Include(e => e.RecintoCod)
                .ToListAsync();
            return View(estudiantes);
        }

        public async Task<IActionResult> Editar(string matricula)
        {
            if (matricula == null) return NotFound();
            var estudiante = await _context.Estudiantes.FindAsync(matricula);
            if (estudiante == null) return NotFound();
            await CargarSelectsAsync();
            return View(estudiante);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Editar(EstudianteViewModel estudiante)
        {
            if (estudiante.EstaBecado && (!estudiante.PorcentajeBeca.HasValue || estudiante.PorcentajeBeca < 0 || estudiante.PorcentajeBeca > 100))
            {
                ModelState.AddModelError(nameof(estudiante.PorcentajeBeca), "Porcentaje inválido.");
            }

            if (!ModelState.IsValid)
            {
                await CargarSelectsAsync();
                return View(estudiante);
            }

            var existente = await _context.Estudiantes.FindAsync(estudiante.Matricula);
            if (existente == null) return NotFound();

            existente.NombreCompleto = estudiante.NombreCompleto;
            existente.Carrera = estudiante.Carrera;
            existente.Recinto = estudiante.Recinto;
            existente.CorreoInstitucional = estudiante.CorreoInstitucional;
            existente.Celular = estudiante.Celular;
            existente.Telefono = estudiante.Telefono;
            existente.Direccion = estudiante.Direccion;
            existente.FechaNacimiento = estudiante.FechaNacimiento;
            existente.Genero = estudiante.Genero;
            existente.Turno = estudiante.Turno;
            existente.EstaBecado = estudiante.EstaBecado;
            existente.PorcentajeBeca = estudiante.PorcentajeBeca;

            _context.Estudiantes.Update(existente);
            await _context.SaveChangesAsync();

            return RedirectToAction(nameof(Lista));
        }

        public async Task<IActionResult> Eliminar(string matricula)
        {
            if (matricula == null) return NotFound();
            var estudiante = await _context.Estudiantes.FindAsync(matricula);
            if (estudiante != null)
            {
                _context.Estudiantes.Remove(estudiante);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Lista));
        }

        private async Task CargarSelectsAsync()
        {
            var carreras = await _context.Carreras.OrderBy(c => c.Nombre).ToListAsync();
            var recintos = await _context.Recintos.OrderBy(r => r.Nombre).ToListAsync();
            ViewBag.Carreras = new SelectList(carreras, "Codigo", "Nombre");
            ViewBag.Recintos = new SelectList(recintos, "Codigo", "Nombre");
            ViewBag.Generos = new SelectList(new[] { "Masculino", "Femenino", "Otro" });
            ViewBag.Turnos = new SelectList(new[] { "Mañana", "Tarde", "Noche" });
        }
    }
}
