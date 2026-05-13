using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Escola_Sprint.Data;
using Escola_Sprint.Models;

namespace Escola_Sprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class NotasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public NotasController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Notas>>> GetNotas()
        {
            return await _context.Notas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Notas>> GetNotas(int id)
        {
            var notas = await _context.Notas.FindAsync(id);

            if (notas == null)
            {
                return NotFound();
            }

            return notas;
        }

     
        [HttpPost]
        public async Task<ActionResult<Notas>> PostNotas(Notas notas)
        {

            var alunoExiste = await _context.Alunos.AnyAsync(a => a.IdAluno == notas.AlunoId);
            var disciplinaExiste = await _context.Disciplinas.AnyAsync(d => d.IdDisciplina == notas.DisciplinaId);

            if (!alunoExiste)
            {
                return BadRequest("Erro: O Aluno informado não existe.");
            }

            if (!disciplinaExiste)
            {
                return BadRequest("Erro: A Disciplina informada não existe.");
            }

            _context.Notas.Add(notas);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetNotas), new { id = notas.IdRelatorio }, notas);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutNotas(int id, Notas notas)
        {
            if (id != notas.IdRelatorio)
            {
                return BadRequest("O ID enviado na URL não coincide com o IdRelatorio do objeto.");
            }

            _context.Entry(notas).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { mensagem = "Notas atualizadas com sucesso.", data = notas });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!NotasExists(id))
                {
                    return NotFound($"Erro: O relatório de notas com ID {id} não foi encontrado.");
                }
                throw;
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Erro: {ex.Message}");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteNotas(int id)
        {
            var notas = await _context.Notas.FindAsync(id);
            if (notas == null)
            {
                return NotFound();
            }

            _context.Notas.Remove(notas);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool NotasExists(int id)
        {
            return _context.Notas.Any(e => e.IdRelatorio == id);
        }
    }
}