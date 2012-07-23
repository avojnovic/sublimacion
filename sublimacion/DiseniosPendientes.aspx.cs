using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using sublimacion.BussinesObjects.BussinesObjects;
using System.Data;
using sublimacion.DataAccessObjects.DataAccessObjects;

namespace sublimacion
{
    public partial class DiseniosPendientes : System.Web.UI.Page
    {
        Usuario user;
        private Dictionary<long, Pedido> _dicPedidos;
        Dictionary<long, Estado> _listaEstados = new Dictionary<long, Estado>();

        protected void Page_Load(object sender, EventArgs e)
        {
            user = (Usuario)Session["usuario"];
            cargarGrilla();


        }



        private void setearGrillaSiEstaVacia()
        {

            if (GridViewPedidos.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdPedido");
                dt.Columns.Add("FechaVer");
                dt.Columns.Add("Comentario");
                dt.Columns.Add("Prioridad");
                dt.Columns.Add("Estado");
                dt.Columns.Add("Ubicacion");
                dt.Columns.Add("UserNombre");
                dt.Columns.Add("ClienteNombre");


                dt.Rows.Add(new object[] { "", "", "", "", "", "", "", "" });

                GridViewPedidos.Columns[GridViewPedidos.Columns.Count - 1].Visible = false;

                GridViewPedidos.DataSource = dt;
                GridViewPedidos.DataBind();
            }
            else
            {
                GridViewPedidos.Columns[GridViewPedidos.Columns.Count - 1].Visible = true;
            }

        }

        private void cargarGrilla()
        {

            Dictionary<long, Pedido> _dicTemp = new Dictionary<long, Pedido>();


            _dicPedidos = PedidoDAO.obtenerTodos();

            List<Pedido> listP = _dicPedidos.Values.ToList();



            var newList = (from x in listP
                           where x.EstadoId == (long)sublimacion.BussinesObjects.BussinesObjects.EstadosPedido.EstadosPedidoEnum.DisenioPendiente
                           select x).ToList();

            listP = (List<Pedido>)newList;



            listP.Sort(delegate(Pedido p1, Pedido p2)
            {
                return p2.Prioridad.CompareTo(p1.Prioridad);
            });


            GridViewPedidos.DataSource = listP;

            GridViewPedidos.DataBind();

            setearGrillaSiEstaVacia();
        }


        protected void GridViewPedidos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {


            GridViewPedidos.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }




    }
}