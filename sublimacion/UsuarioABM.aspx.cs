using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using sublimacion.BussinesObjects.BussinesObjects;
using sublimacion.DataAccessObjects.DataAccessObjects;

namespace sublimacion
{
    public partial class UsuarioABM : System.Web.UI.Page
    {
        Usuario _usuario;

        Dictionary<long, Perfil> _listaPerfil = new Dictionary<long, Perfil>();

        ModosEdicionEnum _modoApertura = new ModosEdicionEnum();

        protected void Page_Load(object sender, EventArgs e)
        {
            BtnBorrar.Attributes.Add("OnClick", "javascript:if(confirm('Esta seguro que desea borrar el Usuario')== false) return false;");

            string id = Request.QueryString["id"];

            if (id != null && id != "")
            {
                _usuario = UsuarioDAO.obtenerUsuarioPorId(id);


            }

            _listaPerfil = PerfilDAO.obtenerTodos();

            LblMensaje.Text = "";

            if (!IsPostBack)
            {
                cargarUsuario();
                cargarPerfiles();
            }



            if (_usuario != null)
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

        private void cargarPerfiles()
        {
            CmbPerfil.DataSource = _listaPerfil.Values.ToList();
            CmbPerfil.DataTextField = "Nombre";
            CmbPerfil.DataValueField = "Id";
            CmbPerfil.DataBind();
        }

        private void cargarUsuario()
        {
            if (_usuario != null)
            {

                TxtNombre.Text = _usuario.Nombre;
                TxtApellido.Text = _usuario.Apellido;
                TxtMail.Text = _usuario.Email;
                TxtTelefono.Text = _usuario.Telefono.ToString();
                TxtUsuario.Text = _usuario.User;
                TxtPassword.Text = _usuario.Password;

                CmbPerfil.SelectedValue = ((int)_usuario.Perfil).ToString();


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
                Usuario user = UsuarioDAO.validarUsuario(_usuario.User);

                if (user == null || user.Id == 0)
                {
                    UsuarioDAO.insertarUsuario(_usuario);
                    Response.Redirect("UsuarioVer.aspx");
                }
                else
                {
                    LblMensaje.Text = "Ya existe ese Usuario!";
                }


            }
            else
            {
                if (_modoApertura == ModosEdicionEnum.Modificar)
                {
                    setearObjeto();
                    Usuario user = UsuarioDAO.validarUsuario(_usuario.User, _usuario.Id.ToString());

                    if (user == null || user.Id == 0)
                    {
                        UsuarioDAO.actualizarUsuario(_usuario);
                        Response.Redirect("UsuarioVer.aspx");
                    }
                    else
                    {
                        LblMensaje.Text = "Ya existe ese Usuario!";
                    }

                }
            }

            

        }

        private void setearObjeto()
        {
            if (_usuario == null)
                _usuario = new Usuario();


            _usuario.Nombre = TxtNombre.Text;
            _usuario.Apellido = TxtApellido.Text;
            _usuario.Email = TxtMail.Text;
            _usuario.Telefono = TxtTelefono.Text;
            _usuario.User = TxtUsuario.Text;
            _usuario.Password = TxtPassword.Text;

            switch (int.Parse(CmbPerfil.SelectedValue))
            {
                case 1:
                    _usuario.Perfil = Usuario.PerfilesEnum.Diseniador;
                    break;
                case 2:
                    _usuario.Perfil = Usuario.PerfilesEnum.Operario;
                    break;
                case 3:
                    _usuario.Perfil = Usuario.PerfilesEnum.Vendedor;
                    break;
                case 4:
                    _usuario.Perfil = Usuario.PerfilesEnum.JefeSuperior;
                    break;
                case 5:
                    _usuario.Perfil = Usuario.PerfilesEnum.JefeProduccion;
                    break;
                case 6:
                    _usuario.Perfil = Usuario.PerfilesEnum.Administrador;
                    break;

            }


        }

        protected void BtnSalir_Click(object sender, EventArgs e)
        {
            Response.Redirect("UsuarioVer.aspx");
        }

        protected void BtnBorrar_Click(object sender, EventArgs e)
        {

            setearObjeto();
            _usuario.Borrado = true;
            UsuarioDAO.actualizarUsuario(_usuario);
            Response.Redirect("UsuarioVer.aspx");

        }
    }
}