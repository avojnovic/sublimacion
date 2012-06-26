using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using sublimacion.DataAccessObjects.DataAccessObjects;
using System.Data;
using sublimacion.BussinesObjects.BussinesObjects;

namespace sublimacion
{
    public partial class ProductoVer : System.Web.UI.Page
    {
        private Dictionary<long, Producto> _dicProducto;

        protected void Page_Load(object sender, EventArgs e)
        {
            cargarGrilla();
            setearGrillaSiEstaVacia();
        }

        private void cargarGrilla()
        {
            _dicProducto = ProductoDAO.obtenerProductoTodos();

            GridView1.DataSource = _dicProducto.Values.ToList();
            GridView1.DataBind();
        }
        
        private void setearGrillaSiEstaVacia()
        {

            if (GridView1.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdProducto");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Precio");
                dt.Columns.Add("Cantidad");
                dt.Columns.Add("Costo");
                dt.Columns.Add("Tiempo");
                

                dt.Rows.Add(new object[] { "", "", "", "", "", ""});

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
                Response.Redirect("ProductoABM.aspx?id=" + id.ToString());
           }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {

            Response.Redirect("ProductoABM.aspx");
        }
               
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
    
    }
}