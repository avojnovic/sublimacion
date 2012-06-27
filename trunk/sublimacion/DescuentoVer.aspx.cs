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
    public partial class DescuentoVer : System.Web.UI.Page
    {
        private List<Descuento> _listDescuento;



        protected void Page_Load(object sender, EventArgs e)
        {

            cargarGrilla();

            setearGrillaSiEstaVacia();
        }

        private void cargarGrilla()
        {
            _listDescuento = DescuentoDAO.obtenerDescuentoTodos();

            GridView1.DataSource = _listDescuento;

            GridView1.DataBind();

        }


        private void setearGrillaSiEstaVacia()
        {

            if (GridView1.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("productoId");
                dt.Columns.Add("productoNombre");
                dt.Columns.Add("Cantidad");
                dt.Columns.Add("Descuento1");
                dt.Columns.Add("Fecha");
              




                dt.Rows.Add(new object[] { "", "", "", "", "" });

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {

            Response.Redirect("DescuentoABM.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

    }
}