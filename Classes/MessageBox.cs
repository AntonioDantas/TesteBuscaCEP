
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;

namespace BuscaCEP.Classes
{
    public class MessageBox
    {
        /// <summary>
        /// Classe destinada a exibição prioritário de script registrado 
        /// </summary>
        /// <param name="message">Mensagem de Alerta</param>
        public static void Show(string message)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            if ((!page.ClientScript.IsClientScriptBlockRegistered("alert")))
            {
                page.ClientScript.RegisterStartupScript(page.Page.GetType(), "Alerta", "alert('" + message + "');", true);
            }
        }

        /// <summary>
        /// Classe destinada a exibição prioritário de script registrado com redirecionamento
        /// </summary>
        /// <param name="message">Mensagem de Alerta</param>
        /// <param name="path">Página de redirecionamento</param>
        public static void ShowAndRedirect(string message, string path)
        {
            Page page = HttpContext.Current.CurrentHandler as Page;
            if ((!page.ClientScript.IsClientScriptBlockRegistered("alert")))
            {
                page.ClientScript.RegisterClientScriptBlock(page.GetType(), "alert", "<script type='text/javascript'>alert('" + message + "'); window.location.href='" + path + "';</script>");
            }
        }
    }
}