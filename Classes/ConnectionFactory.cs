using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.OleDb;
using System.Linq;
using System.Web;

namespace BuscaCEP.Classes
{
    public class ConnectionFactory
    {
        private OleDbConnection _Connection;
        public ConnectionFactory()
        {
            try
            {
                string conexao = ConfigurationManager.ConnectionStrings["defaultConnection"].ConnectionString;
                _Connection = new OleDbConnection(conexao);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        /// <summary>
        /// Método para fechar a conexão com o banco de dados após realização das queries
        /// </summary>
        public void CloseConnection()
        {
            _Connection.Close();
        }

        /// <summary>
        /// Método que devolve um objeto DataTable genérico a partir de uma consulta SQL.
        /// </summary>
        /// <param name="sql">String SQL</param>
        /// <param name="parameters">Parâmetros da String SQL</param>
        /// <returns>DataTable genérico com os campos do SQL passado por parâmetro.</returns>
        public DataTable ExecuteToDataTable(string sql, List<OleDbParameter> parameters)
        {
            try
            {
                _Connection.Open();
            }
            catch
            {
                _Connection.Close();
                _Connection.Open();
            }

            try
            {
                OleDbCommand command = new OleDbCommand(sql, _Connection);
                foreach (var p in parameters)
                    command.Parameters.Add(p);
                OleDbDataReader reader = command.ExecuteReader();
                var dt = new DataTable();
                dt.Load(reader);
                return dt;
            }
            catch (Exception err)
            {

                return null;
            }
            finally
            {
                _Connection.Close();
            }
        }

        /// <summary>
        /// Método que executa uma função SQL de inserção/alteração
        /// </summary>
        /// <param name="sql">Instrução a ser executada</param>
        /// <param name="parameters">Parâmetros da String SQL</param>
        /// <returns>Número de linhas afetadas</returns>
        public int executeNonQuery(string sql, List<OleDbParameter> parameters)
        {
            try
            {
                _Connection.Open();
            }
            catch
            {
                _Connection.Close();
                _Connection.Open();
            }

            try
            {
                OleDbCommand command = new OleDbCommand(sql, _Connection);
                foreach (var p in parameters)
                    command.Parameters.Add(p);
                return (int)command.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                return -1;
            }
            finally
            {
                _Connection.Close();
            }
        }
    }
}