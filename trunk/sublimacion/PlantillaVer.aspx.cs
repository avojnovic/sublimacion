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
    public partial class PlantillaVer : System.Web.UI.Page
    {
        private Dictionary<long, Plantilla> _dicPlantilla;

        protected void Page_Load(object sender, EventArgs e)
        {

            cargarGrilla();

            setearGrillaSiEstaVacia();
        }

        private void cargarGrilla()
        {
            _dicPlantilla = PlantillaDAO.obtenerPlantillaTodos();

            GridView1.DataSource = _dicPlantilla.Values.ToList();
            GridView1.DataBind();

        }


        private void setearGrillaSiEstaVacia()
        {

            if (GridView1.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdPlantilla");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Medida_ancho");
                dt.Columns.Add("Medida_largo");
               

                dt.Rows.Add(new object[] { "", "", "", "" });

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {

            Response.Redirect("PlantillaABM.aspx");
        }

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

    }
}