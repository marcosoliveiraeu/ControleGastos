using ControleGastos.Data;
using ControleGastos.DTOs;
using ControleGastos.Entities;
using ControleGastos.Enum;
using ControleGastos.Exceptions;
using ControleGastos.Repositorios.Interfaces;
using ControleGastos.Services.Interfaces;
using Dapper;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Repositorios
{
    public class TransacaoRepository : ITransacaoRepository
    {

        private readonly DataContext _dataContext;


        public TransacaoRepository(DataContext dataContext) 
        { 
            _dataContext = dataContext;
        }

        public async Task<bool> VerificaClienteAsync(int clienteId)
        {
            try
            {
                var cliente = await _dataContext.Clientes.FindAsync(clienteId);

                if (cliente == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Erro no acesso ao banco de dados: " + ex.Message);
            }
        }


        public async Task IncluirTransacaoAsync(DateTime dataTransacao , 
                                                string descricao , 
                                                decimal valor, 
                                                int ClienteId, 
                                                TipoTransacao tipoTransacaoId,
                                                int formaSaidaId,
                                                int ClassificacaoId)
        {
            try
            {

                var transacao = new Transacao();

                var cli = await _dataContext.Clientes.FindAsync(ClienteId);
                var formaSaida = await _dataContext.FormasSaida.FindAsync(formaSaidaId);
                var classificacao = await _dataContext.Classificacoes.FindAsync(ClassificacaoId);
      
                transacao.DataTransacao = dataTransacao;
                transacao.Descricao = descricao;
                transacao.Valor = valor;
                transacao.Cliente = cli;
                transacao.TipoTransacao = tipoTransacaoId;
                transacao.FormaSaida = formaSaida;
                transacao.Classificacao = classificacao;

                _dataContext.Transacoes.Add(transacao);
                await _dataContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new DatabaseException("Erro ao incluir no banco de dados: " + ex.Message);
            }
        }


        public async Task<Transacao> GetTransacaoById(int id)
        {
            try
            {

                return await _dataContext.Transacoes.FindAsync(id);
                

            }catch (Exception ex)
            {
                throw new DatabaseException("Erro no acesso ao banco de dados: " + ex.Message);
            }
        }


        public async Task<List<TransacaoDto>> GetTransacoesAsync(int clienteId, DateTime dataInicio, DateTime dataFim)
        {

            try
            {

                using (var connection = _dataContext.Database.GetDbConnection())
                {

                    if (connection.State == System.Data.ConnectionState.Closed)
                    {
                        connection.Open();
                    }

                    var query = "EXEC sp_getTransacoes @ClienteId, @DataInicio, @DataFim";
                    var transacoes = await connection.QueryAsync<TransacaoDto>(query, new { ClienteId = clienteId, DataInicio = dataInicio, DataFim = dataFim });

                    return transacoes.ToList();
                }

                
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Erro no acesso ao banco de dados: " + ex.Message);
            }

            
        }

        public async Task<bool> VerificaClassificacaoAsync(int classificacaoId)
        {
            try
            {
                var classificacao = await _dataContext.Classificacoes.FindAsync(classificacaoId);

                if (classificacao == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Erro no acesso ao banco de dados: " + ex.Message);
            }
        }

        public async Task<bool> VerificaFormaSaidaAsync(int formaSaidaId)
        {
            try
            {
                var formaSaida = await _dataContext.FormasSaida.FindAsync(formaSaidaId);

                if (formaSaida == null)
                {
                    return false;
                }

                return true;
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Erro no acesso ao banco de dados: " + ex.Message);
            }
        }

        public async Task EditarTransacaoAsync(int transacaoId, DateTime dataTransacao, string descricao, decimal valor, TipoTransacao tipoTransacaoId, int formaSaidaId, int ClassificacaoId)
        {
            try
            {
                var t = await _dataContext.Transacoes.FindAsync(transacaoId);
                var fs = await _dataContext.FormasSaida.FindAsync(formaSaidaId);
                var c = await _dataContext.Classificacoes.FindAsync(ClassificacaoId);


                t.DataTransacao = dataTransacao;
                t.Descricao = descricao;
                t.Valor = valor;
                t.TipoTransacao = tipoTransacaoId;
                t.FormaSaida = fs;
                t.Classificacao = c;

                _dataContext.Transacoes.Update(t);
                await _dataContext.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                throw new DatabaseException("Erro no acesso ao banco de dados: " + ex.Message);
            }
        }

        public async Task ExcluirTransacaoAsync(int transacaoId)
        {
            try
            {
                var t = await _dataContext.Transacoes.FindAsync(transacaoId);

                _dataContext.Transacoes.Remove(t); 
                await _dataContext.SaveChangesAsync();
                                
            }
            catch (Exception ex)
            {
                throw new DatabaseException("Erro no acesso ao banco de dados: " + ex.Message);
            }
        }
    }
}
