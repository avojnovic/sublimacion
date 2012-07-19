using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using sublimacion.BussinesObjects.BussinesObjects;
using sublimacion.DataAccessObjects.DataAccessObjects;
using System.Data;
using System.Globalization;

namespace sublimacion
{
    public partial class ReporteRanking : System.Web.UI.Page
    {

        private List<Pedido> _Pedidos;
        private Dictionary<long, Estado> _listaEstados;

        protected void Page_Load(object sender, EventArgs e)
        {
                                
            if (!IsPostBack)
            {
                cargarCombo();
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

            if (LbComentario.Text == "")
            {
                _Pedidos = PedidoDAO.obtenerTodosFecha(fechaDesde, fechaHasta).Values.ToList(); 

                Dictionary<long,int> _producto= new Dictionary<long,int>(); 
             

                long idEstado = long.Parse(CmbEstados.SelectedValue);

                if (idEstado != -1)
                {
                    var newList = (from x in _Pedidos
                                   where x.EstadoId == idEstado
                                   select x).ToList();

                    _Pedidos = (List<Pedido>)newList;
                }

                foreach (Pedido p in _Pedidos)
                {
                    foreach (LineaPedido lp in p.LineaPedido )
                    {
                        if(_producto.ContainsKey(lp.Idproducto))
                        {
                        _producto[lp.Idproducto]+= lp.Cantidad;
                        }
                        else
                        {
                        _producto.Add(lp.Idproducto,lp.Cantidad);
                        }
                    }
                }

                long idMax;
                int cantMax=0;
                List<long> listPro = new List<long>();

                foreach (long id in _producto.Keys.ToList())
                {

                    if (_producto[id] > cantMax)
                    {
                        listPro.Clear();
                        idMax = id;
                        cantMax = _producto[id];
                        listPro.Add(id);
                    }
                    else 
                    {
                        if (_producto[id] == cantMax)
                        {
                            listPro.Add(id);
                        }
                    }
                }

                List<Producto> _ProductosMax = new List<Producto>();
                
                foreach (long id in listPro)
                {

                    Producto p = ProductoDAO.obtenerProductoPorId(id.ToString());
                    LbComentario.Text = LbComentario.Text + " Producto: " + p.Nombre + " - ";
                }
                LbComentario.Text = LbComentario.Text + "Cantidad Maxima: " + cantMax.ToString();

                GridViewReporte.DataSource = _Pedidos;
                GridViewReporte.DataBind();
            }
            setearGrillaSiEstaVacia();
        }

        private void cargarCombo()
        {
            _listaEstados = TipoEstadoDAO.obtenerEstados();
            Estado e = new Estado();
            e.Id = -1;
            e.Descripcion = "Todos";
            _listaEstados.Add(e.Id, e);

            CmbEstados.DataSource = _listaEstados.Values.ToList();
            CmbEstados.DataValueField = "Id";
            CmbEstados.DataTextField = "Descripcion";
            CmbEstados.DataBind();

            CmbEstados.SelectedValue = "-1";

        }

        private void setearGrillaSiEstaVacia()
        {

            if (GridViewReporte.Rows.Count == 0)
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

                GridViewReporte.DataSource = dt;
                GridViewReporte.DataBind();
            }

        }





        protected void BtnBuscar_Click(object sender, EventArgs e)
        {
            LbComentario.Text = "";
            cargarGrilla();
        }

       

        protected void Btnlimpiar_Click(object sender, EventArgs e)
        {
            TxtFechaDesde.Text = "";
            TxtFechaHasta.Text = "";
            CmbEstados.SelectedValue = "-1";
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

        protected void CmbEstados_SelectedIndexChanged(object sender, EventArgs e)
        {
            cargarGrilla();
        }  

    }
}