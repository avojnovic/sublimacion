using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using sublimacion.BussinesObjects.BussinesObjects;
using sublimacion.DataAccessObjects.DataAccessObjects;
using System.Data;

namespace sublimacion
{
    public partial class UsuarioVer : System.Web.UI.Page
    {
        private Dictionary<long, Usuario> _dicUsuario;

        protected void Page_Load(object sender, EventArgs e)
        {

            cargarGrilla();

           
        }

        private void cargarGrilla()
        {
            _dicUsuario = UsuarioDAO.obtenerTodos();

            GridView1.DataSource = _dicUsuario.Values.ToList();
            GridView1.DataBind();

            setearGrillaSiEstaVacia();

        }


        private void setearGrillaSiEstaVacia()
        {

            if (GridView1.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Id");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Apellido");
                dt.Columns.Add("User");
                dt.Columns.Add("Password");
                dt.Columns.Add("Telefono");
                dt.Columns.Add("Mail");
                dt.Columns.Add("Perfil");




                dt.Rows.Add(new object[] { "", "", "", "", "", "", "", "" });

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {

            Response.Redirect("UsuarioABM.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }
    }
}