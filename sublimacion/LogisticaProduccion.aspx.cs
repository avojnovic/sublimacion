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

namespace sublimacion
{
    public partial class LogisticaProduccion : System.Web.UI.Page
    {

        Usuario user;
        private Dictionary<long, BussinesObjects.BussinesObjects.Pedido> _dicPedidos;
        private Dictionary<long, BussinesObjects.BussinesObjects.Pedido> _dicPlanificar;


        protected void Page_Load(object sender, EventArgs e)
        {
            LblComentario.Text = "";
            if (!IsPostBack)
            {
                _dicPlanificar = new Dictionary<long, sublimacion.BussinesObjects.BussinesObjects.Pedido>();
                Session["listaPedidos"] = null;

                TxtFechaInicio.Text = DateTime.Now.ToShortDateString() + " " + DateTime.Now.ToLongTimeString();

            }


            user = (Usuario)Session["usuario"];
            cargarGrilla();

            LblFecha.Text = "Fecha: " + DateTime.Now.ToShortDateString();
           


            setearGrillaSiEstaVacia();
        }

        private void setearGrillaSiEstaVacia()
        {
            if (GridViewPlanif.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdPedido");
                dt.Columns.Add("CostoTotalTiempo");
                dt.Columns.Add("CostoTotal");
                dt.Columns.Add("Prioridad");
                dt.Columns.Add("Estado");

                dt.Rows.Add(new object[] { "", "","","","" });

                GridViewPlanif.DataSource = dt;
                GridViewPlanif.DataBind();
            }

            if (GridView1.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdPedido");
                dt.Columns.Add("CostoTotalTiempo");
                dt.Columns.Add("CostoTotal");
                dt.Columns.Add("Prioridad");
                dt.Columns.Add("Estado");

                dt.Rows.Add(new object[] { "", "", "", "", "" });

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

        }

       

        private void cargarGrilla()
        {

            Dictionary<long, BussinesObjects.BussinesObjects.Pedido> _dicTemp = new Dictionary<long, sublimacion.BussinesObjects.BussinesObjects.Pedido>();
            _dicTemp = PedidoDAO.Instancia.obtenerTodos();
            _dicPedidos = new Dictionary<long, sublimacion.BussinesObjects.BussinesObjects.Pedido>();

            if ((user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.JefeProduccion))
            {

                foreach (BussinesObjects.BussinesObjects.Pedido p in _dicTemp.Values.ToList())
                {
                    if (p.EstadosPedido.ContainsKey(3))
                    {
                        if (p.EstadosPedido[3].Fecha_fin == null)
                        {
                            if (!_dicPedidos.ContainsKey(p.IdPedido))
                                _dicPedidos.Add(p.IdPedido, p);
                        }
                    }
                }


            }
            


            List<BussinesObjects.BussinesObjects.Pedido> listP = _dicPedidos.Values.ToList();


            listP.Sort(delegate(BussinesObjects.BussinesObjects.Pedido p1, BussinesObjects.BussinesObjects.Pedido p2)
            {
                return p2.Prioridad.CompareTo(p1.Prioridad);
            });


            GridView1.DataSource = listP;

            GridView1.DataBind();
        }


   

       

        protected void AgregarAPlan_Click(object sender, EventArgs e)
        {
            LblComentario.Text = "";
            DateTime fechaInicio = DateTime.Now;



            if (Session["listaPedidos"] == null)
            {
                _dicPlanificar = new Dictionary<long, sublimacion.BussinesObjects.BussinesObjects.Pedido>();
                Session["listaPedidos"] = _dicPlanificar;
            }
            else
            {
                _dicPlanificar = (Dictionary<long, sublimacion.BussinesObjects.BussinesObjects.Pedido>)Session["listaPedidos"];
            }


            if (DateTime.TryParse(TxtFechaInicio.Text.Trim(), out fechaInicio))
            {

                GridViewRow row = GridView1.SelectedRow;
                if (row != null)
                {
                    string id = (row.Cells[1].Text);

                    BussinesObjects.BussinesObjects.Pedido i = PedidoDAO.Instancia.obtenerPorId(id.Trim());

                    if (i != null && i.IdPedido != 0)
                    {
                        float tiempo= agregarPedidoAPlanificado(i);
                        float t = tiempo - (int)tiempo;
                        
                        float minutos = 0;
                        minutos = t * 60;

                        TimeSpan ts = new TimeSpan((int)tiempo, (int)minutos, 0);

                        fechaInicio=fechaInicio.Add(ts);
                       TxtFechaFin.Text=fechaInicio.ToShortDateString()+" " +fechaInicio.ToLongTimeString();

                       setearGrillaPlanificados();

                        Session["listaPedidos"] = _dicPlanificar;
                    }
                }
            }
            else
            {
                LblComentario.Text = "Fecha de Inicio Incorrecta";
            }
        }

        private void setearGrillaPlanificados()
        {

            List<BussinesObjects.BussinesObjects.Pedido> listP = _dicPlanificar.Values.ToList();


            listP.Sort(delegate(BussinesObjects.BussinesObjects.Pedido p1, BussinesObjects.BussinesObjects.Pedido p2)
            {
                return p2.Prioridad.CompareTo(p1.Prioridad);
            });


            GridViewPlanif.DataSource = listP;

            GridViewPlanif.DataBind();


        }

        private float agregarPedidoAPlanificado(sublimacion.BussinesObjects.BussinesObjects.Pedido i)
        {
            float tiempoTotal = 0;
            if (!_dicPlanificar.ContainsKey(i.IdPedido))
            {
                _dicPlanificar.Add(i.IdPedido, i);

               
                foreach (BussinesObjects.BussinesObjects.Pedido ped in _dicPlanificar.Values.ToList())
                {
                    try 
	                    {
                            tiempoTotal += float.Parse(ped.CostoTotalTiempo);
	                    }
	                    catch (Exception)
	                    {

                            tiempoTotal += 0;
	                    }
                   
                }

            
            }
            return tiempoTotal;

        }

        protected void BtnGuardarLogistica_Click(object sender, EventArgs e)
        {

            if (Session["listaPedidos"] == null)
            {
                _dicPlanificar = new Dictionary<long, sublimacion.BussinesObjects.BussinesObjects.Pedido>();
                Session["listaPedidos"] = _dicPlanificar;
            }
            else
            {
                _dicPlanificar = (Dictionary<long, sublimacion.BussinesObjects.BussinesObjects.Pedido>)Session["listaPedidos"];
            }
            DateTime fechaInicio = DateTime.MinValue;
            DateTime fechaFin = DateTime.MinValue;
            if (DateTime.TryParse(TxtFechaInicio.Text.Trim(), out fechaInicio))
            {
                if (DateTime.TryParse(TxtFechaFin.Text.Trim(), out fechaFin))
                {
                    PlanDeProduccion plan=new PlanDeProduccion();
                    plan.Borrado=false;
                    plan.Fecha_inicio=fechaInicio;
                    plan.Fecha_fin=fechaFin;

                    PlanProdDAO.Instancia.insertarPlan(plan);
                    if (plan.IdPlan != 0)
                    {
                        foreach (BussinesObjects.BussinesObjects.Pedido pedido in _dicPlanificar.Values.ToList())
                        {
                            setearEstadoEnProduccion(pedido);
                            pedido.PlanDeProduccion = plan;
                            PedidoDAO.Instancia.actualizarPedido(pedido);

                        }
                        Response.Redirect("LogisticaProduccion.aspx");
                    }
                }
                else
                {
                    LblComentario.Text = "Fecha de Fin Incorrecta";
                }
            }
            else
            {
                LblComentario.Text = "Fecha de Inicio Incorrecta";
            }
        }

        private void setearEstadoEnProduccion(sublimacion.BussinesObjects.BussinesObjects.Pedido pedido)
        {
          Estado c= EstadoDAO.Instancia.obtenerEstadosPorId("6");

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

        protected void BtnVerPedido_Click(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            if (row != null)
            {
                string id = (row.Cells[1].Text);

                BussinesObjects.BussinesObjects.Pedido i = PedidoDAO.Instancia.obtenerPorId(id.Trim());

                if (i != null && i.IdPedido != 0)
                {
                    Session["pedido"] = i;
                    Response.Redirect("Pedido.aspx");
                }
            }
        }

        protected void BtnLimpiar_Click(object sender, EventArgs e)
        {
            Session["listaPedidos"] = null;
            Response.Redirect("LogisticaProduccion.aspx");
        }

        protected void GridViewPlanif_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPlanif.PageIndex = e.NewPageIndex;
            GridViewPlanif.DataBind();
        }
        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
    }
}
