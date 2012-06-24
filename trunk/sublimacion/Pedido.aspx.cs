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
using sublimacion.BussinesObjects.BussinesObjects;
using sublimacion.BussinesObjects;
using sublimacion.DataAccessObjects.DataAccessObjects;
using System.Collections.Generic;

namespace sublimacion
{
    public partial class Pedido : System.Web.UI.Page
    {

        ModosEdicionEnum _modoApertura = new ModosEdicionEnum();
        BussinesObjects.BussinesObjects.Pedido _pedido;
        Dictionary<long, Estado> _listaEstados = new Dictionary<long, Estado>();
        Dictionary<long, BussinesObjects.BussinesObjects.Cliente> _listaClientes = new Dictionary<long, BussinesObjects.BussinesObjects.Cliente>();
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
            _listaProductos = ProductoDAO.obtenerTodos();



            LblComentario.Text = "";
            user = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {

                cargarCombos();
                cargarPedido();

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

            }


            if (user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.JefeProduccion)
            {
                TxtCantidad.ReadOnly = true;
                TxtComentario.ReadOnly = true;
                TxtFecha.ReadOnly = true;
                TxtUbicacion.ReadOnly = true;
                TxtUsuario.ReadOnly = true;
                CmbCliente.Enabled = false;
                CmbEstado.Enabled = false;
                ListBoxProductos.Enabled = false;
                ListBoxProductosAgregados.Enabled = false;
                BtnAgregarProducto.Enabled = false;
                LblProductosDisp.Enabled = false;
            }
        }

        private void cargarCombos()
        {

            ListBoxProductos.DataSource = _listaProductos.Values.ToList();
            ListBoxProductos.DataTextField = "Nombre";
            ListBoxProductos.DataValueField = "Idproducto";
            ListBoxProductos.DataBind();

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

                CmbEstado.SelectedValue =est.Id.ToString();

                CmbCliente.SelectedValue = _pedido.Cliente.IdCliente.ToString();



                ListBoxProductosAgregados.DataValueField = "Idproducto";
                ListBoxProductosAgregados.DataTextField = "ParaPedido";
                ListBoxProductosAgregados.DataSource = _pedido.LineaPedido.Keys.ToList();
                ListBoxProductosAgregados.DataBind();

                if (ListBoxProductosAgregados.Items.Count > 0)
                    ListBoxProductosAgregados.SelectedIndex = 0;

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

            if ((user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.JefeProduccion))
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



            if ((user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.JefeProduccion))
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
                _pedido = new BussinesObjects.BussinesObjects.Pedido();

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

             _pedido.LineaPedido = new Dictionary<Producto,int>();




             _listaProductosAgregados = new Dictionary<Producto, int>();

             for (int i = 0; i < ListBoxProductosAgregados.Items.Count; i++)
             {
                 string text = ListBoxProductosAgregados.Items[i].Text;
                 int cantidad = int.Parse(text.Substring(text.IndexOf(" - ") + 3));
                 _listaProductos[long.Parse(ListBoxProductosAgregados.Items[i].Value)].Cantidad = cantidad;
                 _listaProductosAgregados.Add(_listaProductos[long.Parse(ListBoxProductosAgregados.Items[i].Value)], cantidad);

             }

            _pedido.LineaPedido=_listaProductosAgregados;



        }



        protected void BtnSalir_Click(object sender, EventArgs e)
        {

            if ((user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.JefeProduccion))
            {
                Response.Redirect("LogisticaProduccion.aspx");
            }
            else
            {
                Response.Redirect("PedidoVer.aspx");
            }
        }

        protected void BtnAgregarProducto_Click(object sender, EventArgs e)
        {

            int cant = int.Parse(TxtCantidad.Text.Trim());


            _listaProductosAgregados = new Dictionary<Producto, int>();

            for (int i = 0; i < ListBoxProductosAgregados.Items.Count; i++)
            {
                string text=ListBoxProductosAgregados.Items[i].Text;
                int cantidad = int.Parse(text.Substring(text.IndexOf(" - ")+3));
                _listaProductos[long.Parse(ListBoxProductosAgregados.Items[i].Value)].Cantidad = cantidad;
                 _listaProductosAgregados.Add( _listaProductos[long.Parse( ListBoxProductosAgregados.Items[i].Value)],cantidad);
               
            }


            bool agregar = true;
            foreach (Producto pi in _listaProductosAgregados.Keys.ToList())
            {
                if (pi.Idproducto == _listaProductos[long.Parse(ListBoxProductos.SelectedValue)].Idproducto)
                    agregar = false;
            }
            _listaProductos[long.Parse(ListBoxProductos.SelectedValue)].Cantidad = cant;

            if (agregar)
                _listaProductosAgregados.Add(_listaProductos[long.Parse(ListBoxProductos.SelectedValue)], cant);


            ListBoxProductosAgregados.DataValueField = "Idproducto";
            ListBoxProductosAgregados.DataTextField = "ParaPedido";
            ListBoxProductosAgregados.DataSource = _listaProductosAgregados.Keys.ToList();
            ListBoxProductosAgregados.DataBind();


            TxtCantidad.Text = "";

            if (ListBoxProductosAgregados.Items.Count > 0)
                ListBoxProductosAgregados.SelectedIndex = 0;
            
        }


    }
}
