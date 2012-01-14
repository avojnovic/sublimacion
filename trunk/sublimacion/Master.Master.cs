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

namespace sublimacion
{
    public partial class Master : System.Web.UI.MasterPage
    {
        Usuario user;
        protected void Page_Load(object sender, EventArgs e)
        {

            user = (Usuario)Session["usuario"];
            //LblUsuario.Text = user.Nombre;
           if(!IsPostBack)
            {
                if (!Page.Request.Url.ToString().ToLower().Contains("login.aspx") && !Page.Request.Url.ToString().ToLower().Contains("error.aspx"))
                {

                    if (user == null)
                    {
                        Response.Redirect("login.aspx");
                    }
                    else
                    {
                        this.divMenu.Visible = true;
                        generarMenu();
                    }
                }
                else
                {
                    this.divMenu.Visible = false;
                }
            }
        }

        protected void ScriptManager1_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            ScriptManager1.AsyncPostBackErrorMessage = e.Exception.Message;
            Response.Redirect(Request.Url.ToString());
        }

        private void generarMenu()
        {

            if ((user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.Diseniador) || (user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.Administrador) )
            {
                MenuItem itCa = new MenuItem("Catalogos");


                MenuItem itn = new MenuItem("Ver");
                itn.Value = "CatalogoVer";
                itn.NavigateUrl = "CatalogoVer.aspx";
                itCa.ChildItems.Add(itn);

                MenuItem ite = new MenuItem("Nuevo");
                ite.Value = "CatalogoNuevo";
                ite.NavigateUrl = "Catalogo.aspx";
                itCa.ChildItems.Add(ite);



                divMenu.Items.Add(itCa);


                MenuItem itPla = new MenuItem("Plantillas");

                MenuItem itPlaE = new MenuItem("Ver");
                itPlaE.Value = "PlantillaVer";
                itPlaE.NavigateUrl = "PlantillaVer.aspx";
                itPla.ChildItems.Add(itPlaE);

                MenuItem itPlaN = new MenuItem("Nuevo");
                itPlaN.Value = "PlantillaNuevo";
                itPlaN.NavigateUrl = "Plantilla.aspx";
                itPla.ChildItems.Add(itPlaN);

                

                divMenu.Items.Add(itPla);

                MenuItem itReg = new MenuItem("Registrar Diseño");

                itReg.Value = "RegistrarDisenio";
                itReg.NavigateUrl = "RegistrarDisenio.aspx";

                divMenu.Items.Add(itReg);

            }

            if ((user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.Vendedor) || (user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.Administrador) )
            {
                //INSUMO
                MenuItem itIns = new MenuItem("Insumos");


                MenuItem itInsE = new MenuItem("Ver");
                itInsE.Value = "InsumoVer";
                itInsE.NavigateUrl = "InsumoVer.aspx";
                itIns.ChildItems.Add(itInsE);

                MenuItem itInsN = new MenuItem("Nuevo");
                itInsN.Value = "InsumoNuevo";
                itInsN.NavigateUrl = "Insumo.aspx";
                itIns.ChildItems.Add(itInsN);




                divMenu.Items.Add(itIns);

                //PRODUCTO
                MenuItem itPro = new MenuItem("Productos");


                MenuItem itProE = new MenuItem("Ver");
                itProE.Value = "ProductoVer";
                itProE.NavigateUrl = "ProductoVer.aspx";
                itPro.ChildItems.Add(itProE);

                MenuItem itProN = new MenuItem("Nuevo");
                itProN.Value = "ProductoNuevo";
                itProN.NavigateUrl = "Producto.aspx";
                itPro.ChildItems.Add(itProN);


                divMenu.Items.Add(itPro);

                //DESCUENTO
                MenuItem itDe = new MenuItem("Descuentos");


                MenuItem itDeE = new MenuItem("Ver");
                itDeE.Value = "DescuentoVer";
                itDeE.NavigateUrl = "DescuentoVer.aspx";
                itDe.ChildItems.Add(itDeE);

                MenuItem itDeN = new MenuItem("Nuevo");
                itDeN.Value = "DescuentoNuevo";
                itDeN.NavigateUrl = "Descuento.aspx";
                itDe.ChildItems.Add(itDeN);





                divMenu.Items.Add(itDe);

               


                //ACEPTAR DISENIO
                MenuItem itAcep = new MenuItem("Aceptar Diseño");

                itAcep.Value = "AceptarDisenio";
                itAcep.NavigateUrl = "AceptarDisenio.aspx";

                divMenu.Items.Add(itAcep);


            }

            if ((user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.Vendedor) || (user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.Administrador) || (user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.JefeProduccion))
            {
             //PEDIDO
                MenuItem itPe = new MenuItem("Pedidos");

                MenuItem itPeE = new MenuItem("Ver");
                itPeE.Value = "PedidoVer";
                itPeE.NavigateUrl = "PedidoVer.aspx";
                itPe.ChildItems.Add(itPeE);

                if ((user.Perfil != sublimacion.BussinesObjects.Usuario.PerfilesEnum.JefeProduccion))
                {
                    MenuItem itPeN = new MenuItem("Nuevo");
                    itPeN.Value = "PedidoNuevo";
                    itPeN.NavigateUrl = "Pedido.aspx";
                    itPe.ChildItems.Add(itPeN);
                }

                

                //MenuItem itPeD = new MenuItem("Eliminar");
                //itPeD.Value = "PedidoEliminar";
                //itPeD.NavigateUrl = "Pedido.aspx";
                //itPe.ChildItems.Add(itPeD);

                divMenu.Items.Add(itPe);
            }

            if ((user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.JefeProduccion) || (user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.Administrador))
            {
                
                MenuItem itIns = new MenuItem("Logistica Producción");

                MenuItem itPeE = new MenuItem("Ver");
                itPeE.Value = "VerLogistica";
                itPeE.NavigateUrl = "VerLogistica.aspx";
                itIns.ChildItems.Add(itPeE);

                MenuItem itInsN = new MenuItem("Crear");
                itInsN.Value = "Crear";
                itInsN.NavigateUrl = "LogisticaProduccion.aspx";
                itIns.ChildItems.Add(itInsN);

            

                divMenu.Items.Add(itIns);
            }






            MenuItem itSalir = new MenuItem("Logout");

            itSalir.Value = "Salir";
            itSalir.NavigateUrl = "Login.aspx";

            divMenu.Items.Add(itSalir);

        }
    }
}
