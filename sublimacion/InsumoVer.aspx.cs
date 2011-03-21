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

namespace sublimacion
{
    public partial class InsumoVer : System.Web.UI.Page
    {

        private Dictionary<long, BussinesObjects.Insumo> _dicInsumo;

        protected void Page_Load(object sender, EventArgs e)
        {
            cargarGrilla();
            setearGrillaSiEstaVacia();
        }

        private void cargarGrilla()
        {
            _dicInsumo = InsumoDAO.Instancia.obtenerTodos();

            GridView1.DataSource = _dicInsumo.Values.ToList();
            GridView1.DataBind();
        }


        private void setearGrillaSiEstaVacia()
        {

            if (GridView1.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdInsumoStr");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("NombreFab");
                dt.Columns.Add("CostoStr");
                dt.Columns.Add("StockStr");
                dt.Columns.Add("FechaActStr");
                


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
                string id = (row.Cells[1].Text);

                BussinesObjects.Insumo i = InsumoDAO.Instancia.obtenerPorId(id.Trim()) ;

                if (i != null &&  i.Idinsumo!=0)
                {
                    Session["insumo"] = i;
                    Response.Redirect("Insumo.aspx");
                }
            }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            Session["insumo"] = null;
            Response.Redirect("Insumo.aspx");
        }

        protected void BtnBorrar_Click(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            if (row != null)
            {
                string id = (row.Cells[1].Text);

                BussinesObjects.Insumo i = InsumoDAO.Instancia.obtenerPorId(id.Trim());

                if (i != null &&  i.Idinsumo != 0)
                {
                    i.Borrado = true;
                    InsumoDAO.Instancia.actualizarInsumo(i);
                    Response.Redirect("InsumoVer.aspx");
                }
            }
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
    }
}
