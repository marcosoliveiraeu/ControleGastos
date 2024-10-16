namespace ControleGastos.DTOs
{
    public class TransacoesResponseDto : ApiResponseBase
    {
        public List<TransacaoDto> Transacoes { get; set; }
        
    }
}
