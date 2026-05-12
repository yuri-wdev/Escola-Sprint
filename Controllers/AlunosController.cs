using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Escola_Sprint.Data;
using Escola_Sprint.Models;

namespace Escola_Sprint.Controllers
{
    [Route("api/[controller]")] // Define a rota como api/alunos
    [ApiController]
    public class AlunosController : ControllerBase
    {
        private readonly AppDbContext _context;

        public AlunosController(AppDbContext context)
        {
            _context = context;

        }

        // GET: api/alunos
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Aluno>>> GetAlunos()
        {
            // O .Include(a => a.Curso) traz os dados do curso junto com o aluno
            return await _context.Alunos.Include(a => a.Curso).ToListAsync();
        }

        // GET: api/alunos/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Aluno>> GetAluno(int id)
        {
            var aluno = await _context.Alunos
                .Include(a => a.Curso)
                .FirstOrDefaultAsync(a => a.IdAluno == id);

            if (aluno == null)
            {
                return NotFound();
            }

            return aluno;
        }

        // POST: api/alunos
        [HttpPost]
        public async Task<ActionResult<Aluno>> PostAluno(Aluno aluno)
        {
            _context.Alunos.Add(aluno);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetAluno), new { id = aluno.IdAluno }, aluno);
        }
    }
}