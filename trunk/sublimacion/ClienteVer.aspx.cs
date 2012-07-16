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
        private List<Cliente> _Clientes;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _Clientes = ClienteDAO.obtenerClienteTodos().Values.ToList();
                cargarGrilla();
            }
        }

        private void cargarGrilla()
        {
           

            GridView1.DataSource = _Clientes;
            GridView1.DataBind();

            setearGrillaSiEstaVacia();
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


        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            string buscar = txtBuscar.Text.Trim().ToLower();
            _Clientes = ClienteDAO.obtenerClienteTodos().Values.ToList();

            var newList = (from x in _Clientes
                           where x.Apellido.ToLower().Contains(buscar) 
                           || x.Nombre.ToLower().Contains(buscar) 
                           || x.Dni.ToString().ToLower().Contains(buscar)
                           || x.Mail.ToString().ToLower().Contains(buscar)
                           || x.Telefono.ToString().ToLower().Contains(buscar)
                           || x.Direccion.ToString().ToLower().Contains(buscar)
                           select x).ToList();

            _Clientes = newList;
            cargarGrilla();
        }

        protected void Btnlimpiar_Click(object sender, EventArgs e)
        {
            txtBuscar.Text = "";
            _Clientes = ClienteDAO.obtenerClienteTodos().Values.ToList();
            cargarGrilla();
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