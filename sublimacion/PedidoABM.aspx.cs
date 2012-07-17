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
        Dictionary<long, Catalogo> _listaCatalogo = new Dictionary<long, Catalogo>();
        Dictionary<long, Plantilla> _listaPlantilla = new Dictionary<long, Plantilla>();


        Dictionary<Producto, int> _listaProductosAgregados = new Dictionary<Producto, int>();
        Usuario user;

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];

            if (id != null && id!="")
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

        private void calcularPrecio()
        {
            List<LineaPedido> dt = (List<LineaPedido>)Session["Productos"];

            decimal precio = 0;
            foreach (LineaPedido p in dt)
            {
                precio += p.Cantidad * p.Producto.Precio;
            }

            lblPrecioFinal.Text = precio.ToString();
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

                CmbEstado.SelectedValue = _pedido.EstadoId.ToString();

                CmbCliente.SelectedValue = _pedido.Cliente.IdCliente.ToString();


                Session["Productos"] = _pedido.LineaPedido;
               
                GridViewProductos.DataSource = _pedido.LineaPedido;
                GridViewProductos.DataBind();

                calcularPrecio();

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
            else
            {
                _pedido.EstadosPedido[long.Parse(this.CmbEstado.SelectedValue)].Fecha_fin = null;
               
                foreach (EstadosPedido e in _pedido.EstadosPedido.Values.ToList())
                {

                    if (e.Estado.Id != _pedido.EstadosPedido[long.Parse(this.CmbEstado.SelectedValue)].Estado.Id && e.Fecha_fin == null)
                    {
                        e.Fecha_fin = DateTime.Now;
                    }
                }
            }

            _pedido.LineaPedido = new List<LineaPedido>();



            List<LineaPedido> dt = (List<LineaPedido>)Session["Productos"];

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
                dt.Columns.Add("ArchivoClienteNombreMostrable");
                dt.Columns.Add("ArchivoDisenioNombreMostrable");

                dt.Rows.Add(new object[] { "", "", "", "", "","" });

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


        protected void BtnSeleccionarArchivo_Click(object sender, EventArgs e)
        {


        }

        protected void BtnAgregar_Click(object sender, EventArgs e)
        {


            List<LineaPedido> dt = (List<LineaPedido>)Session["Productos"];

            if (dt == null)
            {
                dt = new List<LineaPedido>();
            }
            LineaPedido p = new LineaPedido();
            p.Producto = ProductoDAO.obtenerProductoPorId(DDLProducto.SelectedValue.Trim());
            p.Catalogo=CatalogoDAO.obtenerCatalogoPorId(DDLCatalogo.SelectedValue.Trim());
            p.Plantilla = PlantillaDAO.obtenerPlantillaPorId(DDLPlantilla.SelectedValue.Trim());
            p.Cantidad = int.Parse(TxtCantidad.Text);

            if ((FileUsuario.PostedFile != null) && (FileUsuario.PostedFile.ContentLength > 0))
            {
                string fn = DateTime.Now.ToString("yyyyMMddhhmmssffff") + System.IO.Path.GetFileName(FileUsuario.PostedFile.FileName);
                p.ArchivoCliente = fn;
                string SaveLocation = Server.MapPath("Data") + "\\" +fn;
                try
                {
                    FileUsuario.PostedFile.SaveAs(SaveLocation);
                   
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }


            dt.Add(p);
            Session["Productos"] = dt;

            GridViewProductos.DataSource = dt;
            GridViewProductos.DataBind();

            calcularPrecio();

        }

        protected void GridViewProductos_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Borrar")
            {

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewProductos.Rows[index];

                
                Label Id = row.FindControl("LblIdproducto") as Label;
                Label Cata = row.FindControl("LblCata") as Label;
                Label Plant = row.FindControl("LblPlant") as Label;
                Label Cant = row.FindControl("LblCant") as Label;
                Label NombreArchivo = row.FindControl("LblArchivo") as Label;
             
                
                if (Id.Text.Trim() != "")
                {
                    List<LineaPedido> dt = (List<LineaPedido>)Session["Productos"];
                    List<LineaPedido> dtN = new List<LineaPedido>();
                    foreach (LineaPedido p in dt)
                    {
                        if (p.Producto.Idproducto.ToString() == Id.Text.Trim() && p.CatalogoNombre == Cata.Text && p.PlantillaNombre == Plant.Text && p.Cantidad.ToString() == Cant.Text && p.ArchivoClienteNombreMostrable == NombreArchivo.Text.Trim())
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

                    calcularPrecio();
                }
            }
            
            
            if (e.CommandName == "VerAdjunto")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewProductos.Rows[index];
                Label Id = row.FindControl("LblIdproducto") as Label;
                Label Cata = row.FindControl("LblCata") as Label;
                Label Plant = row.FindControl("LblPlant") as Label;
                Label Cant = row.FindControl("LblCant") as Label;
                Label NombreArchivo = row.FindControl("LblArchivo") as Label;


                if (Id.Text.Trim() != "" && NombreArchivo.Text.Trim()!="")
                {
                    List<LineaPedido> dt = (List<LineaPedido>)Session["Productos"];
                  
                    foreach (LineaPedido p in dt)
                    {
                        if (p.Producto.Idproducto.ToString() == Id.Text.Trim() && p.CatalogoNombre == Cata.Text && p.PlantillaNombre == Plant.Text && p.Cantidad.ToString() == Cant.Text && p.ArchivoClienteNombreMostrable == NombreArchivo.Text.Trim())
                        {
                            if (p.ArchivoCliente != "")
                            {
                                Response.AppendHeader("content-disposition", "attachment; filename=" + p.ArchivoClienteNombreMostrable);
                                Response.WriteFile("Data\\" + p.ArchivoCliente);
                                Response.End();
                            }
                        }
                        else
                        {
                            
                        }
                    }

                 
                }
            }

            if (e.CommandName == "VerAdjuntoDisenio")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewProductos.Rows[index];
                Label Id = row.FindControl("LblIdproducto") as Label;
                Label Cata = row.FindControl("LblCata") as Label;
                Label Plant = row.FindControl("LblPlant") as Label;
                Label Cant = row.FindControl("LblCant") as Label;
                Label NombreArchivo = row.FindControl("LblArchivo") as Label;
                Label NombreArchivoDisenio = row.FindControl("LblArchivoDisenio") as Label;

                if (Id.Text.Trim() != "" && NombreArchivoDisenio.Text.Trim() != "")
                {
                    List<LineaPedido> dt = (List<LineaPedido>)Session["Productos"];

                    foreach (LineaPedido p in dt)
                    {
                        if (p.Producto.Idproducto.ToString() == Id.Text.Trim() && p.CatalogoNombre == Cata.Text && p.PlantillaNombre == Plant.Text && p.Cantidad.ToString() == Cant.Text && p.ArchivoClienteNombreMostrable == NombreArchivo.Text.Trim() && p.ArchivoDisenioNombreMostrable == NombreArchivoDisenio.Text.Trim())
                        {
                            if (p.ArchivoDisenio != "")
                            {
                                Response.AppendHeader("content-disposition", "attachment; filename=" + p.ArchivoDisenioNombreMostrable);
                                Response.WriteFile("Data\\" + p.ArchivoDisenio);
                                Response.End();
                            }
                        }
                        else
                        {

                        }
                    }


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
