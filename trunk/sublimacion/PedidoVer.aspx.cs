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
using sublimacion.BussinesObjects.BussinesObjects;
using sublimacion.DataAccessObjects.DataAccessObjects;
using sublimacion.BussinesObjects;

namespace sublimacion
{
    public partial class PedidoVer : System.Web.UI.Page
    {
        Usuario user;
        private Dictionary<long, Pedido> _dicPedidos;
        Dictionary<long, Estado> _listaEstados = new Dictionary<long, Estado>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (Usuario)Session["usuario"];
            cargarGrilla();

            if (!IsPostBack)
            {
                cargarCombos();
            }

            if ((user.Perfil == Usuario.PerfilesEnum.JefeProduccion))
            {

                BtnImgNuevo.Visible = false;
            }

            setearGrillaSiEstaVacia();
        }

        private void cargarCombos()
        {
            _listaEstados = TipoEstadoDAO.obtenerEstados();
            CmbEstados.DataSource = _listaEstados.Values.ToList();
            CmbEstados.DataBind();
        }

        private void setearGrillaSiEstaVacia()
        {
           
            if (GridView1.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdPedido");
                dt.Columns.Add("Fecha");
                dt.Columns.Add("Comentario");
                dt.Columns.Add("Prioridad");
                dt.Columns.Add("Estado");
                dt.Columns.Add("Ubicacion");
                dt.Columns.Add("UserNombre");
                dt.Columns.Add("ClienteNombre");


                dt.Rows.Add(new object[] { "", "", "", "", "","","","" });

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

        }

        private void cargarGrilla()
        {

            Dictionary<long, Pedido> _dicTemp = new Dictionary<long, Pedido>();
            _dicPedidos = PedidoDAO.obtenerTodos();
           
            List<Pedido> listP = _dicPedidos.Values.ToList();


            listP.Sort(delegate(Pedido p1, Pedido p2)
            {
                return p2.Prioridad.CompareTo(p1.Prioridad);
            });


            GridView1.DataSource = listP;

            GridView1.DataBind();
        }
      

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }

     

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {

            Response.Redirect("PedidoABM.aspx");
        }

       
    }
}
