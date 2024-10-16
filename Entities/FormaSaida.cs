using System.ComponentModel.DataAnnotations;

namespace ControleGastos.Entities
{
    public class FormaSaida
    {

        // cartão debito , cartão credito, saque, pix, boleto, etc

        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        [MinLength(4)]
        public string Descricao { get; set; } = string.Empty;
    }
}
