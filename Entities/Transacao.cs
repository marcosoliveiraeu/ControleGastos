using ControleGastos.Enum;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Entities
{
    public class Transacao
    {

        public int Id { get; set; }

        [Required]
        public DateTime DataTransacao { get; set; }

        [Required]
        [StringLength(100)]
        [MinLength(4)]
        public string Descricao { get; set; } = string.Empty;

        [Required]
        public decimal Valor { get; set; }

        [Required]
        public Cliente Cliente { get; set; } = new Cliente();

        [Required]
        public TipoTransacao TipoTransacao { get; set; }

        [Required]
        public FormaSaida FormaSaida { get; set; } = new FormaSaida();

        [Required]
        public Classificacao Classificacao { get; set; } = new Classificacao();

        

    }
}
