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
        Dictionary<long, Cliente> _listaClientes = new Dictionary<long, Cliente>();
        Dictionary<long, Producto> _listaProductos = new Dictionary<long, Producto>();
        Dictionary<Producto, int> _listaProductosAgregados;
        Usuario user;

        protected void Page_Load(object sender, EventArgs e)
        {
            _pedido = (BussinesObjects.BussinesObjects.Pedido)Session["pedido"];
            _listaEstados = EstadoDAO.Instancia.obtenerEstados();
            _listaClientes = ClienteDAO.Instancia.obtenerClienteTodos();
            _listaProductos = ProductoDAO.Instancia.obtenerTodos();

        

            LblComentario.Text = "";
            user = (Usuario)Session["usuario"];

            if (!IsPostBack)
            {
                _listaProductosAgregados = new Dictionary<Producto, int>();
                cargarCombos();
                cargarPedido();

                if (_pedido!=null)
                    Session["listaProd"] = _pedido.LineaPedido;

            }
            

            if (_pedido != null)
            {
                _modoApertura = ModosEdicionEnum.Modificar;
            }
            else
            {
                _modoApertura = ModosEdicionEnum.Nuevo;

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
                ListBoxProductos.Visible = false;
                ListBoxProductosAgregados.Enabled = false;
                BtnAgregarProducto.Visible = false;
                LblProductosDisp.Visible = false;
            }
        }

        private void cargarCombos()
        {

            ListBoxProductos.DataSource = _listaProductos.Values.ToList();
            ListBoxProductos.DataBind();
           
            CmbEstado.DataSource = _listaEstados.Values.ToList();
            CmbEstado.DataBind();


         
            CmbCliente.DataSource = _listaClientes.Values.ToList();
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
                    if (e.Fecha_inicio>t)
                    {
                        t = e.Fecha_inicio;
                        est = e.Estado;
                    }
                }

                CmbEstado.SelectedValue = _listaEstados[est.Id].ToString();

                CmbCliente.SelectedValue = _listaClientes[_pedido.Cliente.IdCliente].ToString();


                ListBoxProductosAgregados.DataSource = _pedido.LineaPedido.Keys.ToList();
                ListBoxProductosAgregados.DataBind();

            }
            else
            {
                _modoApertura = ModosEdicionEnum.Nuevo;
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (verificar())
            {
                if (_modoApertura == ModosEdicionEnum.Nuevo)
                {
                    setearObjeto();
                    PedidoDAO.Instancia.insertarPedido(_pedido);
                }
                else
                {
                    if (_modoApertura == ModosEdicionEnum.Modificar)
                    {
                        setearObjeto();
                        PedidoDAO.Instancia.actualizarPedido(_pedido);
                       
                    }
                }
                Session["listaProd"] = null;
                Session["pedido"] = null;

                if ((user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.JefeProduccion))
                {
                    Response.Redirect("LogisticaProduccion.aspx");
                }
                else
                {
                    Response.Redirect("PedidoVer.aspx");
                }
            }
        }

        private void setearObjeto()
        {

            if (_pedido == null)
                _pedido = new BussinesObjects.BussinesObjects.Pedido();

   

             foreach (Cliente c in _listaClientes.Values.ToList())
             {
                 if (c.ToString() == CmbCliente.SelectedItem.Text)
                 {
                     _pedido.Cliente=c;
                     break;
                 }
             }

             _pedido.Borrado = false;
             _pedido.Comentario = TxtComentario.Text.Trim();

             if (TxtPrioridad.Text.Trim()=="")
                 _pedido.Prioridad = 0;
            else
                 _pedido.Prioridad =int.Parse( TxtPrioridad.Text.Trim());

             _pedido.Ubicacion = TxtUbicacion.Text.Trim();
            
            
            
            


            if (_modoApertura == ModosEdicionEnum.Nuevo)
            {
                _pedido.Usuario = (Usuario)Session["usuario"];
                _pedido.Fecha = DateTime.Now;
                _pedido.EstadosPedido = new Dictionary<long, EstadosPedido>();
                _pedido.OrdenDeTrabajo = new OrdenDeTrabajo();
                _pedido.PlanDeProduccion = new PlanDeProduccion();

            }

           

            foreach (Estado c in _listaEstados.Values.ToList())
            {
                if (c.ToString() == CmbEstado.SelectedItem.Text)
                {
                    if (!_pedido.EstadosPedido.ContainsKey(c.Id))
                    {
                        EstadosPedido est = new EstadosPedido();
                        est.Fecha_inicio = DateTime.Now;
                        est.Fecha_fin = null;
                        est.Estado = c;
                        _pedido.EstadosPedido.Add(c.Id, est);


                        foreach (EstadosPedido e in _pedido.EstadosPedido.Values.ToList())
                        {

                            if (e.Estado.Id != c.Id && e.Fecha_fin == null)
                            {
                                e.Fecha_fin = DateTime.Now;
                            }
                        }

                    }
                    break;
                }
            }


            _pedido.LineaPedido =(Dictionary<Producto,int>) Session["listaProd"];

        }

        private bool verificar()
        {
            if (TxtComentario.Text.Trim() == "")
            {
                LblComentario.Text = "Completar campo Comentario";
                return false;
            }
            
            try
            {
                if (TxtPrioridad.Text.Trim() != "")
                {
                    int s = 0;
                    s = int.Parse(TxtPrioridad.Text.Trim());
                }
            }
            catch (Exception)
            {
                LblComentario.Text = "Corregir campo Prioridad";
                return false;
            }



            if(ListBoxProductosAgregados.Items.Count==0)
            {
                LblComentario.Text = "Agregar Productos";
                return false;
            }

            return true;
        }

        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            Session["listaProd"] = null;
            Session["pedido"] = null;
            Response.Redirect("PedidoVer.aspx");
        }

        protected void BtnAgregarProducto_Click(object sender, EventArgs e)
        {
  
            _listaProductosAgregados = (Dictionary<Producto, int>)Session["listaProd"];
             LblValidarAgregarProducto.Text="";
            if(TxtCantidad.Text!="")
            {
                bool continuar = true;
                int cant = 0;
                try
                {
                    cant =int.Parse( TxtCantidad.Text.Trim());

                }
                catch (Exception)
                {

                    LblValidarAgregarProducto.Text = "Escriba una cantidad correcta";
                    continuar = false;
                }

                if (continuar)
                {
                    Producto p = new Producto();
                    if (_listaProductosAgregados == null)
                        _listaProductosAgregados = new Dictionary<Producto, int>();
                    foreach (Producto pr in _listaProductos.Values.ToList())
                    {
                        if (pr.ToString() == ListBoxProductos.SelectedItem.Text)
                        {
                            
                            
                            
                            
                            bool agregar=true;
                            foreach (Producto pi in _listaProductosAgregados.Keys.ToList())
                            {
                                if (pi.Idproducto == pr.Idproducto)
                                    agregar = false;
                            }

                            if (agregar)
                            {
                                if (!_listaProductosAgregados.ContainsKey(pr))
                                    _listaProductosAgregados.Add(pr, cant);

                            }
                            else
                            {
                                LblValidarAgregarProducto.Text = "Producto ya agregado";
                            }

                            Session["listaProd"] = _listaProductosAgregados;
                            ListBoxProductosAgregados.DataSource = _listaProductosAgregados.Keys.ToList();
                            ListBoxProductosAgregados.DataBind();
                            break;
                        }
                    }
                }
            }
            else
            {
                LblValidarAgregarProducto.Text="Escriba una cantidad";
            }
        }

        protected void ListBoxProductosAgregados_SelectedIndexChanged(object sender, EventArgs e)
        {
            //if (Session["listaProd"] != null)
            //    _listaProductosAgregados = (Dictionary<Producto, int>)Session["listaProd"];
            //else
            //{
            //    if (_listaProductosAgregados == null)
            //        _listaProductosAgregados = new Dictionary<Producto, int>();
            //}
            //foreach (Producto pr in _listaProductosAgregados.Keys.ToList())
            //{
            //    if (pr.ToString() == ListBoxProductosAgregados.SelectedItem.Text)
            //    {
            //        TxtCantidad.Text = _listaProductosAgregados[pr].ToString();

            //        break;
            //    }
            //}

        }
    }
}
