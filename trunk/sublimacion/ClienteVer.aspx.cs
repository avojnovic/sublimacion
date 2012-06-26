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
using System.Collections.Generic;
using sublimacion.DataAccessObjects.DataAccessObjects;
using sublimacion.BussinesObjects.BussinesObjects;

namespace sublimacion
{
    public partial class ClienteVer : System.Web.UI.Page
    {
        private Dictionary<long, Cliente> _dicCliente;

        protected void Page_Load(object sender, EventArgs e)
        {

            cargarGrilla();
            
            setearGrillaSiEstaVacia();
        }

        private void cargarGrilla()
        {
            _dicCliente = ClienteDAO.obtenerClienteTodos();

            GridView1.DataSource = _dicCliente.Values.ToList();
            GridView1.DataBind();

        }


        private void setearGrillaSiEstaVacia()
        {

            if (GridView1.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdClienteStr");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Apellido");
                dt.Columns.Add("Dni");
                dt.Columns.Add("Direccion");
                dt.Columns.Add("Telefono");
                dt.Columns.Add("Mail");
                dt.Columns.Add("Fecha");
                
                    


                dt.Rows.Add(new object[] { "", "", "", "", "", "","","" });

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {

            Response.Redirect("ClienteABM.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }


    }
}