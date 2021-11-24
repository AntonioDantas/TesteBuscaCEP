using BuscaCEP.Classes;
using BuscaCEP.Controllers;
using BuscaCEP.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BuscaCEP
{
    public partial class _Default : Page
    {
        /// <summary>
        /// URL base do correios
        /// </summary>
        public static string URL = "https://buscacepinter.correios.com.br/app/endereco/carrega-cep-endereco.php";
       
        /// <summary>
        /// Método para busca do CEP
        /// </summary>
        protected async void lnkBusca_Click(object sender, EventArgs e)
        {
            //Verificação do CEP
            Regex regex = new Regex(@"^[0-9]*$");
            string cep = txtCEP.Text;
            string cep_limpo = cep.Replace(".", "").Replace("-", "");
            if (!regex.IsMatch(cep_limpo))
            {
                MessageBox.Show("Favor digitar um CEP válido!");
                return;
            }

            #region Busca no Site do Correios
            WebRequest req = WebRequest.Create(URL);
            string postData = $"pagina=/app/endereco/index.php&cepaux=&mensagem_alerta=&endereco={cep_limpo}&tipoCEP=ALL";

            byte[] send = Encoding.Default.GetBytes(postData);
            req.Method = "POST";
            req.ContentType = "application/x-www-form-urlencoded";
            req.ContentLength = send.Length;

            Stream sout = req.GetRequestStream();
            sout.Write(send, 0, send.Length);
            sout.Flush();
            sout.Close();

            WebResponse res = req.GetResponse();
            StreamReader sr = new StreamReader(res.GetResponseStream(), Encoding.UTF8);
            string returnvalue = sr.ReadToEnd();
            #endregion

            List<Cep> lista_encontrados = new List<Cep>();
            if (returnvalue.ToUpper().Contains("DADOS NAO ENCONTRADOS"))
            {
                //Não Achou o CEP
                new CepController().Salvar(new Pesquisa
                {
                    cep = cep,
                });
                MessageBox.Show("CEP não identificado!");
            }
            else
            {
                //Achou o CEP
                Retorno retornos = JsonConvert.DeserializeObject<Retorno>(returnvalue);
                foreach (var obj in retornos.dados)
                {
                    new CepController().Salvar(new Pesquisa
                    {
                        cep = cep,
                    });
                    string logradouro = obj.logradouroDNEC.ToUpper();
                    string bairro = obj.bairro.ToUpper();
                    string sigla = obj.uf.ToUpper();
                    string municipio = obj.localidade.ToUpper();


                    #region Verificando o cadastro do estado
                    var parametros = new List<OleDbParameter>();
                    parametros.Add(new OleDbParameter("Sigla", sigla));
                    var estados = new CepController().GetEstados($" AND Sigla = ?", parametros);
                    if (estados.Count == 0)
                    {
                        new CepController().Salvar(new Estado
                        {
                            sigla = sigla
                        });
                        estados = new CepController().GetLastEstado(1);
                    }
                    #endregion

                    #region Verificando o cadastro da cidade
                    parametros = new List<OleDbParameter>();
                    parametros.Add(new OleDbParameter("Nome", municipio));
                    var cidades = new CepController().GetCidades($" AND Nome = ? COLLATE Latin1_General_CI_AI", parametros);
                    if (cidades.Count == 0)
                    {
                        new CepController().Salvar(new Cidade
                        {
                            nome = municipio,
                            estado = estados.FirstOrDefault()
                        });
                        cidades = new CepController().GetLastCidade(1);
                    }
                    #endregion

                    #region Verificando o CEP
                    parametros = new List<OleDbParameter>();
                    parametros.Add(new OleDbParameter("Cep", cep));
                    var ceps = new CepController().GetCeps($" AND Cep = ? ", parametros);
                    if (ceps.Count == 0)
                    {
                        new CepController().Salvar(new Cep
                        {
                            cep = cep,
                            logradouro = logradouro,
                            bairro = bairro,
                            cidade = cidades.FirstOrDefault()
                        });
                        parametros = new List<OleDbParameter>();
                        parametros.Add(new OleDbParameter("Cep", cep));
                        ceps = new CepController().GetCeps($" AND Cep = ? ", parametros);
                    }
                    lista_encontrados.Add(ceps.FirstOrDefault());
                    #endregion
                }
            }

            //Apresentando dados
            grdResultado.DataSource = lista_encontrados;
            grdResultado.DataBind();
        }
    }
}