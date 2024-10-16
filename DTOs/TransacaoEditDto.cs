using ControleGastos.Enum;
using System.ComponentModel.DataAnnotations;

namespace ControleGastos.DTOs
{
    public class TransacaoEditDto
    {

        /// <summary>
        ///     Chave unica da transação
        /// </summary>
        [Required(ErrorMessage = "Informe o Id da transação")]
        public int Id { get; set; }


        /// <summary>
        ///     Data em que foi realizada a transação
        /// </summary>
        [Required(ErrorMessage = "A data da transação é obrigatória.")]
        [DataType(DataType.Date, ErrorMessage = "Formato de data inválido.")]
        public DateTime DataTransacao { get; set; }

        /// <summary>
        ///     Descrição da transação
        /// </summary>
        [Required(ErrorMessage = "Informe uma descrição para a transação.")]
        [StringLength(100, ErrorMessage = "O numero máximo de caracteres da descrição é 100.")]
        [MinLength(4)]
        public string Descricao { get; set; }

        /// <summary>
        ///     Valor da transação
        /// </summary>
        [Required(ErrorMessage = "Informe o valor referente a transação")]
        public decimal Valor { get; set; }


        /// <summary>
        ///     Id do tipo da transação , 0 para Entrada ou 1 para Saída de valor
        /// </summary>
        [Required(ErrorMessage = "Informe o ID do tipo de transação")]
        public TipoTransacao TipoTransacaoId { get; set; }

        /// <summary>
        ///     Id da Forma de saída : 
        ///     1 =	Cartão Débito , 
        ///     2 =	Cartão Crédito , 
        ///     3 =	Pix , 
        ///     4 = Boleto , 
        ///     5 = Saque
        /// </summary>
        [Required(ErrorMessage = "Informe o ID da forma de saída")]
        public int FormaSaidaId { get; set; }

        /// <summary>
        ///     Id da classificação da transação : 
        ///     1 =	Mercado , 
        ///     2 =	Lazer , 
        ///     3 =	Combustivel , 
        ///     4 =	Farmacia , 
        ///     5 =	Pets , 
        ///     6 =	Comida extra , 
        ///     7 = outros
        /// </summary>
        [Required(ErrorMessage = "Informe o ID da classificação")]
        public int ClassificacaoId { get; set; }
    }
}
