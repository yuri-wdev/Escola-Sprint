using Escola_Sprint.Data;
using Escola_Sprint.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Escola_Sprint.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    
    public class DisciplinasController : ControllerBase
    {
        private readonly AppDbContext _context;

        public DisciplinasController(AppDbContext context)
        {
            _context = context;
        }

        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Disciplina>>> GetDisciplinas()
        {
            return await _context.Disciplinas.ToListAsync();
        }

        
        [HttpGet("{id}")]
        public async Task<ActionResult<Disciplina>> GetDisciplina(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);

            if (disciplina == null) return NotFound();

            return disciplina;
        }

        
        [HttpPost]
        public async Task<ActionResult<Disciplina>> PostDisciplina(Disciplina disciplina)
        {

            var professorExiste = await _context.Professores.AnyAsync(p => p.Id == disciplina.codigoprofessor);
            if (!professorExiste)
            {
                return BadRequest("Erro: O Professor informado no campo 'codigoprofessor' não existe no banco.");
            }

            _context.Disciplinas.Add(disciplina);
            await _context.SaveChangesAsync();

            
            return CreatedAtAction(nameof(GetDisciplina), new { id = disciplina.IdDisciplina }, disciplina);
        }

        
        [HttpPut("{id}")]
        public async Task<IActionResult> PutDisciplina(int id, Disciplina disciplina)
        {
            if (id != disciplina.IdDisciplina)
            {
                return BadRequest("O ID enviado na URL não coincide com o ID do objeto.");
            }

            var professorExiste = await _context.Professores.AnyAsync(p => p.Id == disciplina.codigoprofessor);
            if (!professorExiste)
            {
                return BadRequest("Erro: Não é possível atualizar para um Professor que não existe.");
            }

            _context.Entry(disciplina).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { mensagem = "Disciplina atualizada com sucesso!", data = disciplina });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DisciplinaExists(id))
                {
                    return NotFound($"Erro: A disciplina com ID {id} não foi encontrada.");
                }
                throw;
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(500, $"Erro ao acessar o banco de dados: {ex.InnerException?.Message ?? ex.Message}");
            }
            catch (Exception ex)
            {
                return StatusCode(500, $"Ocorreu um erro inesperado: {ex.Message}");
            }
        }

        
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDisciplina(int id)
        {
            var disciplina = await _context.Disciplinas.FindAsync(id);
            if (disciplina == null) return NotFound();

            _context.Disciplinas.Remove(disciplina);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool DisciplinaExists(int id)
        {
            return _context.Disciplinas.Any(e => e.IdDisciplina == id);
        }
    }
}