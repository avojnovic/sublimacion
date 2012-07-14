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

using sublimacion.DataAccessObjects.DataAccessObjects;
using System.Collections.Generic;
using sublimacion.BussinesObjects.BussinesObjects;
using sublimacion.BussinesObjects;

namespace sublimacion
{
    public partial class PedidoABM : System.Web.UI.Page
    {

        ModosEdicionEnum _modoApertura = new ModosEdicionEnum();
        Pedido _pedido;

        Dictionary<long, Estado> _listaEstados = new Dictionary<long, Estado>();
        Dictionary<long, Cliente> _listaClientes = new Dictionary<long, Cliente>();
        Dictionary<long, Producto> _listaProductos = new Dictionary<long, Producto>();
        Dictionary<Producto, int> _listaProductosAgregados = new Dictionary<Producto, int>();
        Usuario user;

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];

            if (id != null)
            {
                _pedido = PedidoDAO.obtenerPorId(id);
            }


            _listaEstados = TipoEstadoDAO.obtenerEstados();
            _listaClientes = ClienteDAO.obtenerClienteTodos();
            _listaProductos = ProductoDAO.obtenerProductoTodos();



            LblComentario.Text = "";
            user = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {

                cargarCombos();
                cargarPedido();

                Session["Productos"] = _listaProductos;

                cargarGrilla();
                setearGrillaSiEstaVacia();
            }


            if (_pedido != null)
            {
                _modoApertura = ModosEdicionEnum.Modificar;
                BtnBorrar.Visible = true;
            }
            else
            {
                _modoApertura = ModosEdicionEnum.Nuevo;
                BtnBorrar.Visible = false;
                Usuario u = (Usuario)Session["usuario"];
                TxtUsuario.Text = u.NombreCompleto;
                TxtFecha.Text = DateTime.Now.ToShortDateString();
            }


            if (user.Perfil == Usuario.PerfilesEnum.JefeProduccion)
            {

                TxtComentario.ReadOnly = true;
                TxtFecha.ReadOnly = true;
                TxtUbicacion.ReadOnly = true;
                TxtUsuario.ReadOnly = true;
                CmbCliente.Enabled = false;
                CmbEstado.Enabled = false;

                GridViewProductos.Enabled = false;

            }

        }



        private void cargarCombos()
        {



            CmbEstado.DataSource = _listaEstados.Values.ToList();
            CmbEstado.DataTextField = "Descripcion";
            CmbEstado.DataValueField = "Id";
            CmbEstado.DataBind();



            CmbCliente.DataSource = _listaClientes.Values.ToList();
            CmbCliente.DataTextField = "NombreCompleto";
            CmbCliente.DataValueField = "IdCliente";
            CmbCliente.DataBind();
        }

        private void cargarPedido()
        {
            if (_pedido != null)
            {
                TxtComentario.Text = _pedido.Comentario;
                TxtFecha.Text = _pedido.Fecha.ToShortDateString();
                TxtPrioridad.Text = _pedido.Prioridad.ToString();
                TxtUbicacion.Text = _pedido.Ubicacion;
                TxtUsuario.Text = _pedido.UserNombre;

                DateTime? t = DateTime.MinValue;
                Estado est = new Estado();

                foreach (EstadosPedido e in _pedido.EstadosPedido.Values.ToList())
                {
                    if (e.Fecha_inicio > t)
                    {
                        t = e.Fecha_inicio;
                        est = e.Estado;
                    }
                }

                CmbEstado.SelectedValue = est.Id.ToString();

                CmbCliente.SelectedValue = _pedido.Cliente.IdCliente.ToString();


                foreach (Producto p in _pedido.LineaPedido.Keys.ToList())
                {
                    if (_listaProductos.ContainsKey(p.Idproducto))
                    {
                        _listaProductos[p.Idproducto] = p;

                    }

                }

            }
            else
            {
                _modoApertura = ModosEdicionEnum.Nuevo;
            }
        }

        protected void BtnBorrar_Click(object sender, EventArgs e)
        {
            setearObjeto();
            _pedido.Borrado = true;
            PedidoDAO.actualizarPedido(_pedido);

            if ((user.Perfil == Usuario.PerfilesEnum.JefeProduccion))
            {
                Response.Redirect("LogisticaProduccion.aspx");
            }
            else
            {
                Response.Redirect("PedidoVer.aspx");
            }

        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {

            if (_modoApertura == ModosEdicionEnum.Nuevo)
            {
                setearObjeto();
                PedidoDAO.insertarPedido(_pedido);
            }
            else
            {
                if (_modoApertura == ModosEdicionEnum.Modificar)
                {
                    setearObjeto();
                    PedidoDAO.actualizarPedido(_pedido);

                }
            }



            if ((user.Perfil == Usuario.PerfilesEnum.JefeProduccion))
            {
                Response.Redirect("LogisticaProduccion.aspx");
            }
            else
            {
                Response.Redirect("PedidoVer.aspx");
            }


        }

        private void setearObjeto()
        {

            if (_pedido == null)
                _pedido = new Pedido();

            _pedido.Cliente = _listaClientes[long.Parse(this.CmbCliente.SelectedValue)];

            _pedido.Borrado = false;
            _pedido.Comentario = TxtComentario.Text.Trim();

            if (TxtPrioridad.Text.Trim() == "")
                _pedido.Prioridad = 0;
            else
                _pedido.Prioridad = int.Parse(TxtPrioridad.Text.Trim());

            _pedido.Ubicacion = TxtUbicacion.Text.Trim();






            if (_modoApertura == ModosEdicionEnum.Nuevo)
            {
                _pedido.Usuario = (Usuario)Session["usuario"];
                _pedido.Fecha = DateTime.Now;
                _pedido.EstadosPedido = new Dictionary<long, EstadosPedido>();
                _pedido.OrdenDeTrabajo = new OrdenDeTrabajo();
                _pedido.PlanDeProduccion = new PlanDeProduccion();

            }






            if (!_pedido.EstadosPedido.ContainsKey(long.Parse(this.CmbEstado.SelectedValue)))
            {
                EstadosPedido est = new EstadosPedido();
                est.Fecha_inicio = DateTime.Now;
                est.Fecha_fin = null;
                est.Estado = _listaEstados[long.Parse(this.CmbEstado.SelectedValue)];
                _pedido.EstadosPedido.Add(est.Estado.Id, est);


                foreach (EstadosPedido e in _pedido.EstadosPedido.Values.ToList())
                {

                    if (e.Estado.Id != est.Estado.Id && e.Fecha_fin == null)
                    {
                        e.Fecha_fin = DateTime.Now;
                    }
                }

            }

            _pedido.LineaPedido = new Dictionary<Producto, int>();



            Dictionary<long, Producto> dt = (Dictionary<long, Producto>)Session["Productos"];
            foreach (Producto p in dt.Values.ToList())
            {
                if (p.Cantidad > 0)
                    _pedido.LineaPedido.Add(p, p.Cantidad);
            }






        }




        protected void BtnSalir_Click(object sender, EventArgs e)
        {

            if ((user.Perfil == Usuario.PerfilesEnum.JefeProduccion))
            {
                Response.Redirect("LogisticaProduccion.aspx");
            }
            else
            {
                Response.Redirect("PedidoVer.aspx");
            }
        }



        protected void GridViewProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewProductos.PageIndex = e.NewPageIndex;
            GridViewProductos.DataBind();
        }


        protected void GridViewProductos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewProductos.EditIndex = e.NewEditIndex;
            cargarGrilla();
        }

        protected void GridViewProductos_OnRowCancelingEdit(Object sender, GridViewCancelEditEventArgs e)
        {
            GridViewProductos.EditIndex = -1;
            cargarGrilla();
        }


        protected void GridViewProductos_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int index = GridViewProductos.EditIndex;
            GridViewRow row = GridViewProductos.Rows[index];

            Label id = (Label)row.FindControl("LblIdproducto");
            TextBox cantidad = (TextBox)row.FindControl("TxtCantidad");
            Dictionary<long, Producto> dt = (Dictionary<long, Producto>)Session["Productos"];

            dt[long.Parse(id.Text)].Cantidad = int.Parse(cantidad.Text);
            Session["Productos"] = dt;

            GridViewProductos.EditIndex = -1;
            cargarGrilla();
        }


        protected void DDLCatalogo_SelectedIndexChanged(object sender, EventArgs e)
        {
           
            DropDownList ddl = sender as DropDownList;

            CargarPlantillas(ddl);

        }

        private void CargarPlantillas(DropDownList ddl)
        {
            foreach (GridViewRow row in GridViewProductos.Rows)
            {

                Control ctrl = row.FindControl("DDLCatalogo") as DropDownList;
                if (ctrl != null)
                {
                    DropDownList ddlCata = (DropDownList)ctrl;

                    if (ddl.ClientID == ddlCata.ClientID)
                    {

                        DropDownList ddlPlant = row.FindControl("DDLPlantilla") as DropDownList;

                        Dictionary<long, Plantilla> _pl = PlantillaDAO.obtenerPlantillaPorCatalogo(ddlCata.SelectedValue);

                        if (_pl.Values.Count > 0)
                        {

                            ddlPlant.DataSource = _pl.Values.ToList();
                            ddlPlant.DataBind();
                        }
                        break;
                    }
                }
            }
        }

        protected void GridViewProductos_RowDataBound(object sender, GridViewRowEventArgs e)
        {

            Label id = (Label)e.Row.FindControl("LblIdproducto");
            DropDownList ddl = (DropDownList)e.Row.FindControl("DDLCatalogo");
            if (ddl != null && id != null && id.Text.Trim() != "")
            {
                Dictionary<long, Catalogo> _ct = CatalogoDAO.obtenerCatalogoPorIdProducto(id.Text);

                if (_ct.Values.Count > 0)
                {

                    ddl.DataSource = _ct.Values.ToList();
                    ddl.DataBind();

                    CargarPlantillas(ddl);
                }
            }

        }


        private void cargarGrilla()
        {
            Dictionary<long, Producto> dt = (Dictionary<long, Producto>)Session["Productos"];
            GridViewProductos.DataSource = dt.Values.ToList();
            GridViewProductos.DataBind();
        }
        private void setearGrillaSiEstaVacia()
        {

            if (GridViewProductos.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Idproducto");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Cantidad");


                dt.Rows.Add(new object[] { "", "", "" });

                GridViewProductos.DataSource = dt;
                GridViewProductos.DataBind();
            }

        }


    }
}
