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
        Cliente _cli;
        string idCliente;
        protected void Page_Load(object sender, EventArgs e)
        {
            user = (Usuario)Session["usuario"];
             
            idCliente = Request.QueryString["idCliente"];

            if (idCliente != null && idCliente != "")
            {
                _cli = ClienteDAO.obtenerClientePorId(idCliente);
                LblCliente.Text = _cli.NombreCompleto + " - DNI: " + _cli.Dni.ToString();
            }
            else
            {
                LblCliente.Text = "";
            }

            if (!IsPostBack)
            {
                cargarCombos();
            }

            cargarGrilla();

            if ((user.Perfil == Usuario.PerfilesEnum.JefeProduccion))
            {

                BtnImgNuevo.Visible = false;
            }

          
        }

        private void cargarCombos()
        {
            _listaEstados = TipoEstadoDAO.obtenerEstados();
            Estado e = new Estado();
            e.Id=-1;
            e.Descripcion="Todos";
            _listaEstados.Add(e.Id,e);

            CmbEstados.DataSource = _listaEstados.Values.ToList();
            CmbEstados.DataValueField = "Id";
            CmbEstados.DataTextField = "Descripcion";
            CmbEstados.DataBind();

            CmbEstados.SelectedValue = "-1";

        }

        private void setearGrillaSiEstaVacia()
        {
           
            if (GridView1.Rows.Count == 0)
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


                dt.Rows.Add(new object[] { "", "", "", "", "", "", "", "", "", "" });

                GridView1.Columns[GridView1.Columns.Count - 1].Visible = false;
                
                
                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

        }

        private void cargarGrilla()
        {

            Dictionary<long, Pedido> _dicTemp = new Dictionary<long, Pedido>();

            if (idCliente != null && idCliente != "")
                _dicPedidos = PedidoDAO.obtenerTodosPorCliente(idCliente);
            else
                _dicPedidos = PedidoDAO.obtenerTodos();
           
            List<Pedido> listP = _dicPedidos.Values.ToList();

            long idEstado= long.Parse(CmbEstados.SelectedValue);

            if (idEstado != -1)
            {
                var newList = (from x in listP
                              where x.EstadoId == idEstado
                              select x).ToList();

                listP = (List<Pedido>)newList;
            }


            listP.Sort(delegate(Pedido p1, Pedido p2)
            {
                return p2.Prioridad.CompareTo(p1.Prioridad);
            });


            GridView1.DataSource = listP;

            GridView1.DataBind();

            setearGrillaSiEstaVacia();

            //this.GridView1.Columns[2].ItemStyle.Width = new Unit(50);
        }
      

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            cargarGrilla();
        }

     

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {
            if (idCliente != null && idCliente != "")
            { 
                Response.Redirect("PedidoABM.aspx?idCliente=" + idCliente); 
            }
            else 
            {
                Response.Redirect("PedidoABM.aspx");
            }


        }

        protected void CmbEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarGrilla();
        }



       
    }
}
