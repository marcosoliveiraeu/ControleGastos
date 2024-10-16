using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Entities
{
    public class Classificacao
    {

        // ( mercado, lazer, combustivel, etc...)

        
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(4)]
        public string Descricao { get; set; } = string.Empty;

    }
}
