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
using sublimacion.BussinesObjects;
using System.Collections.Generic;
using sublimacion.BussinesObjects.BussinesObjects;
using sublimacion.DataAccessObjects.DataAccessObjects;
using System.Globalization;

namespace sublimacion
{
    public partial class LogisticaProduccion : System.Web.UI.Page
    {

        Usuario user;
        private Dictionary<long, Pedido> _dicPedidos;
        private Dictionary<long, Pedido> _dicPlanificar;


        protected void Page_Load(object sender, EventArgs e)
        {
            LblComentario.Text = "";
            user = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {
                LblFecha.Text = "Fecha: " + DateTime.Now.ToString("dd/MM/yyyy");
                _dicPlanificar = new Dictionary<long, Pedido>();
                TxtFechaInicio.Text = DateTime.Now.ToString("dd/MM/yyyy");
                TxtHoraInicio.Text = DateTime.Now.ToString("HH:mm");

                Session["Pedidos"] = null;
                cargarGrillas();
               
            }

        }

         private void setearGrillaSiEstaVacia()
        {
            if (GridViewPlanif.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdPedido");
                dt.Columns.Add("PlanId");
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

                GridViewPlanif.DataSource = dt;
                GridViewPlanif.DataBind();
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

                GridViewPedidos.DataSource = dt;
                GridViewPedidos.DataBind();
            }

        }

        private void cargarGrillas()
        {
            //CARGO LOS SIN PLANIFICADOS
            _dicPedidos = PedidoDAO.obtenerTodos();
            List<Pedido> listP = _dicPedidos.Values.ToList();

            var newList = (from x in listP
                           where x.EstadoId == (long)sublimacion.BussinesObjects.BussinesObjects.EstadosPedido.EstadosPedidoEnum.DisenioAceptado
                           && (x.PlanDeProduccion == null || x.PlanDeProduccion.IdPlan == 0)
                           select x).ToList();

            listP = (List<Pedido>)newList;



            listP.Sort(delegate(Pedido p1, Pedido p2)
            {
                return p2.Prioridad.CompareTo(p1.Prioridad);
            });


            GridViewPedidos.DataSource = listP;
            GridViewPedidos.DataBind();


            //CARGO LOS PLANIFICADOS
             List<Pedido> listPlan = PedidoDAO.obtenerTodosConIdPlanProd().Values.ToList();

            var newList2 = (from x in listPlan
                           where x.EstadoId == (long)sublimacion.BussinesObjects.BussinesObjects.EstadosPedido.EstadosPedidoEnum.Producción
                           && (x.PlanDeProduccion!= null || x.PlanDeProduccion.IdPlan != 0)
                           select x).ToList();

            listPlan = (List<Pedido>)newList2;

            GridViewPlanif.DataSource = listPlan;
            GridViewPlanif.DataBind();

            setearGrillaSiEstaVacia();
        }

        public void GridViewCheckBoxGuardar()
        {

            Dictionary<long, Pedido> dp = (Dictionary<long, Pedido>)Session["Pedidos"];
            _dicPedidos = PedidoDAO.obtenerTodos();

            if (dp == null)
            {
                dp = new Dictionary<long, Pedido>();
            }

            for (int i = 0; i < GridViewPedidos.Rows.Count; i++)
            {
                GridViewRow row = GridViewPedidos.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("checkBox")).Checked;
                Label id = ((Label)row.FindControl("LblIdPedido"));

                if (id.Text != "")
                {
                    if (isChecked)
                    {

                        if (!dp.ContainsKey(long.Parse(id.Text)))
                        {
                            dp.Add(long.Parse(id.Text), _dicPedidos[long.Parse(id.Text)]);
                        }

                    }
                    else
                    {
                        if (dp.ContainsKey(long.Parse(id.Text)))
                        {
                            dp.Remove(long.Parse(id.Text));
                        }

                    }
                }
            }

            Session["Pedidos"] = dp;
        }

        public void GridViewCheckBoxSetear()
        {

            Dictionary<long, Pedido> dp = (Dictionary<long, Pedido>)Session["Pedidos"];

            if (dp != null)
            {
                for (int i = 0; i < GridViewPedidos.Rows.Count; i++)
                {
                    GridViewRow row = GridViewPedidos.Rows[i];

                    Label id = ((Label)row.FindControl("LblIdPedido"));

                    if (dp.ContainsKey(long.Parse(id.Text.Trim())))
                        ((CheckBox)row.FindControl("checkBox")).Checked = true;
                    else
                        ((CheckBox)row.FindControl("checkBox")).Checked = false;

                }
            }
        }

        protected void GridViewPlanif_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
    

        }

        protected void GridViewPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewCheckBoxGuardar();
            GridViewPedidos.PageIndex = e.NewPageIndex;
            cargarGrillas();
            GridViewCheckBoxSetear();


           
        }

        protected void BtnCalcularTiempo_Click(object sender, EventArgs e)
        {
            LblComentario.Text = "";
            GridViewCheckBoxGuardar();
            Dictionary<long, Pedido> dp = (Dictionary<long, Pedido>)Session["Pedidos"];
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime fechaInicio = DateTime.MinValue;

            if (dp.Values.Count > 0)
            {

                try
                {
                    fechaInicio = DateTime.ParseExact(TxtFechaInicio.Text + " " + TxtHoraInicio.Text, "dd/MM/yyyy HH:mm", provider);
                }
                catch (Exception)
                {
                    LblComentario.Text = "Formato de Fecha inicio invalido";
                }

                System.Nullable<Decimal> tiempoTotal = (from ord in dp.Values.ToList()
                                                        select ord.TiempoTotal).Sum();

                fechaInicio=fechaInicio.AddMinutes((double)tiempoTotal);

                TxtFechaFin.Text = fechaInicio.ToString("dd/MM/yyyy");
                TxtHoraFin.Text = fechaInicio.ToString("HH:mm");
            }
            else
            {
                TxtFechaFin.Text = "";
                TxtHoraFin.Text = "";

                LblComentario.Text = "Seleccione al menos un pedido";
            }

            
        }



        protected void BtnAgregarAPlan_Click(object sender, EventArgs e)
        {
            LblComentario.Text = "";
           
            Dictionary<long, Pedido> dp = (Dictionary<long, Pedido>)Session["Pedidos"];
            CultureInfo provider = CultureInfo.InvariantCulture;
            DateTime fechaInicio = DateTime.MinValue;
            DateTime fechaFin = DateTime.MinValue;

            if (dp!=null && dp.Values.Count > 0)
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


                if (LblComentario.Text=="")
                {

                   GenerarPlanificacion(fechaInicio,fechaFin,dp);
                }
                else
                {
                    LblComentario.Text = "Fecha de Inicio Incorrecta";
                }
            }
            else
            {
                LblComentario.Text = "Por favor calcule el Tiempo";
            }
        }



        private void GenerarPlanificacion(DateTime inicio, DateTime fin,Dictionary<long, Pedido> _pedidos)
        {
            PlanDeProduccion plan = new PlanDeProduccion();
            plan.Fecha_inicio = inicio;
            plan.Fecha_fin = fin;
            plan = PlanProdDAO.insertarPlan(plan);

            OrdenDeTrabajo orden = new OrdenDeTrabajo();
            orden.Tiempo_estimado = 0;
            orden.Fecha_comienzo = null;
            orden.Fecha_finalizacion = null;
            orden = OrdenTrabajoDAO.insertarOrdenTrabajo(orden);

            Dictionary<long, Pedido> dp = (Dictionary<long, Pedido>)Session["Pedidos"];

            foreach (Pedido p in dp.Values.ToList())
            {
                p.OrdenDeTrabajo = orden;
                p.PlanDeProduccion = plan;
                setearEstadoPedido(p);
                PedidoDAO.actualizarPedido(p);
            }
            
            Session["Pedidos"]=null;
         

            LblComentarioPlan.Text = "Plan nro:" + plan.Id + " creado";
            
            

             cargarGrillas();
        }

        private void setearEstadoPedido(Pedido _pedido)
        {
            if (!_pedido.EstadosPedido.ContainsKey((long)sublimacion.BussinesObjects.BussinesObjects.EstadosPedido.EstadosPedidoEnum.Producción))
            {
                EstadosPedido est = new EstadosPedido();
                est.Fecha_inicio = DateTime.Now;
                est.Fecha_fin = null;
                est.Estado = TipoEstadoDAO.obtenerEstadosPorId(((long)sublimacion.BussinesObjects.BussinesObjects.EstadosPedido.EstadosPedidoEnum.Producción).ToString());
                _pedido.EstadosPedido.Add(est.Estado.Id, est);


                foreach (EstadosPedido e in _pedido.EstadosPedido.Values.ToList())
                {

                    if (e.Estado.Id != est.Estado.Id && e.Fecha_fin == null)
                    {
                        e.Fecha_fin = DateTime.Now;
                    }
                }

            }
            else
            {
                _pedido.EstadosPedido[((long)sublimacion.BussinesObjects.BussinesObjects.EstadosPedido.EstadosPedidoEnum.Producción)].Fecha_fin = null;

                foreach (EstadosPedido e in _pedido.EstadosPedido.Values.ToList())
                {

                    if (e.Estado.Id != _pedido.EstadosPedido[((long)sublimacion.BussinesObjects.BussinesObjects.EstadosPedido.EstadosPedidoEnum.Producción)].Estado.Id && e.Fecha_fin == null)
                    {
                        e.Fecha_fin = DateTime.Now;
                    }
                }
            }

        }

       
        private void setearEstadoEnProduccion(Pedido pedido)
        {
            Estado c = TipoEstadoDAO.obtenerEstadosPorId(((long)sublimacion.BussinesObjects.BussinesObjects.EstadosPedido.EstadosPedidoEnum.Producción).ToString());

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





    }
}
