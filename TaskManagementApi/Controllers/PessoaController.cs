using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TaskManagementApi.Data;
using TaskManagementApi.Models;

namespace TaskManagementApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PessoaController : ControllerBase
    {
        private readonly ApplicationDbContext _context;

        public PessoaController(ApplicationDbContext context)
        {
            _context = context;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Pessoa>>> GetPessoas()
        {
            // Obtém todas as pessoas com suas respectivas tarefas
            var pessoas = await _context.Pessoas
                .Include(p => p.Tarefas)
                .ToListAsync();

            // Itera pelas pessoas e verifica o status das tarefas
            foreach (var pessoa in pessoas)
            {
                bool todasTarefasConcluidas = pessoa.Tarefas.All(t => t.Status == "concluída");
                bool possuiTarefasNaoConcluidas = pessoa.Tarefas.Any(t => t.Status != "concluída");

                if (todasTarefasConcluidas && pessoa.Status != "disponível")
                {
                    pessoa.Status = "disponível";
                }
                
                else if (possuiTarefasNaoConcluidas && pessoa.Status == "disponível")
                {
                    pessoa.Status = "indisponível";

                }else if (pessoa.Status == "") {
                    pessoa.Status = todasTarefasConcluidas ? "disponível" : "indisponível";
                }
            }

            
            await _context.SaveChangesAsync();

            return pessoas;
        }


        [HttpGet("{id}")]
        public async Task<ActionResult<Pessoa>> GetPessoa(int id)
        {
            var pessoa = await _context.Pessoas.Include(p => p.Tarefas).FirstOrDefaultAsync(p => p.PessoaId == id);

            if (pessoa == null) return NotFound();

            return pessoa;
        }

        [HttpPost]
        public async Task<ActionResult<Pessoa>> PostPessoa(Pessoa pessoa)
        {
            pessoa.Status = "disponível";
            pessoa.Tarefas = new List<Tarefa>();

            _context.Pessoas.Add(pessoa);
            await _context.SaveChangesAsync();

            return CreatedAtAction(nameof(GetPessoa), new { id = pessoa.PessoaId }, pessoa);
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> PutPessoa(int id, Pessoa pessoa)
        {
            if (id != pessoa.PessoaId) return BadRequest();

            _context.Entry(pessoa).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException) when (!_context.Pessoas.Any(p => p.PessoaId == id))
            {
                return NotFound();
            }

            return NoContent();
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeletePessoa(int id)
        {
            var pessoa = await _context.Pessoas.Include(p => p.Tarefas).FirstOrDefaultAsync(p => p.PessoaId == id);

            if (pessoa == null) return NotFound();

            if (pessoa.Tarefas.Any(t => t.Status != "concluída"))
                return BadRequest(new {message = "Não é possível excluir uma pessoa com tarefas pendentes."});

            _context.Pessoas.Remove(pessoa);
            await _context.SaveChangesAsync();

            return NoContent();
        }
    }
}
