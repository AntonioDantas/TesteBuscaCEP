using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace BuscaCEP.Models
{
    /// <summary>
    /// Classe para retorno das informações do correios
    /// </summary>
    public class Retorno
    {
        [JsonProperty(PropertyName = "erro")]
        public string erro { get; set; }

        [JsonProperty(PropertyName = "mensagem")]
        public string mensagem { get; set; }

        [JsonProperty(PropertyName = "total")]
        public int total { get; set; }

        [JsonProperty(PropertyName = "dados")]
        public List<Dados> dados { get; set; }
    }
    public class Dados
    {
        public string uf { get; set; }
        public string localidade { get; set; }
        public string locNoSem { get; set; }
        public string locNu { get; set; }
        public string localidadeSubordinada { get; set; }
        public string logradouroDNEC { get; set; }
        public string logradouroTextoAdicional { get; set; }
        public string logradouroTexto { get; set; }
        public string bairro { get; set; }
        public string baiNu { get; set; }
        public string nomeUnidade { get; set; }
        public string cep { get; set; }
        public string tipoCep { get; set; }
        public string numeroLocalidade { get; set; }
        public string situacao { get; set; }
        public List<string> faixasCaixaPostal { get; set; }
        public List<string> faixasCep { get; set; }

    }
}