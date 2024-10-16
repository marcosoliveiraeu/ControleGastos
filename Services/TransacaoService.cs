using ControleGastos.Data;
using ControleGastos.DTOs;
using ControleGastos.Entities;
using ControleGastos.Enum;
using ControleGastos.Exceptions;
using ControleGastos.Repositorios.Interfaces;
using ControleGastos.Services.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ControleGastos.Services
{
    public class TransacaoService : ITransacaoService
    {

        private readonly ITransacaoRepository _transacaoRepository;

        public TransacaoService(ITransacaoRepository transacaoRepository) 
        { 
            _transacaoRepository = transacaoRepository;
        }

        public void VerificaIntegridadeTransacao(TipoTransacao tpTransacao, int formaSaida, int classificacao)
        {

            if(tpTransacao == TipoTransacao.Entrada)
            {
                // não é permitida entrada de cartão debito(1) , boleto(4) ou saque(5)
                if (formaSaida == 1 || formaSaida == 4 || formaSaida == 5  ) 
                {
                    throw new BusinessException($"Erro ao validar as informações. Tipo de transação e forma de saída inconsistentes.");
                }

            }
            else
            {
                // se for uma operação de saida de valores
                //não pode ser um saque(5)

                if (formaSaida == 5)
                {
                    throw new BusinessException($"Erro ao validar as informações. Tipo de transação e forma de saída inconsistentes.");
                }


            }

        }


        public async Task AddTransacaoAsync(TransacaoAddDto transacao)
        {

            if(!await _transacaoRepository.VerificaClassificacaoAsync(transacao.ClassificacaoId))
            {
                throw new NotFoundException($"Não existe classificação com ID {transacao.ClassificacaoId}.");
            }

            if (!await _transacaoRepository.VerificaFormaSaidaAsync(transacao.FormaSaidaId))
            {
                throw new NotFoundException($"Não existe forma de saída com ID {transacao.FormaSaidaId}.");
            }

            if( transacao.TipoTransacaoId != Enum.TipoTransacao.Entrada && transacao.TipoTransacaoId != Enum.TipoTransacao.Saida)
            {
                throw new NotFoundException($"Não existe tipo de transação com ID {transacao.TipoTransacaoId}.");
            }

            if (!await _transacaoRepository.VerificaClienteAsync(transacao.ClienteId))
            {
                throw new NotFoundException($"Cliente com ID {transacao.ClienteId} não encontrado.");
            }

            VerificaIntegridadeTransacao(transacao.TipoTransacaoId, transacao.FormaSaidaId, transacao.ClassificacaoId);

            await _transacaoRepository.IncluirTransacaoAsync(   transacao.DataTransacao ,
                                                                transacao.Descricao,
                                                                transacao.Valor,
                                                                transacao.ClienteId,
                                                                transacao.TipoTransacaoId,
                                                                transacao.FormaSaidaId,
                                                                transacao.ClassificacaoId );
           

        }

        public async Task DeleteTransacaoAsync(int transacaoId)
        {
            
            if( await _transacaoRepository.GetTransacaoById( transacaoId ) == null )
            {
                throw new NotFoundException($"Transação com ID {transacaoId} não encontrada.");
            }

            await _transacaoRepository.ExcluirTransacaoAsync(transacaoId);


        }

        public async Task EditTransacaoAsync(TransacaoEditDto transacao)
        {
            if (await _transacaoRepository.GetTransacaoById(transacao.Id) == null)
            {
                throw new NotFoundException($"Transação com ID {transacao.Id} não encontrada.");
            }

            if (!await _transacaoRepository.VerificaClassificacaoAsync(transacao.ClassificacaoId))
            {
                throw new NotFoundException($"Não existe classificação com ID {transacao.ClassificacaoId}.");
            }

            if (!await _transacaoRepository.VerificaFormaSaidaAsync(transacao.FormaSaidaId))
            {
                throw new NotFoundException($"Não existe forma de saída com ID {transacao.FormaSaidaId}.");
            }

            if (transacao.TipoTransacaoId != Enum.TipoTransacao.Entrada && transacao.TipoTransacaoId != Enum.TipoTransacao.Saida)
            {
                throw new NotFoundException($"Não existe tipo de transação com ID {transacao.TipoTransacaoId}.");
            }

            VerificaIntegridadeTransacao(transacao.TipoTransacaoId, transacao.FormaSaidaId, transacao.ClassificacaoId);

            await _transacaoRepository.EditarTransacaoAsync(transacao.Id 
                                                          , transacao.DataTransacao
                                                          , transacao.Descricao
                                                          , transacao.Valor 
                                                          , transacao.TipoTransacaoId 
                                                          , transacao.FormaSaidaId 
                                                          , transacao.ClassificacaoId );

        }

        public async Task<TransacoesResponseDto> GetTransacoesAsync(int idCliente, DateTime dtInicio, DateTime dtFim)
        {
            
            //verificar se o cliente existe na base
            // senão , lançar uma excessão
            if(!await _transacaoRepository.VerificaClienteAsync(idCliente))
            {
                throw new NotFoundException($"Cliente com ID {idCliente} não encontrado.");
            }


            var transacoes =  await _transacaoRepository.GetTransacoesAsync(idCliente, dtInicio, dtFim);

            var responseDto = new TransacoesResponseDto
            {
                Success = true,
                Message = "Transações recuperadas com sucesso.",
                Transacoes = transacoes.Select(t => new TransacaoDto
                {
                    TransacaoId = t.TransacaoId,
                    ClienteId = t.ClienteId,
                    Nome = t.Nome,
                    Email = t.Email,
                    DataTransacao = t.DataTransacao,
                    Valor = t.Valor,
                    Descricao = t.Descricao,
                    Classificacao = t.Classificacao,
                    FormaSaida = t.FormaSaida,
                    TipoTransacao = t.TipoTransacao
                }).ToList()
            };

            return responseDto;
        }
    }
}
