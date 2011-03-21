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
            _insumo = (BussinesObjects.Insumo)Session["insumo"];
            LblMensaje.Text = "";
            if (!IsPostBack)
            {
                cargarInsumo();
            }
           
            
            if (_insumo != null)
            {
                _modoApertura = ModosEdicionEnum.Modificar;
            }
            else
            {
                _modoApertura = ModosEdicionEnum.Nuevo;
               
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

            if (verificar())
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

                Session["insumo"] = null;
                Response.Redirect("InsumoVer.aspx");
            }
        }

        private bool verificar()
        {
            if (TxtNombre.Text.Trim() == "")
            {
                LblMensaje.Text = "Completar campo Nombre";
                return false;
            }
            if (TxtCosto.Text.Trim() == "")
            {
                LblMensaje.Text = "Completar campo costo";
                return false;
            }
            if (TxtFabricante.Text.Trim() == "")
            {
                LblMensaje.Text = "Completar campo Fabricante";
                return false;
            }
            if (Txtstock.Text.Trim() == "")
            {
                LblMensaje.Text = "Completar campo Stock";
                return false;
            }
           
            try
            {
                int s = 0;
                s = int.Parse(Txtstock.Text.Trim());

            }
            catch(Exception )
            {
                LblMensaje.Text = "Corregir campo Stock";
                return false;
            }

            try
            {
                float s = 0;
                s = int.Parse(TxtCosto.Text.Trim().Replace(',','.'));

            }
            catch (Exception )
            {
                LblMensaje.Text = "Corregir campo Costo";
                return false;
            }


            return true;

        }

        private void setearObjeto()
        {
            if (_insumo == null)
                _insumo = new BussinesObjects.Insumo();

            _insumo.Nombre = TxtNombre.Text;
            _insumo.Costo = float.Parse(TxtCosto.Text.Trim().Replace(',', '.'));
            _insumo.NombreFab=TxtFabricante.Text.Trim() ;
            _insumo.FechaAct = DateTime.Now;
            _insumo.Stock=int.Parse( Txtstock.Text.Trim());

        }

        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            Session["insumo"] = null;
            Response.Redirect("InsumoVer.aspx");
        }

       

    }
}
