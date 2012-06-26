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
using sublimacion.BussinesObjects.BussinesObjects;

namespace sublimacion
{
    public partial class ClienteABM : System.Web.UI.Page
    {
        Cliente _cliente;


        ModosEdicionEnum _modoApertura = new ModosEdicionEnum();

        protected void Page_Load(object sender, EventArgs e)
        {
            BtnBorrar.Attributes.Add("OnClick", "javascript:if(confirm('Esta seguro que desea borrar el Cliente')== false) return false;");

            string id = Request.QueryString["id"];

            if (id != null)
            {
                _cliente = ClienteDAO.obtenerClientePorId(id);

            }

            LblMensaje.Text = "";

            if (!IsPostBack)
            {
                cargarCliente();
            }

            TxtFecha.ReadOnly = true;

            if (_cliente != null)
            {
                _modoApertura = ModosEdicionEnum.Modificar;
                BtnBorrar.Visible = true;
            }
            else
            {
                _modoApertura = ModosEdicionEnum.Nuevo;
                TxtFecha.Text = DateTime.Now.ToShortDateString();
              
                BtnBorrar.Visible = false;
            }

        }

        private void cargarCliente()
        {
            if (_cliente != null)
            {

                TxtNombre.Text = _cliente.Nombre;
                TxtApellido.Text = _cliente.Apellido;
                TxtDni.Text= _cliente.Dni.ToString();
                TxtDireccion.Text =_cliente.Direccion;
                TxtTelefono.Text = _cliente.Telefono;
                TxtMail.Text = _cliente.Mail;
                TxtFecha.Text= _cliente.Fecha.ToString();
                
           }
            else
            {
                _modoApertura = ModosEdicionEnum.Nuevo;
            }
        }

        protected void BtnGuardar_Click(object sender, EventArgs e)
        {


            if (_modoApertura == ModosEdicionEnum.Nuevo)
            {
                setearObjeto();
                ClienteDAO.insertarCliente(_cliente);

            }
            else
            {
                if (_modoApertura == ModosEdicionEnum.Modificar)
                {
                    setearObjeto();
                    ClienteDAO.actualizarCliente(_cliente);
                }
            }


            Response.Redirect("ClienteVer.aspx");

        }

        private void setearObjeto()
        {
            if (_cliente == null)
                _cliente = new BussinesObjects.BussinesObjects.Cliente();


            _cliente.Nombre = TxtNombre.Text;
            _cliente.Apellido = TxtApellido.Text;
            _cliente.Dni = long.Parse(TxtDni.Text);
             _cliente.Direccion = TxtDireccion.Text;
            _cliente.Telefono = TxtTelefono.Text;
            _cliente.Mail = TxtMail.Text;
          
         
        }

        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("ClienteVer.aspx");
        }

        protected void BtnBorrar_Click(object sender, EventArgs e)
        {

            setearObjeto();
            _cliente.Borrado = true;
            ClienteDAO.actualizarCliente(_cliente);
            Response.Redirect("ClienteVer.aspx");

        }

    }
}