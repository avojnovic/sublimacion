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
    public partial class ProductoABM : System.Web.UI.Page
    {
        Producto _producto;

        Dictionary<long, Insumo> _listaInsumos;
       
        ModosEdicionEnum _modoApertura = new ModosEdicionEnum();

        protected void Page_Load(object sender, EventArgs e)
        {
            BtnBorrar.Attributes.Add("OnClick", "javascript:if(confirm('Esta seguro que desea borrar el Producto')== false) return false;");

            string id = Request.QueryString["id"];

            if (id != null)
            {
                _producto = ProductoDAO.obtenerProductoPorId(id);

            }

            _listaInsumos = InsumoDAO.obtenerInsumoTodos();

            LblMensaje.Text = "";

            if (!IsPostBack)
            {
                cargarProducto();

                Session["Insumo"] = _listaInsumos;

                cargarGrilla();
                setearGrillaSiEstaVacia();
            
            }

         
            if (_producto != null)
            {
                _modoApertura = ModosEdicionEnum.Modificar;
                BtnBorrar.Visible = true;
            }
            else
            {
                _modoApertura = ModosEdicionEnum.Nuevo;
                BtnBorrar.Visible = false;
            }

        }

      
        private void cargarProducto()
        {
            if (_producto != null)
            {

                TxtNombre.Text = _producto.Nombre;
                TxtPrecio.Text = _producto.Precio.ToString();
                TxtTiempo.Text = _producto.Tiempo.ToString();

                foreach (Insumo i in _producto.Insumos.Values.ToList())
                {
                    if (_listaInsumos.ContainsKey(i.Idinsumo))
                        _listaInsumos[i.Idinsumo].Cantidad = _producto.Insumos[i.Idinsumo].Cantidad;

                }
            }
            else
            {
                _modoApertura = ModosEdicionEnum.Nuevo;
            }
        }

       

        private void setearObjeto()
        {
            if (_producto == null)
                _producto = new Producto();


            _producto.Nombre = TxtNombre.Text;
            _producto.Precio = decimal.Parse(TxtPrecio.Text);
            _producto.Tiempo = decimal.Parse(TxtTiempo.Text);
          
            //corregir el temita de los decimales :P

            _producto.Insumos= new Dictionary<long,Insumo>();


            Dictionary<long, Insumo> dt = (Dictionary<long, Insumo>)Session["Insumo"];
            foreach (Insumo i in dt.Values.ToList())
            {
                if (i.Cantidad > 0)
                    _producto.Insumos.Add(i.Idinsumo, i);
                 
            }

        }

        private void cargarGrilla()
        {
            Dictionary<long, Insumo> dt = (Dictionary<long, Insumo>)Session["Insumo"];
            GridViewInsumos.DataSource = dt.Values.ToList();
            GridViewInsumos.DataBind();
        }
        private void setearGrillaSiEstaVacia()
        {

            if (GridViewInsumos.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Idinsumo");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("Cantidad");


                dt.Rows.Add(new object[] { "", "", "" });

                GridViewInsumos.DataSource = dt;
                GridViewInsumos.DataBind();
            }

        }

        protected void GridViewInsumos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewInsumos.PageIndex = e.NewPageIndex;
            GridViewInsumos.DataBind();
        }


        protected void GridViewInsumos_RowEditing(object sender, GridViewEditEventArgs e)
        {
            GridViewInsumos.EditIndex = e.NewEditIndex;
            cargarGrilla();
        }

        protected void GridViewInsumos_OnRowCancelingEdit(Object sender, GridViewCancelEditEventArgs e)
        {
            GridViewInsumos.EditIndex = -1;
            cargarGrilla();
        }


        protected void GridViewInsumos_OnRowUpdating(object sender, GridViewUpdateEventArgs e)
        {

            int index = GridViewInsumos.EditIndex;
            GridViewRow row = GridViewInsumos.Rows[index];

            Label id = (Label)row.FindControl("LblIdinsumo");
            TextBox cantidad = (TextBox)row.FindControl("TxtCantidad");
            Dictionary<long, Insumo> dt = (Dictionary<long, Insumo>)Session["Insumo"];

            dt[long.Parse(id.Text)].Cantidad = int.Parse(cantidad.Text);
            Session["Insumo"] = dt;

            GridViewInsumos.EditIndex = -1;
            cargarGrilla();
        }


        protected void BtnGuardar_Click(object sender, EventArgs e)
        {


            if (_modoApertura == ModosEdicionEnum.Nuevo)
            {
                setearObjeto();
                ProductoDAO.insertarProducto(_producto);

            }
            else
            {
                if (_modoApertura == ModosEdicionEnum.Modificar)
                {
                    setearObjeto();
                    ProductoDAO.actualizarProducto(_producto);
                }
            }


            Response.Redirect("ProductoVer.aspx");

        }

        
        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("ProductoVer.aspx");
        }

        protected void BtnBorrar_Click(object sender, EventArgs e)
        {

            setearObjeto();
            _producto.Borrado = true;
            ProductoDAO.actualizarProducto(_producto);
            Response.Redirect("ProductoVer.aspx");

        }
    }
}