using ControleGastos.DTOs;
using ControleGastos.Enum;

namespace ControleGastos.Services.Interfaces
{
    public interface ITransacaoService
    {

        Task<TransacoesResponseDto> GetTransacoesAsync(int idCliente , DateTime dtInicio , DateTime dtFim);

        Task AddTransacaoAsync(TransacaoAddDto transacao);

        Task EditTransacaoAsync(TransacaoEditDto transacao);

        Task DeleteTransacaoAsync(int transacaoId);

        void VerificaIntegridadeTransacao(TipoTransacao tpTransacao, int formaSaida , int classificacao);

    }
}
