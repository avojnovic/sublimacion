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

        Pedido _pedido;

        Dictionary<long, Estado> _listaEstados = new Dictionary<long, Estado>();
        Usuario user;

        protected void Page_Load(object sender, EventArgs e)
        {
            string id = Request.QueryString["id"];

            if (id != null && id != "")
            {
                _pedido = PedidoDAO.obtenerPorId(id);
            }


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
                TxtCliente.Text = _pedido.Cliente.NombreCompleto.ToString();

                CmbEstado.SelectedValue = _pedido.EstadoId.ToString();
                              
                Session["Productos"] = _pedido.LineaPedido;

                GridViewLineaPedido.DataSource = _pedido.LineaPedido;
                GridViewLineaPedido.DataBind();              
            }

        }



        protected void BtnGuardar_Click(object sender, EventArgs e)
        {

            setearObjeto();

            PedidoDAO.actualizarPedido(_pedido);

            Response.Redirect("DiseniosPendientes.aspx");
            
        }

        private void setearObjeto()
        {
            _pedido.LineaPedido = (List<LineaPedido>)Session["Productos"];
            _pedido.Comentario = TxtComentario.Text;

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
        }

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
                dt.Columns.Add("ArchivoDisenioNombreMostrable");

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

        protected void BtnAdjuntar_Click(object sender, EventArgs e)
        {
            if (lblLineaSeleccionada.Text != "")
            {
                //Solo si seleccione una linea me permite adjuntar.
                // la linea seleccionada la guarde en  Session["linea"]

                List<LineaPedido> dt = (List<LineaPedido>)Session["Productos"];
                LineaPedido _lineaSeleccionada = (LineaPedido)Session["linea"];

                foreach (LineaPedido p in dt)
                {
                    if (p.Idproducto == _lineaSeleccionada.Idproducto && p.Cantidad == _lineaSeleccionada.Cantidad && p.Plantilla.IdPlantilla == _lineaSeleccionada.Plantilla.IdPlantilla && p.Catalogo.IdCatalogo == _lineaSeleccionada.Catalogo.IdCatalogo && p.ArchivoCliente == _lineaSeleccionada.ArchivoCliente)
                    {
                        //ACA LO ENCONTRE

                        if ((FileDisenio.PostedFile != null) && (FileDisenio.PostedFile.ContentLength > 0))
                        {
                            string fn = DateTime.Now.ToString("yyyyMMddhhmmssffff") + System.IO.Path.GetFileName(FileDisenio.PostedFile.FileName);

                            //le pongo el nombre del archivo
                            p.ArchivoDisenio = fn;
                            string SaveLocation = Server.MapPath("Data") + "\\" + fn;
                            try
                            {
                                FileDisenio.PostedFile.SaveAs(SaveLocation);

                            }
                            catch (Exception ex)
                            {
                                throw ex;
                            }
                        }

                        break;
                    }
                }

                //actualizo la lista con el nombre del archivo adjuntado
                Session["Productos"] = dt;
                GridViewLineaPedido.DataSource = dt;
                GridViewLineaPedido.DataBind();
            }

            //limpio la seleccion
            lblLineaSeleccionada.Text = "";
            Session["linea"] = null;
        }


        protected void GridViewLineaPedido_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "Seleccionar")
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
                            Session["linea"] = p;
                            lblLineaSeleccionada.Text = "Producto: " + p.Producto.Nombre + " - Catalogo: " + p.CatalogoNombre + " - Plantilla: " + p.PlantillaNombre + " - Cantidad: " + p.Cantidad.ToString() + " - Archivo Cliente: " + p.ArchivoClienteNombreMostrable;
                        }

                    }

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


                if (Id.Text.Trim() != "" && NombreArchivo.Text.Trim() != "")
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
                GridViewRow row = GridViewLineaPedido.Rows[index];
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








    }
}