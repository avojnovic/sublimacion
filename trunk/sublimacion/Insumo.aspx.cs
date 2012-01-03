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
using sublimacion.BussinesObjects;
using sublimacion.DataAccessObjects.DataAccessObjects;

namespace sublimacion
{
    public partial class Insumo : System.Web.UI.Page
    {
        BussinesObjects.Insumo _insumo;
        ModosEdicionEnum _modoApertura = new ModosEdicionEnum();
        protected void Page_Load(object sender, EventArgs e)
        {
            BtnBorrar.Attributes.Add("OnClick", "javascript:if(confirm('Esta seguro que desea borrar el Caso')== false) return false;");

            string id = Request.QueryString["id"];

            if (id != null)
            {
                _insumo = InsumoDAO.Instancia.obtenerPorId(id);

            }

            LblMensaje.Text = "";
            if (!IsPostBack)
            {
                cargarInsumo();
            }


            if (_insumo != null)
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

        private void cargarInsumo()
        {
            if (_insumo != null)
            {

                TxtCosto.Text = _insumo.Costo.ToString();
                TxtFabricante.Text = _insumo.NombreFab;
                TxtFecha.Text = _insumo.FechaAct.ToShortDateString();
                TxtNombre.Text = _insumo.Nombre;
                Txtstock.Text = _insumo.Stock.ToString();
            }
            else
            {
                _modoApertura = ModosEdicionEnum.Nuevo;
            }
        }

        protected void Button1_Click(object sender, EventArgs e)
        {

          
                if (_modoApertura == ModosEdicionEnum.Nuevo)
                {
                    setearObjeto();
                    InsumoDAO.Instancia.insertarInsumo(_insumo);

                }
                else
                {
                    if (_modoApertura == ModosEdicionEnum.Modificar)
                    {
                        setearObjeto();
                        InsumoDAO.Instancia.actualizarInsumo(_insumo);
                    }
                }


                Response.Redirect("InsumoVer.aspx");
           
        }

       
        private void setearObjeto()
        {
            if (_insumo == null)
                _insumo = new BussinesObjects.Insumo();

            _insumo.Nombre = TxtNombre.Text;
            _insumo.Costo = float.Parse(TxtCosto.Text.Trim().Replace(',', '.'));
            _insumo.NombreFab = TxtFabricante.Text.Trim();
            _insumo.FechaAct = DateTime.Now;
            _insumo.Stock = int.Parse(Txtstock.Text.Trim());

        }

        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("InsumoVer.aspx");
        }

        protected void BtnBorrar_Click(object sender, EventArgs e)
        {

            setearObjeto();
            _insumo.Borrado = true;
            InsumoDAO.Instancia.actualizarInsumo(_insumo);
            Response.Redirect("InsumoVer.aspx");

        }

    }
}
