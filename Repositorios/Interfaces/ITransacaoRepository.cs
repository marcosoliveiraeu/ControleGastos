using ControleGastos.DTOs;
using ControleGastos.Entities;
using ControleGastos.Enum;

namespace ControleGastos.Repositorios.Interfaces
{
    public interface ITransacaoRepository
    {

        Task<Transacao> GetTransacaoById(int id);

        Task<List<TransacaoDto>> GetTransacoesAsync(int clienteId, DateTime dataInicio, DateTime dataFim);

        Task<bool> VerificaClienteAsync(int clienteId);

        Task<bool> VerificaClassificacaoAsync(int classificacaoId);

        Task<bool> VerificaFormaSaidaAsync(int formaSaidaId);

        Task IncluirTransacaoAsync(DateTime dataTransacao,
                                                string descricao,
                                                decimal valor,
                                                int ClienteId,
                                                TipoTransacao tipoTransacaoId,
                                                int formaSaidaId,
                                                int ClassificacaoId);
        Task EditarTransacaoAsync(int transacaoId, 
                                  DateTime dataTransacao,
                                  string descricao,
                                  decimal valor,
                                  TipoTransacao tipoTransacaoId,
                                  int formaSaidaId,
                                  int ClassificacaoId);

        Task ExcluirTransacaoAsync(int transacaoId);


    }
}
