using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TarefaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public TarefaController(ApplicationDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Tarefa>>> GetTarefas()
        {
            return await _context.Tarefas.ToListAsync();
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Tarefa>> GetTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }
            return tarefa;
        }

        [HttpPost]
        public async Task<ActionResult<Tarefa>> PostTarefa(Tarefa tarefa)
        {
            
           var pessoa = await _context.Pessoas.FindAsync(tarefa.PessoaId);
            if (pessoa == null)
            {
                return NotFound("Pessoa não encontrada."); 
            }

            _context.Tarefas.Add(tarefa);
            await _context.SaveChangesAsync();
            return CreatedAtAction("GetTarefa", new { id = tarefa.TarefaId }, tarefa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutTarefa(int id, Tarefa tarefa)
        {
            if (id != tarefa.TarefaId)
            {
                return BadRequest();
            }

            // Verifique se a PessoaId é válida antes de atualizar
            if (!await _context.Pessoas.AnyAsync(p => p.PessoaId == tarefa.PessoaId))
            {
                return BadRequest("Pessoa não encontrada.");
            }

            _context.Entry(tarefa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!TarefaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteTarefa(int id)
        {
            var tarefa = await _context.Tarefas.FindAsync(id);
            if (tarefa == null)
            {
                return NotFound();
            }

            _context.Tarefas.Remove(tarefa);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        private bool TarefaExists(int id)
        {
            return _context.Tarefas.Any(e => e.TarefaId == id);
        }
    }
}
