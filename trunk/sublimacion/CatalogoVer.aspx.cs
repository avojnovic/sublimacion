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
    public partial class CatalogoVer : System.Web.UI.Page
    {
        private Dictionary<long, Catalogo> _dicCatalogo;

        protected void Page_Load(object sender, EventArgs e)
        {
            cargarGrilla();
            setearGrillaSiEstaVacia();
        }

        private void cargarGrilla()
        {
            _dicCatalogo = CatalogoDAO.obtenerCatalogoTodos();

            GridView1.DataSource = _dicCatalogo.Values.ToList();
            GridView1.DataBind();
        }
        
        private void setearGrillaSiEstaVacia()
        {

            if (GridView1.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdCatalogo");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Fecha");
              
                

                dt.Rows.Add(new object[] { "", "", ""});

                GridView1.Columns[GridView1.Columns.Count].Visible = false;

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {

            GridViewRow row = GridView1.SelectedRow;
            if (row != null)
            {
              
                long id = long.Parse(row.Cells[1].Text);
                Response.Redirect("CatalogoABM.aspx?id=" + id.ToString());
           }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {

            Response.Redirect("CatalogoABM.aspx");
        }
               
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
    }
}