using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementApi.Models
{
    public class Pessoa
    {
        [Key]
        public int PessoaId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public required string Nome { get; set; }
        
        [Required]
        [EmailAddress]
        public required string Email { get; set; }
        
        [Required]
        public DateTime DataNascimento { get; set; }

        [Required]
        public required string Status { get; set; } = "disponível";

        public ICollection<Tarefa> Tarefas { get; set; } = new List<Tarefa>();
    }
}
