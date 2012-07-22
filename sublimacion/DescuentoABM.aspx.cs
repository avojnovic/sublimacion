using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using sublimacion.BussinesObjects.BussinesObjects;
using sublimacion.DataAccessObjects.DataAccessObjects;
using System.Globalization;
using Fwk.Utils;

namespace sublimacion
{
    public partial class DescuentoABM : System.Web.UI.Page
    {
        Descuento _descuento;
        ModosEdicionEnum _modoApertura = new ModosEdicionEnum();

        Dictionary<long, Producto> _listaProductos = new Dictionary<long, Producto>();

        protected void Page_Load(object sender, EventArgs e)
        {

            string id = Request.QueryString["productoId"];
            string cantidad = Request.QueryString["cantidad"];

            TxtFecha.Enabled = false;


            if (id != null && cantidad != null && id!="")
            {
                _modoApertura = ModosEdicionEnum.Modificar;
                TxtCantidad.Enabled = false;
                CmbProducto.Enabled = false;
                _descuento = DescuentoDAO.obtenerDescuentoPorId(id, cantidad);

            }
            else
            {
                _modoApertura = ModosEdicionEnum.Nuevo;
                TxtFecha.Text = DateTime.Now.ToShortDateString();
            }

            _listaProductos = ProductoDAO.obtenerProductoTodos();

            LblMensaje.Text = "";
            if (!IsPostBack)
            {
                cargarCombo();
                cargarDescuento();

            }




        }

        private void cargarDescuento()
        {
            if (_descuento != null)
            {
                TxtCantidad.Text = _descuento.Cantidad.ToString();

                TxtDescuento.Text = _descuento.Descuento1.ToString().Replace(".", ",");

                TxtFecha.Text = _descuento.Fecha.ToShortDateString();


                CmbProducto.SelectedValue = _descuento.productoId.ToString();

                TxtPrecio.Text = _listaProductos[long.Parse(this.CmbProducto.SelectedValue)].Precio.ToString();

            }
            else
            {
                _modoApertura = ModosEdicionEnum.Nuevo;
            }
        }

        private void cargarCombo()
        {

            CmbProducto.DataSource = _listaProductos.Values.ToList();
            CmbProducto.DataTextField = "Nombre";
            CmbProducto.DataValueField = "Idproducto";
            CmbProducto.DataBind();


        }


        protected void BtnGuardar_Click(object sender, EventArgs e)
        {


            if (_modoApertura == ModosEdicionEnum.Nuevo)
            {
                setearObjeto();
                Descuento des = DescuentoDAO.obtenerDescuentoPorId(_descuento.Producto.Idproducto.ToString(), _descuento.Cantidad.ToString());

                if (des == null)
                    DescuentoDAO.insertarDescuento(_descuento);
                else
                    DescuentoDAO.actualizarDescuento(_descuento);

            }
            else
            {
                if (_modoApertura == ModosEdicionEnum.Modificar)
                {
                    setearObjeto();
                    DescuentoDAO.actualizarDescuento(_descuento);
                }
            }


            Response.Redirect("DescuentoVer.aspx");

        }


        private void setearObjeto()
        {
            if (_descuento == null)
                _descuento = new Descuento();




            _descuento.Producto = _listaProductos[long.Parse(this.CmbProducto.SelectedValue)];
            _descuento.Cantidad = int.Parse(TxtCantidad.Text);

            _descuento.Descuento1 = Utils.convertToDecimal(TxtDescuento.Text);

            _descuento.Fecha = DateTime.Now;



        }

        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("DescuentoVer.aspx");
        }

        protected void CmbProducto_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.CmbProducto.SelectedValue!=null)
                 TxtPrecio.Text = _listaProductos[long.Parse(this.CmbProducto.SelectedValue)].Precio.ToString();
        }




    }
}