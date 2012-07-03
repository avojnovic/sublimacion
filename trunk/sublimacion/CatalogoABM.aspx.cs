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
    public partial class CatalogoABM : System.Web.UI.Page
    {
        Catalogo _catalogo;

        Dictionary<long, Plantilla> _listaPlantilla;

        Dictionary<long, Producto> _dicProductos = new Dictionary<long, Producto>();

        ModosEdicionEnum _modoApertura = new ModosEdicionEnum();

        protected void Page_Load(object sender, EventArgs e)
        {
            BtnBorrar.Attributes.Add("OnClick", "javascript:if(confirm('Esta seguro que desea borrar el Catalogo')== false) return false;");

            string id = Request.QueryString["id"];
            TxtFecha.ReadOnly = true;

            if (id != null)
            {
                
                _catalogo = CatalogoDAO.obtenerCatalogoPorId(id);

            }

            _listaPlantilla = PlantillaDAO.obtenerPlantillaTodos();
            _dicProductos = ProductoDAO.obtenerProductoTodos();


            LblMensaje.Text = "";

            if (!IsPostBack)
            {
                cargarCatalogo();

                Session["Plantilla"] = _listaPlantilla;

                ComboProducto.DataValueField = "Idproducto";
                ComboProducto.DataTextField = "Nombre";

                ComboProducto.DataSource = _dicProductos.Values.ToList();
                ComboProducto.DataBind();

                cargarGrilla();
                setearGrillaSiEstaVacia();

            }

    
            if (_catalogo != null)
            {
                _modoApertura = ModosEdicionEnum.Modificar;
                ComboProducto.Enabled = false;
                BtnBorrar.Visible = true;
            }
            else
            {
                _modoApertura = ModosEdicionEnum.Nuevo;
                TxtFecha.Text = DateTime.Now.ToShortDateString();
                BtnBorrar.Visible = false;
            }

        }


        private void cargarCatalogo()
        {
            if (_catalogo != null)
            {


                TxtNombre.Text = _catalogo.Nombre;
                TxtFecha.Text = _catalogo.Fecha.ToShortDateString();

                ComboProducto.SelectedValue = _catalogo.Producto.Idproducto.ToString();

                if (_catalogo.Plantilla != null)
                {
                    foreach (Plantilla p in _catalogo.Plantilla.Values.ToList())
                    {
                        if (_listaPlantilla.ContainsKey(p.IdPlantilla))
                            _listaPlantilla[p.IdPlantilla].Pertenece = _catalogo.Plantilla[p.IdPlantilla].Pertenece;

                    }
                }


            }
            else
            {
                _modoApertura = ModosEdicionEnum.Nuevo;
                TxtFecha.Text = DateTime.Now.ToShortDateString();
            }
        }

        private void setearObjeto()
        {
            if (_catalogo == null)
                _catalogo = new Catalogo();


            _catalogo.Nombre = TxtNombre.Text;
            _catalogo.Fecha = DateTime.Now;
            _catalogo.Producto = _dicProductos[long.Parse(ComboProducto.SelectedValue)];



            _catalogo.Plantilla = new Dictionary<long, Plantilla>();


           Dictionary<long, Plantilla> dp = (Dictionary<long, Plantilla>)Session["Plantilla"];

            foreach (Plantilla p in dp.Values.ToList())
            {
                if (p.Pertenece == true)
                    _catalogo.Plantilla.Add(p.IdPlantilla, p);

            }
        }


        private void cargarGrilla()
        {
            Dictionary<long, Plantilla> dp = (Dictionary<long, Plantilla>)Session["Plantilla"];
            GridViewPlantillas.DataSource = dp.Values.ToList();
            GridViewPlantillas.DataBind();
        }

        private void setearGrillaSiEstaVacia()
        {

            if (GridViewPlantillas.Rows.Count == 0)
            {
                DataTable dp = new DataTable();
                dp.Columns.Add("IdPlantilla");
                dp.Columns.Add("Nombre");
                dp.Columns.Add("Pertenece");


                dp.Rows.Add(new object[] { "", "", "" });

                GridViewPlantillas.DataSource = dp;
                GridViewPlantillas.DataBind();
            }

        }


        protected void GridViewPlantillas_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPlantillas.PageIndex = e.NewPageIndex;
            GridViewPlantillas.DataBind();
        }

        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("CatalogoVer.aspx");
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {
            if (_modoApertura == ModosEdicionEnum.Nuevo)
            {
                setearObjeto();
                CatalogoDAO.insertarCatalogo(_catalogo);
                

            }
            else
            {
                if (_modoApertura == ModosEdicionEnum.Modificar)
                {
                    setearObjeto();
                    CatalogoDAO.actualizarCatalogo(_catalogo);
                }
            }


            Response.Redirect("CatalogoVer.aspx");

        }

       

        protected void BtnBorrar_Click(object sender, EventArgs e)
        {
            setearObjeto();
            _catalogo.Borrado = true;
            CatalogoDAO.actualizarCatalogo(_catalogo);
            Response.Redirect("CatalogoVer.aspx");
        }
    }
}