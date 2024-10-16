using ControleGastos.Validations;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.DTOs
{
    public class TransacaoRequestDto
    {
        /// <summary>
        ///     Id único do cliente a ser consultado
        /// </summary>
        [Required(ErrorMessage = "O ID do cliente é obrigatório.")]
        public int ClienteId { get; set; }

        /// <summary>
        ///     Data inicial do intervalo da consulta
        /// </summary>
        [Required(ErrorMessage = "A data de início é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
        public DateTime DataInicio { get; set; }

        /// <summary>
        ///     Data final do intervalo da consulta
        /// </summary>
        [Required(ErrorMessage = "A data de fim é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
        [DateGreaterThan("DataInicio", ErrorMessage = "A data de fim deve ser maior que a data de início.")]
        public DateTime DataFim { get; set; }


        
    }
}
