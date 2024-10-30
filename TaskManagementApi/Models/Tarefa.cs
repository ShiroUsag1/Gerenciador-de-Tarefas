using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TaskManagementApi.Models
{
    public class Tarefa
    {
        [Key]
        public int TarefaId { get; set; }
        
        [Required]
        [MaxLength(100)]
        public required string Titulo { get; set; }
        
        [MaxLength(500)]
        public required string Descricao { get; set; }
        
        [Required]
        public DateTime DataCriacao { get; set; } = DateTime.Now;
        
        [Required]
        public string Status { get; set; } = "pendente"; // Status padrão
        
        // Chave estrangeira para Pessoa
        public int PessoaId { get; set; }
    }
}
