using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using sublimacion.BussinesObjects.BussinesObjects;
using sublimacion.DataAccessObjects.DataAccessObjects;
using System.Data;

namespace sublimacion
{
    public partial class RegistrarDisenio : System.Web.UI.Page
    {

        ModosEdicionEnum _modoApertura = new ModosEdicionEnum();
        Pedido _pedido;

        Dictionary<long, Estado> _listaEstados = new Dictionary<long, Estado>();
       /* Dictionary<long, Cliente> _listaClientes = new Dictionary<long, Cliente>();

        Dictionary<long, Producto> _listaProductos = new Dictionary<long, Producto>();
        Dictionary<long, Catalogo> _listaCatalogo = new Dictionary<long, Catalogo>();
        Dictionary<long, Plantilla> _listaPlantilla = new Dictionary<long, Plantilla>();*/


       /* Dictionary<Producto, int> _listaProductosAgregados = new Dictionary<Producto, int>();*/

        Usuario user;

        protected void Page_Load(object sender, EventArgs e)
        {
           string id = Request.QueryString["id"];

            if (id != null && id != "")
            {
                _pedido = PedidoDAO.obtenerPorId(id);
            }


            /* 
             _listaClientes = ClienteDAO.obtenerClienteTodos();
             _listaProductos = ProductoDAO.obtenerProductoTodos();

             */

            _listaEstados = TipoEstadoDAO.obtenerEstados();

            LblComentario.Text = "";
            user = (Usuario)Session["usuario"];
         

            if (!IsPostBack)
            {

                cargarEstado();
                cargarPedido();

                setearGrillaSiEstaVacia();
            }


     

        }

        private void cargarEstado()
        {
            CmbEstado.DataSource = _listaEstados.Values.ToList();
            CmbEstado.DataTextField = "Descripcion";
            CmbEstado.DataValueField = "Id";
            CmbEstado.DataBind();
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

                TxtCliente.Text = _pedido.Cliente.NombreCompleto.ToString();

        



                Session["Productos"] = _pedido.LineaPedido;

                GridViewLineaPedido.DataSource = _pedido.LineaPedido;
                GridViewLineaPedido.DataBind();

               

            }
            
        }

   

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {

            PedidoDAO.actualizarPedido(_pedido);

            Response.Redirect("DiseniosPendientes.aspx");
          

        }

       /* private void setearObjeto()
        {

            if (_pedido == null)
                _pedido = new Pedido();

            _pedido.Cliente = TxtCliente.Text.Trim();

            _pedido.Borrado = false;
            _pedido.Comentario = TxtComentario.Text.Trim();

            if (TxtPrioridad.Text.Trim() == "")
                _pedido.Prioridad = 0;
            else
                _pedido.Prioridad = int.Parse(TxtPrioridad.Text.Trim());

            _pedido.Ubicacion = TxtUbicacion.Text.Trim();

                     



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

            _pedido.LineaPedido = new List<LineaPedido>();



            List<LineaPedido> dt = (List<LineaPedido>)Session["Productos"];

            _pedido.LineaPedido = dt;







        }*/



        private void setearGrillaSiEstaVacia()
        {

            if (GridViewLineaPedido.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Idproducto");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("CatalogoNombre");
                dt.Columns.Add("PlantillaNombre");
                dt.Columns.Add("Cantidad");
                dt.Columns.Add("ArchivoClienteNombreMostrable");


                dt.Rows.Add(new object[] { "", "", "", "", "", "" });

                GridViewLineaPedido.DataSource = dt;
                GridViewLineaPedido.DataBind();
            }

        }

        protected void GridViewLineaPedido_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewLineaPedido.PageIndex = e.NewPageIndex;
            GridViewLineaPedido.DataBind();
        }

        protected void BtnSalir_Click(object sender, EventArgs e)
        {

          
                Response.Redirect("DiseniosPendientes.aspx");
            
        }


        protected void BtnSeleccionarArchivo_Click(object sender, EventArgs e)
        {


        }



        protected void GridViewLineaPedido_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "SubirDisenio")
            { 

                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewLineaPedido.Rows[index];


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
                    GridViewLineaPedido.DataSource = dtN;
                    GridViewLineaPedido.DataBind();

                    
                }
            }


            if (e.CommandName == "VerAdjunto")
            {
                int index = Convert.ToInt32(e.CommandArgument);
                GridViewRow row = GridViewLineaPedido.Rows[index];
                Label Id = row.FindControl("LblIdproducto") as Label;
                Label Cata = row.FindControl("LblCata") as Label;
                Label Plant = row.FindControl("LblPlant") as Label;
                Label Cant = row.FindControl("LblCant") as Label;
                Label NombreArchivo = row.FindControl("LblArchivo") as Label;


                if (Id.Text.Trim() != "")
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

        }
        

        

           

     
    
    }
}