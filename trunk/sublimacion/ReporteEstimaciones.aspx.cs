using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Globalization;
using sublimacion.BussinesObjects.BussinesObjects;
using sublimacion.DataAccessObjects.DataAccessObjects;
using System.Data;

namespace sublimacion
{
    public partial class ReporteEstimaciones : System.Web.UI.Page
    {
        private List<Estimacion> _Estimaciones;
        


        protected void Page_Load(object sender, EventArgs e)
        {

            if (!IsPostBack)
            {
   
                setearGrillaSiEstaVacia();
            }

        }

        private void cargarGrilla()
        {

            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime fechaDesde = DateTime.MinValue;
            DateTime fechaHasta = DateTime.MinValue;


            try
            {
                fechaDesde = DateTime.ParseExact(TxtFechaDesde.Text, "dd/MM/yyyy", provider);
            }
            catch (Exception)
            {

                LbComentario.Text = "Formato de Fecha Desde invalido";
            }

            try
            {
                fechaHasta = DateTime.ParseExact(TxtFechaHasta.Text, "dd/MM/yyyy", provider);
            }
            catch (Exception)
            {

                LbComentario.Text = "Formato de Fecha Hasta invalido";
            }

            if (fechaDesde > fechaHasta)
                LbComentario.Text = "Periodo de Fecha Incorrecto";
           


            if (LbComentario.Text == "")
            {
                _Estimaciones = EstimacionDAO.obtenerEstimacionesTodas(fechaDesde, fechaHasta);

               

                GridViewReporte.DataSource = _Estimaciones;
                GridViewReporte.DataBind();
            }
            setearGrillaSiEstaVacia();
        }

       

        private void setearGrillaSiEstaVacia()
        {

            if (GridViewReporte.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdPlan");
                dt.Columns.Add("FechaInicioPlan");
                dt.Columns.Add("FechaFinPlan");
                dt.Columns.Add("IdOrden");
                dt.Columns.Add("FechaInicioOrden");
                dt.Columns.Add("FechaFinOrden");
                dt.Columns.Add("CantPedidos");
                dt.Columns.Add("EstimadoMostrar");


                dt.Rows.Add(new object[] { "", "", "", "", "", "", "", "" });

                GridViewReporte.DataSource = dt;
                GridViewReporte.DataBind();
            }

        }





        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            GridViewReporte.DataSource = null;
            GridViewReporte.DataBind();

            LbComentario.Text = "";
            cargarGrilla();
        }



        protected void Btnlimpiar_Click(object sender, EventArgs e)
        {
            TxtFechaDesde.Text = "";
            TxtFechaHasta.Text = "";
          
            LbComentario.Text = "";

            GridViewReporte.DataSource = null;
            GridViewReporte.DataBind();

            setearGrillaSiEstaVacia();

        }




        protected void GridViewReporte_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewReporte.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }

       
    }
}