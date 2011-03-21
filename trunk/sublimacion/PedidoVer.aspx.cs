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
        private Dictionary<long,BussinesObjects.BussinesObjects.Pedido> _dicPedidos;
        Dictionary<long, Estado> _listaEstados = new Dictionary<long, Estado>();
        
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (Usuario)Session["usuario"];
            cargarGrilla();

            if (!IsPostBack)
            {
                cargarCombos();
            }

            if ((user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.JefeProduccion))
            {
                BtnBorrar.Visible = false;
                BtnNuevo.Visible = false;
            }

            setearGrillaSiEstaVacia();
        }

        private void cargarCombos()
        {
            _listaEstados = EstadoDAO.Instancia.obtenerEstados();
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

            Dictionary<long, BussinesObjects.BussinesObjects.Pedido> _dicTemp = new Dictionary<long, sublimacion.BussinesObjects.BussinesObjects.Pedido>();
            _dicPedidos = PedidoDAO.Instancia.obtenerTodos();
           
            List<BussinesObjects.BussinesObjects.Pedido> listP = _dicPedidos.Values.ToList();


            listP.Sort(delegate(BussinesObjects.BussinesObjects.Pedido p1, BussinesObjects.BussinesObjects.Pedido p2)
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

        protected void BtnEditar_Click(object sender, EventArgs e)
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

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            Session["pedido"] = null;
            Response.Redirect("Pedido.aspx");
        }

        protected void BtnBorrar_Click(object sender, EventArgs e)
        {
            GridViewRow row = GridView1.SelectedRow;
            if (row != null)
            {
                string id = (row.Cells[1].Text);

                BussinesObjects.BussinesObjects.Pedido i = PedidoDAO.Instancia.obtenerPorId(id.Trim());

                if (i != null && i.IdPedido != 0)
                {
                    i.Borrado = true;
                    PedidoDAO.Instancia.actualizarPedido(i);
                    Response.Redirect("PedidoVer.aspx");
                }
            }
        }
    }
}
