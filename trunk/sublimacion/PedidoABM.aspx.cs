﻿using System;
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
        Dictionary<long, Catalogo> _listaCatalogo = new Dictionary<long, Catalogo>();
        Dictionary<long, Plantilla> _listaPlantilla = new Dictionary<long, Plantilla>();


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


            DDLProducto.DataSource = _listaProductos.Values.ToList();
            DDLProducto.DataTextField = "Nombre";
            DDLProducto.DataValueField = "Idproducto";
            DDLProducto.DataBind();

            CargarCatalogos();

        }

        protected void DDLProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            DDLPlantilla.Items.Clear();
            DDLCatalogo.Items.Clear();

            CargarCatalogos();
        }

        private void CargarCatalogos()
        {
            if (DDLProducto.SelectedValue != null && DDLProducto.SelectedValue != "")
            {
                _listaCatalogo = CatalogoDAO.obtenerCatalogoPorIdProducto(DDLProducto.SelectedValue.Trim());
                DDLCatalogo.DataSource = _listaCatalogo.Values.ToList();
                DDLCatalogo.DataTextField = "Nombre";
                DDLCatalogo.DataValueField = "IdCatalogo";
                DDLCatalogo.DataBind();

                CargarPlantilla();
            }
        }

        protected void DDLCatalogo_SelectedIndexChanged1(object sender, EventArgs e)
        {
            DDLPlantilla.Items.Clear();
            CargarPlantilla();
        }

        private void CargarPlantilla()
        {
            if (DDLCatalogo.SelectedValue != null && DDLCatalogo.SelectedValue!="")
            {
                _listaPlantilla = PlantillaDAO.obtenerPlantillaPorCatalogo(DDLCatalogo.SelectedValue.Trim());
                DDLPlantilla.DataSource = _listaPlantilla.Values.ToList();
                DDLPlantilla.DataTextField = "Nombre";
                DDLPlantilla.DataValueField = "IdPlantilla";
                DDLPlantilla.DataBind();
            }
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


                Session["Productos"] = _pedido.LineaPedido;
               
                GridViewProductos.DataSource = _pedido.LineaPedido;
                GridViewProductos.DataBind();

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

            _pedido.LineaPedido = new List<Producto>();



            List<Producto> dt = (List<Producto>)Session["Productos"];

            _pedido.LineaPedido = dt;
            






        }


      
        private void setearGrillaSiEstaVacia()
        {

            if (GridViewProductos.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Idproducto");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("CatalogoNombre");
                dt.Columns.Add("PlantillaNombre");
                dt.Columns.Add("Cantidad");


                dt.Rows.Add(new object[] { "", "", "", "", "" });

                GridViewProductos.DataSource = dt;
                GridViewProductos.DataBind();
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

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {
            List<Producto> dt = (List<Producto>)Session["Productos"];

            if (dt == null)
            {
                dt = new List<Producto>();
            }

            Producto p = ProductoDAO.obtenerProductoPorId(DDLProducto.SelectedValue.Trim());
            p.Catalogo=CatalogoDAO.obtenerCatalogoPorId(DDLCatalogo.SelectedValue.Trim());
            p.Plantilla = PlantillaDAO.obtenerPlantillaPorId(DDLPlantilla.SelectedValue.Trim());
            p.Cantidad = int.Parse(TxtCantidad.Text);

            dt.Add(p);
            Session["Productos"] = dt;

            GridViewProductos.DataSource = dt;
            GridViewProductos.DataBind();

        }

        protected void GridViewProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Borrar")
            {
                // Retrieve the row index stored in the 
                // CommandArgument property.
                int index = Convert.ToInt32(e.CommandArgument);

                // Retrieve the row that contains the button 
                // from the Rows collection.
                GridViewRow row = GridViewProductos.Rows[index];

                
                Label Id = row.FindControl("LblIdproducto") as Label;
                Label Cata = row.FindControl("LblCata") as Label;
                Label Plant = row.FindControl("LblPlant") as Label;
                Label Cant = row.FindControl("LblCant") as Label;
             
                
                if (Id.Text.Trim() != "")
                {
                    List<Producto> dt = (List<Producto>)Session["Productos"];
                    List<Producto> dtN = new List<Producto>();
                    foreach (Producto p in dt)
                    {
                        if (p.Idproducto.ToString() == Id.Text.Trim() && p.CatalogoNombre == Cata.Text && p.PlantillaNombre == Plant.Text && p.Cantidad.ToString() == Cant.Text)
                        {

                        }
                        else
                        {
                            dtN.Add(p);
                        }
                    }

                    Session["Productos"] = dtN;
                    GridViewProductos.DataSource = dtN;
                    GridViewProductos.DataBind();
                }
            }

        }
        protected void GridViewProductos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewProductos.PageIndex = e.NewPageIndex;
            GridViewProductos.DataBind();
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

     


       

      


    }
}
