using System;
using System.Collections;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using sublimacion.BussinesObjects;
using sublimacion.DataAccessObjects.DataAccessObjects;

namespace sublimacion
{
    public partial class Login : System.Web.UI.Page
    {
        public Usuario user;

        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void LoginCmp_LoggingIn(object sender, LoginCancelEventArgs e)
        {

        }

        protected void LoginCmp_Authenticate(object sender, AuthenticateEventArgs e)
        {
            user=null;

            if (LoginCmp.Password.Trim() != "" && LoginCmp.UserName.Trim() != "")
            {
                user =UsuarioDAO.Instancia.obtenerUsuario(LoginCmp.UserName.Trim(), LoginCmp.Password.Trim());
            
            }

            if (user != null && user.Id != 0)
            {
                Session["usuario"]=user;
                FormsAuthentication.RedirectFromLoginPage(user.Nombre, false);
            
            }
        }
    }
}
