using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Escola_Sprint.Data;
using Escola_Sprint.Models;

namespace Escola_Sprint.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
        {
            return await _context.Alunos.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null) return NotFound();

            return aluno;
        }

        [HttpPost]
        public async Task<ActionResult<Aluno>> PostAluno(Aluno aluno)
        {
            
            var cursoExiste = await _context.Cursos.AnyAsync(c => c.IdCurso == aluno.IdCurso);
            if (!cursoExiste)
            {
                return BadRequest("Erro: O Curso informado não existe no banco de dados.");
            }

            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAluno), new { id = aluno.IdAluno }, aluno);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutAluno(int id, Aluno aluno)
        {
            if (id != aluno.IdAluno)
            {
                return BadRequest("O ID enviado na URL não coincide com o ID do objeto.");
            }

          
            var cursoExiste = await _context.Cursos.AnyAsync(c => c.IdCurso == aluno.IdCurso);
            if (!cursoExiste)
            {
                return BadRequest("Erro: Não é possível atualizar para um Curso que não existe.");
            }

            _context.Entry(aluno).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
                return Ok(new { mensagem = "Aluno atualizado com sucesso!", data = aluno });
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AlunoExists(id))
                {
                    return NotFound($"Erro: O aluno com ID {id} não foi encontrado no banco.");
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
        public async Task<IActionResult> DeleteAluno(int id)
        {
            var aluno = await _context.Alunos.FindAsync(id);

            if (aluno == null) return NotFound();

            _context.Alunos.Remove(aluno);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AlunoExists(int id)
        {
            return _context.Alunos.Any(e => e.IdAluno == id);
        }
    }
}