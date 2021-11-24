using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace BuscaCEP
{
    public partial class SiteMaster : MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        public static string GetSortDirection(string column)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            string direcao = "ASC";
            string coluna = page.Session["coluna"].ToString();
            if (coluna == column)
            {
                string ultimaDirecao = page.Session["direcao"].ToString();
                if (ultimaDirecao == "ASC")
                {
                    direcao = "DESC";
                }
            }
            page.Session["coluna"] = column;
            page.Session["direcao"] = direcao;
            return direcao;
        }
    }
}