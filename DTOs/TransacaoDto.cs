namespace ControleGastos.DTOs
{
    public class TransacaoDto
    {
        public int TransacaoId { get; set; }
        public int ClienteId { get; set; }
        public string Nome { get; set; }
        public string Email { get; set; }
        public DateTime DataTransacao { get; set; }
        public decimal Valor { get; set; }
        public string Descricao { get; set; }
        public string Classificacao { get; set; }
        public string FormaSaida { get; set; }
        public string TipoTransacao { get; set; }

    }
}
