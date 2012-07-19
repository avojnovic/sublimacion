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

            if (id != null  && id!="")
            {
                
                _catalogo = CatalogoDAO.obtenerCatalogoPorId(id);

            }

            _listaPlantilla = PlantillaDAO.obtenerPlantillaTodos();
            _dicProductos = ProductoDAO.obtenerProductoTodos();


            LblMensaje.Text = "";

            if (!IsPostBack)
            {
                Session["Plantilla"] = null;

                cargarGrilla();
                setearGrillaSiEstaVacia();


                cargarCatalogo();

               

                ComboProducto.DataValueField = "Idproducto";
                ComboProducto.DataTextField = "Nombre";

                ComboProducto.DataSource = _dicProductos.Values.ToList();
                ComboProducto.DataBind();

              

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
                    Session["Plantilla"] = _catalogo.Plantilla;
                    GridViewCheckBoxSetear();
                }



            }
            else
            {
                _modoApertura = ModosEdicionEnum.Nuevo;
                TxtFecha.Text = DateTime.Now.ToShortDateString();
            }
        }

        public void GridViewCheckBoxGuardar()
        {

            Dictionary<long, Plantilla> dp = (Dictionary<long, Plantilla>)Session["Plantilla"];

            if (dp == null)
            {
                dp = new Dictionary<long, Plantilla>();
            }

            for (int i = 0; i < GridViewPlantillas.Rows.Count; i++)
            {
                GridViewRow row = GridViewPlantillas.Rows[i];
                bool isChecked = ((CheckBox)row.FindControl("checkBoxPlantilla")).Checked;
                Label id = ((Label)row.FindControl("LblIdPlantilla"));

                if (isChecked)
                {
                   
                    if (dp.ContainsKey(long.Parse(id.Text)))
                    {
                        dp[long.Parse(id.Text)].Pertenece = true;
                    }
                    else
                    {
                        _listaPlantilla[long.Parse(id.Text)].Pertenece = true;
                        dp.Add(long.Parse(id.Text), _listaPlantilla[long.Parse(id.Text)]);
                    }


                }
                else
                {
                    if (dp.ContainsKey(long.Parse(id.Text)))
                    {
                        dp.Remove(long.Parse(id.Text));
                    }
                    
                }
            }

            Session["Plantilla"] = dp;
        }

        public void GridViewCheckBoxSetear()
        {

            Dictionary<long, Plantilla> _dic = (Dictionary<long, Plantilla>)Session["Plantilla"];

            if (_dic != null)
            {
                foreach (Plantilla p in _dic.Values.ToList())
                {
                    for (int i = 0; i < GridViewPlantillas.Rows.Count; i++)
                    {
                        GridViewRow row = GridViewPlantillas.Rows[i];

                        Label id = ((Label)row.FindControl("LblIdPlantilla"));


                        if (id.Text.Trim() == p.IdPlantilla.ToString())
                        {
                            if (p.Pertenece)
                            {
                                ((CheckBox)row.FindControl("checkBoxPlantilla")).Checked = p.Pertenece;
                            }
                            break;
                        }
                    }
                }
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


            GridViewCheckBoxGuardar();

            _catalogo.Plantilla = (Dictionary<long, Plantilla>)Session["Plantilla"];
        }


        private void cargarGrilla()
        {

            GridViewPlantillas.DataSource = _listaPlantilla.Values.ToList();
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

            GridViewCheckBoxGuardar();
            GridViewPlantillas.PageIndex = e.NewPageIndex;
            cargarGrilla();
            GridViewCheckBoxSetear();



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