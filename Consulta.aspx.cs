using BuscaCEP.Controllers;
using BuscaCEP.Models;
using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BuscaCEP
{
    public partial class Consulta : System.Web.UI.Page
    {
        /// <summary>
        /// Método para pesquisa
        /// </summary>
        protected void btnPesquisar_Click(object sender, EventArgs e)
        {
            (var filtro, var parametros) = CarregarFiltros();
            var classes = new CepController().GetPesquisa(filtro, parametros);

            Session["coluna"] = "Datahora";
            divResultado.Visible = true;
            grdResultado.DataSource = classes;
            grdResultado.DataBind();
            lblTotal.Text = $"<b>Total de buscas cadastrados: </b> {classes.Count}.";
            Session["consultaClasse"] = classes;
        }

        /// <summary>
        /// Método para verificação dos filtros
        /// </summary>
        /// <returns>Lista de filtros</returns>
        public (string, List<OleDbParameter>) CarregarFiltros()
        {
            var filtros = "";
            var param = new List<OleDbParameter>();

            if (txtCEP.Text != "")
            {
                filtros += $" AND p.Cep like ? COLLATE Latin1_General_CI_AI";
                param.Add(new OleDbParameter("Cep", $"%{txtCEP.Text}%"));
            }

            if (txtEndereco.Text != "")
            {
                filtros += $@" AND EXISTS (SELECT * FROM Ceps c WHERE c.cep = p.cep AND logradouro like ? COLLATE Latin1_General_CI_AI)";
                param.Add(new OleDbParameter("logradouro", $"%{txtEndereco.Text}%"));
            }

            DateTime data = DateTime.Now;
            if (DateTime.TryParse(txtData.Text, out data) )
            {
                filtros += $" AND CONVERT(DATE,p.Datahora) = ? ";
                param.Add(new OleDbParameter("Datahora", $"{data.ToShortDateString()}"));
            }

            return (filtros, param);
        }

        /// <summary>
        /// Método para Alteração de páginas
        /// </summary>
        protected void grdResultado_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            grdResultado.PageIndex = e.NewPageIndex;
            grdResultado.DataSource = (List<Pesquisa>)Session["consultaClasse"];
            grdResultado.DataBind();
        }

    }
}