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
    public partial class PlantillaABM : System.Web.UI.Page
    {
        Plantilla _plantilla;

        ModosEdicionEnum _modoApertura = new ModosEdicionEnum();

        protected void Page_Load(object sender, EventArgs e)
        {
            BtnBorrar.Attributes.Add("OnClick", "javascript:if(confirm('Esta seguro que desea borrar la Plantilla')== false) return false;");

            string id = Request.QueryString["id"];

            if (id != null)
            {
                _plantilla = PlantillaDAO.obtenerPlantillaPorId(id);

            }

            LblMensaje.Text = "";
            if (!IsPostBack)
            {
                cargarPlantilla();
            }


            if (_plantilla != null)
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

        private void cargarPlantilla()
        {
            if (_plantilla != null)
            {

                TxtNombre.Text = _plantilla.Nombre;


                TxtAncho.Text = _plantilla.Medida_ancho.ToString().Replace(".",",");
                TxtLargo.Text = _plantilla.Medida_largo.ToString().Replace(".",",");
                
            }
            else
            {
                _modoApertura = ModosEdicionEnum.Nuevo;
            }
        }

        protected void BtnGuardar_Click (object sender, EventArgs e)
        {


            if (_modoApertura == ModosEdicionEnum.Nuevo)
            {
                setearObjeto();
                PlantillaDAO.insertarPlantilla(_plantilla);

            }
            else
            {
                if (_modoApertura == ModosEdicionEnum.Modificar)
                {
                    setearObjeto();
                    PlantillaDAO.actualizarPlantilla(_plantilla);
                }
            }


            Response.Redirect("PlantillaVer.aspx");

        }


        private void setearObjeto()
        {
            if (_plantilla == null)
                _plantilla = new Plantilla();

            _plantilla.Nombre = TxtNombre.Text;

            _plantilla.Medida_largo = Utils.convertToDecimal(TxtLargo.Text);
            _plantilla.Medida_ancho = Utils.convertToDecimal(TxtAncho.Text);
        
          

        }

        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("PlantillaVer.aspx");
        }

        protected void BtnBorrar_Click(object sender, EventArgs e)
        {

            setearObjeto();
            _plantilla.Borrado = true;
            PlantillaDAO.actualizarPlantilla(_plantilla);
            Response.Redirect("PlantillaVer.aspx");

        }
    }
}