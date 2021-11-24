using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuscaCEP.Models
{
    /// <summary>
    /// Modelo para registro do CEP no sistema
    /// </summary>
    public class Cep
    {
        /// <summary>
        /// CEP formatado
        /// </summary>
        public string cep { get; set; }
        /// <summary>
        /// Logradouro registrado no BD
        /// </summary>
        public string logradouro { get; set; }
        /// <summary>
        /// Bairro  registrado no BD
        /// </summary>
        public string bairro { get; set; }
        /// <summary>
        /// Cidade registrado no BD
        /// </summary>
        public Cidade cidade { get; set; }
        /// <summary>
        /// Método para retorno das informações do CEP
        /// </summary>
        public string exibir { get => $"Logradouro: {logradouro} Bairro:{bairro} Cidade:{cidade.nome} Estado:{cidade.estado.sigla}";  }
    }

    /// <summary>
    /// Modelo para registro da cidade no sistema
    /// </summary>
    public class Cidade
    {
        /// <summary>
        /// Id registrado no BD
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Nome da cidade registrado no BD
        /// </summary>
        public string nome { get; set; }
        /// <summary>
        /// Estado vinculado
        /// </summary>
        public Estado estado { get; set; }
    }
    public class Estado
    {
        /// <summary>
        /// Id registrado no BD
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// Sigla registrado no BD
        /// </summary>
        public string sigla { get; set; }
    }
    public class Pesquisa
    {
        /// <summary>
        /// Id registrado no BD
        /// </summary>
        public int id { get; set; }
        /// <summary>
        /// CEP pesquisado pelo usuário
        /// </summary>
        public string cep { get; set; }
        /// <summary>
        /// Data e Hora da consulta
        /// </summary>
        public DateTime datahora { get; set; }
        /// <summary>
        /// Resultado da busca para apresentação
        /// </summary>
        public string resultado { get; set; }
    }

}