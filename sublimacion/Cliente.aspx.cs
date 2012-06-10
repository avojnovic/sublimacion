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
    public partial class Cliente : System.Web.UI.Page
    {
        BussinesObjects.BussinesObjects.Cliente _cliente;
        ModosEdicionEnum _modoApertura = new ModosEdicionEnum();

        protected void Page_Load(object sender, EventArgs e)
        {
            BtnBorrar.Attributes.Add("OnClick", "javascript:if(confirm('Esta seguro que desea borrar el Caso')== false) return false;");

            string id = Request.QueryString["id"];

            if (id != null)
            {
                _cliente = ClienteDAO.Instancia.obtenerClientePorId(id);

            }

            LblMensaje.Text = "";
            if (!IsPostBack)
            {
                cargarCliente();
            }


            if (_cliente != null)
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

        private void cargarCliente()
        {
            if (_cliente != null)
            {

                TxtNombre.Text = _cliente.Nombre;
              
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
                ClienteDAO.Instancia.insertarCliente(_cliente);

            }
            else
            {
                if (_modoApertura == ModosEdicionEnum.Modificar)
                {
                    setearObjeto();
                    ClienteDAO.Instancia.actualizar(_cliente);
                }
            }


            Response.Redirect("ClienteVer.aspx");

        }

        private void setearObjeto()
        {
            if (_cliente == null)
                _cliente = new BussinesObjects.BussinesObjects.Cliente();

            _cliente.Nombre = TxtNombre.Text;
           

        }

        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClienteVer.aspx");
        }

        protected void BtnBorrar_Click(object sender, EventArgs e)
        {

            setearObjeto();
            _cliente.Borrado = true;
            ClienteDAO.Instancia.actualizar(_cliente);
            Response.Redirect("ClienteVer.aspx");

        }

    }
}