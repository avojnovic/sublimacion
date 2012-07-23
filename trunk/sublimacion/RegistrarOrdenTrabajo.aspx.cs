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
using sublimacion.BussinesObjects;
using sublimacion.DataAccessObjects.DataAccessObjects;
using sublimacion.BussinesObjects.BussinesObjects;
using System.Globalization;

namespace sublimacion
{
    public partial class RegistrarOrdenTrabajo : System.Web.UI.Page
    {
        Usuario user;
        private Dictionary<long, Pedido> _dicPedidos;

        protected void Page_Load(object sender, EventArgs e)
        {
            user = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {
                Session["Pedidos"] = null;
                _dicPedidos = new Dictionary<long, Pedido>();

                cargarGrilla();
               
            }
        }

        private void cargarGrilla()
        {

            Dictionary<long, PlanDeProduccion> _listaPlan = new Dictionary<long, PlanDeProduccion>();


            _listaPlan = PlanProdDAO.obtenerPendientes();

            List<PlanDeProduccion> listP = _listaPlan.Values.ToList();
            listP.Sort(delegate(PlanDeProduccion p1, PlanDeProduccion p2)
            {
                return p1.Fecha_inicio.CompareTo(p2.Fecha_inicio);
            });


            GridViewPlanif.DataSource = listP;
            GridViewPlanif.DataBind();

            setearGrillaSiEstaVacia();
        }


        private void setearGrillaSiEstaVacia()
        {
            if (GridViewPlanif.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Id");
                dt.Columns.Add("Fecha_inicio_str");
                dt.Columns.Add("Fecha_fin_str");


                dt.Rows.Add(new object[] { "", "", "" });

                GridViewPlanif.Columns[GridViewPlanif.Columns.Count - 1].Visible = false;


                GridViewPlanif.DataSource = dt;
                GridViewPlanif.DataBind();
            }
            else
            {
                GridViewPlanif.Columns[GridViewPlanif.Columns.Count - 1].Visible = true;
            }

            if (GridViewPedidos.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdPedido");
                dt.Columns.Add("FechaVer");
                dt.Columns.Add("Comentario");
                dt.Columns.Add("Prioridad");
                dt.Columns.Add("Estado");
                dt.Columns.Add("ClienteNombre");
                dt.Columns.Add("Ubicacion");
                dt.Columns.Add("UserNombre");
                dt.Columns.Add("CostoTotalTiempo");
                dt.Columns.Add("PrecioTotal");
                dt.Columns.Add("CostoTotal");

                dt.Rows.Add(new object[] { "", "", "", "", "", "", "", "", "", "", "" });

                GridViewPedidos.Columns[GridViewPedidos.Columns.Count - 1].Visible = false;

                GridViewPedidos.DataSource = dt;
                GridViewPedidos.DataBind();

                Session["Pedidos"] = null;
            }
            else
            {
                GridViewPedidos.Columns[GridViewPedidos.Columns.Count - 1].Visible = true;
            }




        }

        protected void GridViewPlanif_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            LblTitulo.Text = "";

            if (e.CommandName == "Seleccionar")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewPlanif.Rows[index];


                Label Id = row.FindControl("LblId") as Label;
                Label Inicio = row.FindControl("LblFechaInicio") as Label;
                Label Fin = row.FindControl("LblFechaFin") as Label;

                if (Id.Text.Trim() != "")
                {
                    Dictionary<long, Pedido> _pedidos = PedidoDAO.obtenerTodosPlanTrabajo(long.Parse(Id.Text.Trim()));
                    string txt = "Pedidos seleccionados del Plan de Producción Nro:{0} Fecha de Inicio:{1} Fecha de Fin:{2}";
                    txt = string.Format(txt, Id.Text, Inicio.Text, Fin.Text);
                    LblTitulo.Text = txt;

                    GridViewPedidos.DataSource = _pedidos.Values.ToList();
                    GridViewPedidos.DataBind();

                    GridViewPedidos.Columns[GridViewPedidos.Columns.Count - 1].Visible = true;

                    Session["Pedidos"] = _pedidos;
                }
            }
        }

        protected void RegistrarTiempoReal_Click(object sender, EventArgs e)
        {
            LblComentario.Text = "";

            Dictionary<long, Pedido> dp = (Dictionary<long, Pedido>)Session["Pedidos"];
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime fechaInicio = DateTime.MinValue;
            DateTime fechaFin = DateTime.MinValue;

            if (dp != null && dp.Values.Count > 0)
            {

                try
                {
                    fechaInicio = DateTime.ParseExact(TxtFechaInicio.Text + " " + TxtHoraInicio.Text, "dd/MM/yyyy HH:mm", provider);
                }
                catch (Exception)
                {
                    LblComentario.Text = "Formato de Fecha inicio invalido";
                }
                try
                {
                    fechaFin = DateTime.ParseExact(TxtFechaFin.Text + " " + TxtHoraFin.Text, "dd/MM/yyyy HH:mm", provider);
                }
                catch (Exception)
                {
                    LblComentario.Text = "Formato de Fecha fin invalido";
                }

                if (fechaFin<fechaInicio)
                {
                    LblComentario.Text = "Fecha de Inicio debe ser menor a la fecha de finalizacion";
                }




                if (LblComentario.Text == "")
                {

                    RegistrarOrden(fechaInicio, fechaFin, dp);
                }
                else
                {
                    LblComentario.Text = "Fecha de Inicio Incorrecta";
                }
            }
            else
            {
                LblComentario.Text = "Por seleccione un Plan con pedidos";
            }
        }



        private void RegistrarOrden(DateTime fechaInicio, DateTime fechaFin, Dictionary<long, Pedido> dp)
        {
            OrdenDeTrabajo o = new OrdenDeTrabajo();


            foreach (Pedido p in dp.Values.ToList())
            {
                o.Idorden = p.OrdenDeTrabajo.Idorden;
                o.Fecha_comienzo = fechaInicio;
                o.Fecha_finalizacion = fechaFin;
                OrdenTrabajoDAO.actualizarOrdenTrabajo(o);


                p.Ubicacion = TxtUbicacion.Text;
                setearEstadoTerminado(p);
                PedidoDAO.actualizarPedido(p);

            }

            TxtFechaFin.Text = "";
            TxtFechaInicio.Text = "";
            TxtHoraFin.Text = "";
            TxtHoraInicio.Text = "";


            LblComentario.Text = "Se registraron correctamente las fechas, los pedidos fueron Terminados";
            
            cargarGrilla();
            GridViewPedidos.DataSource = null;
            GridViewPedidos.DataBind();
            LblTitulo.Text = "";
            setearGrillaSiEstaVacia();
            Session["Pedidos"] = null;


        }

        private void setearEstadoTerminado(Pedido pedido)
        {
            Estado c = TipoEstadoDAO.obtenerEstadosPorId(((long)sublimacion.BussinesObjects.BussinesObjects.EstadosPedido.EstadosPedidoEnum.Terminado).ToString());

            if (!pedido.EstadosPedido.ContainsKey(c.Id))
            {
                EstadosPedido est = new EstadosPedido();
                est.Fecha_inicio = DateTime.Now;
                est.Fecha_fin = null;
                est.Estado = c;
                pedido.EstadosPedido.Add(c.Id, est);


                foreach (EstadosPedido e in pedido.EstadosPedido.Values.ToList())
                {

                    if (e.Estado.Id != c.Id && e.Fecha_fin == null)
                    {
                        e.Fecha_fin = DateTime.Now;
                    }
                }

            }
        }

        protected void GridViewPlanif_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPlanif.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }

        protected void GridViewPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            Dictionary<long, Pedido> _pedidos = (Dictionary<long, Pedido>)Session["Pedidos"];
            GridViewPedidos.DataSource = _pedidos.Values.ToList();
            GridViewPedidos.DataBind();
            GridViewPedidos.PageIndex = e.NewPageIndex;

        }
    }
}
