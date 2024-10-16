using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Entities
{
    public class Cliente
    {

        public int ID { get; set; }
        [Required]
        [StringLength(100)]
        [MinLength(4)]
        public string Nome { get; set; } = string.Empty;
        [Required]
        [EmailAddress]
        [StringLength(100)]
        [MinLength(4)]
        public string Email { get; set;} = string.Empty;

        public IEnumerable<Transacao> Transacoes { get; set; } = new List<Transacao>();

    }
}
