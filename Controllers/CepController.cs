using BuscaCEP.Classes;
using BuscaCEP.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace BuscaCEP.Controllers
{
    /// <summary>
    /// Classe de controle dos métodos de CEP
    /// </summary>
    public class CepController
    {
        /// <summary>
        /// Método para buscar único CEP
        /// </summary>
        /// <param name="cep">CEP de interesse</param>
        /// <returns>Objeto CEP</returns>
        public Cep GetCep(string cep)
        {
            var parametros = new List<OleDbParameter>();
            parametros.Add(new OleDbParameter("Cep", cep));
            return GetCeps($" AND Cep = ?", parametros).FirstOrDefault();
        }
        /// <summary>
        /// Método para buscar única cidade
        /// </summary>
        /// <param name="Id">Id da cidade</param>
        /// <returns>Obj Cidade</returns>
        public Cidade GetCidade(int Id)
        {
            var parametros = new List<OleDbParameter>();
            parametros.Add(new OleDbParameter("Id", Id.ToString()));
            return GetCidades($" AND Id = ?", parametros).FirstOrDefault();
        }
        /// <summary>
        /// Método para buscar único estado
        /// </summary>
        /// <param name="Id">Id do estado</param>
        /// <returns>Obj Estado</returns>
        public Estado GetEstado(int Id)
        {
            var parametros = new List<OleDbParameter>();
            parametros.Add(new OleDbParameter("Id", Id.ToString()));
            return GetEstados($" AND Id = ?", parametros).FirstOrDefault();
        }
        /// <summary>
        /// Método para retornar as buscas realizadas 
        /// </summary>
        /// <param name="filtros">Script dos filtros</param>
        /// <param name="parameters">Parâmetros dos filtros</param>
        /// <returns>Lista de pesquisas</returns>
        public List<Pesquisa> GetPesquisa(string filtros, List<OleDbParameter> parameters)
        {
            var list = new List<Pesquisa>();
            var select = $@"SELECT * FROM Pesquisas p WHERE (1=1) {filtros} ORDER BY datahora DESC";
            try
            {
                var dt = new ConnectionFactory().ExecuteToDataTable(select, parameters);
                foreach (DataRow dr in dt.Rows)
                {
                    var parametros = new List<OleDbParameter>();
                    parametros.Add(new OleDbParameter("CEP", dr["cep"].ToString()));
                    var busca = new CepController().GetCeps(" AND cep = ? ", parametros);

                    list.Add(new Pesquisa
                    {
                        id = Convert.ToInt32(dr["Id"]),
                        datahora = Convert.ToDateTime(dr["Datahora"]),
                        cep = dr["cep"].ToString(),
                        resultado = (busca.Count > 0) ? busca.FirstOrDefault().exibir : "CEP não encontrado"
                    });

                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }
        /// <summary>
        /// Método para retornar ceps cadastrados
        /// </summary>
        /// <param name="filtros">Script dos filtros</param>
        /// <param name="parameters">Parâmetros dos filtros</param>
        /// <param name="top">Limitação de resultados</param>
        /// <returns>Lista de CEPs</returns>
        public List<Cep> GetCeps(string filtros, List<OleDbParameter> parameters, long top = long.MaxValue)
        {
            var list = new List<Cep>();
            var select = $@"SELECT TOP {top} * FROM CEPs WHERE (1=1) {filtros} ORDER BY cep";
            try
            {
                var dt = new ConnectionFactory().ExecuteToDataTable(select, parameters);
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new Cep
                    {
                        cep = dr["cep"].ToString(),
                        logradouro = dr["logradouro"].ToString(),
                        bairro = dr["bairro"].ToString(),
                        cidade = new CepController().GetCidade(Convert.ToInt32(dr["IdCidade"].ToString()))
                    });

                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }
        /// <summary>
        /// Método para retornar cidades cadastrados
        /// </summary>
        /// <param name="filtros">Script dos filtros</param>
        /// <param name="parameters">Parâmetros dos filtros</param>
        /// <param name="top">Limitação de resultados</param>
        /// <returns>Lista de Cidades</returns>
        public List<Cidade> GetCidades(string filtros, List<OleDbParameter> parameters, long top = long.MaxValue)
        {
            var list = new List<Cidade>();
            var select = $@"SELECT TOP {top} * FROM Cidades WHERE (1=1) {filtros} ORDER BY id DESC";
            try
            {
                var dt = new ConnectionFactory().ExecuteToDataTable(select, parameters);
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new Cidade
                    {
                        id = Convert.ToInt32(dr["Id"]),
                        nome = dr["nome"].ToString(),
                        estado = new CepController().GetEstado(Convert.ToInt32(dr["IdEstado"].ToString()))
                    });

                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }
        /// <summary>
        /// Método para retornar estados cadastrados
        /// </summary>
        /// <param name="filtros">Script dos filtros</param>
        /// <param name="parameters">Parâmetros dos filtros</param>
        /// <param name="top">Limitação de resultados</param>
        /// <returns>Lista de Estados</returns>
        public List<Estado> GetEstados(string filtros, List<OleDbParameter> parameters, long top = long.MaxValue)
        {
            var list = new List<Estado>();
            var select = $@"SELECT TOP {top} * FROM Estados WHERE (1=1) {filtros} ORDER BY id DESC";
            try
            {
                var dt = new ConnectionFactory().ExecuteToDataTable(select, parameters);
                foreach (DataRow dr in dt.Rows)
                {
                    list.Add(new Estado
                    {
                        id = Convert.ToInt32(dr["Id"]),
                        sigla = dr["sigla"].ToString()
                    });

                }
            }
            catch (Exception ex)
            {
            }
            return list;
        }

        /// <summary>
        /// Método para salvar novo CEP
        /// </summary>
        /// <param name="cep">Objeto CEP</param>
        /// <returns>Verdadeiro para sucesso</returns>
        public bool Salvar(Cep cep)
        {
            var parametros = new List<OleDbParameter>();
            parametros.Add(new OleDbParameter("Cep", cep.cep));
            parametros.Add(new OleDbParameter("Logradouro", cep.logradouro));
            parametros.Add(new OleDbParameter("Bairro", cep.bairro));
            parametros.Add(new OleDbParameter("IdCidade", cep.cidade.id));
            var salvar = $"INSERT INTO Ceps (Cep, Logradouro, Bairro, IdCidade) VALUES (?, ?, ?, ?)";
            return new ConnectionFactory().executeNonQuery(salvar, parametros) > 0;
        }
        /// <summary>
        /// Método para salvar nova Cidade
        /// </summary>
        /// <param name="cep">Objeto Cidade</param>
        /// <returns>Verdadeiro para sucesso</returns>
        public bool Salvar(Cidade cidade)
        {
            var parametros = new List<OleDbParameter>();
            parametros.Add(new OleDbParameter("IdEstado", cidade.estado.id));
            parametros.Add(new OleDbParameter("Nome", cidade.nome));
            var salvar = $"INSERT INTO Cidades (IdEstado, Nome) VALUES (?, ?)";
            return new ConnectionFactory().executeNonQuery(salvar, parametros) > 0;
        }
        /// <summary>
        /// Método para salvar novo Estado
        /// </summary>
        /// <param name="cep">Objeto Estado</param>
        /// <returns>Verdadeiro para sucesso</returns>
        public bool Salvar(Estado estado)
        {
            var parametros = new List<OleDbParameter>();
            parametros.Add(new OleDbParameter("Sigla", estado.sigla));
            var salvar = $"INSERT INTO Estados (Sigla) VALUES (?)";
            return new ConnectionFactory().executeNonQuery(salvar, parametros) > 0;
        }
        /// <summary>
        /// Método para salvar nova Pesquisa
        /// </summary>
        /// <param name="cep">Objeto Pesquisa</param>
        /// <returns>Verdadeiro para sucesso</returns>
        public bool Salvar(Pesquisa pesquisa)
        {
            var parametros = new List<OleDbParameter>();
            parametros.Add(new OleDbParameter("Cep", pesquisa.cep));
            var salvar = $"INSERT INTO Pesquisas (Cep, DataHora) VALUES (?, GETDATE())";
            return new ConnectionFactory().executeNonQuery(salvar, parametros) > 0;
        }
        /// <summary>
        /// Método para retornar último(s) cidades cadastradas
        /// </summary>
        /// <param name="qtd">Quantidade de itens a serem retornados</param>
        /// <returns>Lista de cidades</returns>
        public List<Cidade> GetLastCidade(int qtd)
        {
            return GetCidades("", new List<OleDbParameter>(), qtd);
        }
        /// <summary>
        /// Método para retornar último(s) estados cadastradas
        /// </summary>
        /// <param name="qtd">Quantidade de itens a serem retornados</param>
        /// <returns>Lista de estados</returns>
        public List<Estado> GetLastEstado(int qtd)
        {
            return GetEstados("", new List<OleDbParameter>(), qtd);
        }
    }
}