using ControleGastos.DTOs;
using ControleGastos.Exceptions;
using ControleGastos.Services.Interfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace ControleGastos.Controllers
{
    
    [ApiController]
    [Route("api/[controller]")]
    public class TransacaoController : ControllerBase
    {

        private readonly ITransacaoService _transacaoService;

        public TransacaoController(ITransacaoService transacaoService)
        {
            _transacaoService = transacaoService;
        }


        /// <summary>
        ///     Consulta de Transações
        /// </summary>
        /// <remarks>
        ///     Retorna as transações realizadas para determinado cliente no intervalo de datas informado via parametros
        /// </remarks>
        /// <param name="requestDto"></param>
        /// <returns></returns>
        /// <response code="200">Lista de transações</response>
        /// <response code="400">Parâmetros inválidos</response>
        /// <response code="404">Cliente não encontrado</response>
        /// <response code="500">Erro interno</response>
        [ProducesResponseType(typeof(TransacoesResponseDto),StatusCodes.Status200OK)]
        [ProducesResponseType( StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpGet("getTransacoes")]
        public async Task<ActionResult<TransacoesResponseDto>> GetTransacoes([FromQuery] TransacaoRequestDto requestDto)
        {
            var  response =  await _transacaoService.GetTransacoesAsync(requestDto.ClienteId, requestDto.DataInicio, requestDto.DataFim);

            if (response.Transacoes.Any())
            {
                return Ok(response);
            }
            else
            {
                throw new NotFoundException("Nenhuma transação encontrada para os parâmetros solicitados");
            }
            
        }

        /// <summary>
        ///     Incluir nova transação 
        /// </summary>
        /// <remarks>
        ///     Inclui uma nova transação para determinado cliente
        /// </remarks>
        /// <response code="200">Transação inserida com sucesso</response>
        /// <response code="404">Cliente, classificação, tipo de transação ou forma de saída não encontrados</response>
        /// <response code="500">Erro interno</response>
        [ProducesResponseType(typeof(ApiResponseBase), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPost("addTransacao")]
        public async Task<ActionResult<TransacaoAddDto>> AddTransacao(TransacaoAddDto postDto)
        {

            await _transacaoService.AddTransacaoAsync(postDto);


            return Ok(new ApiResponseBase 
                            { Success = true 
                            , Message = "Transação incluída com sucesso."
                            });

        }


        /// <summary>
        ///     Excluir uma transação 
        /// </summary>
        /// <remarks>
        ///     Exclui uma transação de acordo com o Id informado
        /// </remarks>
        /// <response code="200">Transação excluída com sucesso</response>
        /// <response code="404">Transação não encontrada</response>
        /// <response code="500">Erro interno</response>
        [ProducesResponseType(typeof(ApiResponseBase), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpDelete("deleteTransacao")]
        public async Task<ActionResult> DeleteTransacao(int idTransacao)
        {

            await _transacaoService.DeleteTransacaoAsync(idTransacao);

            return Ok(new ApiResponseBase
                {
                    Success = true                                ,
                    Message = "Transação excluída com sucesso."
                });


        }


        /// <summary>
        ///     Editar uma transação 
        /// </summary>
        /// <remarks>
        ///     Edita uma transação de acordo com o dados informados
        /// </remarks>
        /// <response code="200">Transação alterada com sucesso</response>
        /// <response code="400">Parâmetros inconsistentes</response>
        /// <response code="404">Transação , classificação, tipo de transação ou forma de saída não encontrados</response>
        /// <response code="500">Erro interno</response>
        [ProducesResponseType(typeof(ApiResponseBase), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ProblemDetails), StatusCodes.Status500InternalServerError)]
        [HttpPut("editTransacao")]
        public async Task<ActionResult> EditTransacao(TransacaoEditDto transacao)
        {

            await _transacaoService.EditTransacaoAsync(transacao);

            return Ok(new ApiResponseBase
            {
                Success = true
                            ,
                Message = "Transação alterada com sucesso."
            });
        }



    }



}
