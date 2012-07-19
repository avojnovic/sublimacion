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
using sublimacion.BussinesObjects.BussinesObjects;

namespace sublimacion
{
    public partial class Master : System.Web.UI.MasterPage
    {
        Usuario user;
        protected void Page_Load(object sender, EventArgs e)
        {

            user = (Usuario)Session["usuario"];

            //if (!IsPostBack)
            //{
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
            //}
        }

        protected void ScriptManager1_AsyncPostBackError(object sender, AsyncPostBackErrorEventArgs e)
        {
            ScriptManager1.AsyncPostBackErrorMessage = e.Exception.Message;
            Response.Redirect(Request.Url.ToString());
        }

        private void generarMenu()
        {
          

            if ((user.Perfil == Usuario.PerfilesEnum.Diseniador) || (user.Perfil == Usuario.PerfilesEnum.Administrador))
            {
                //CATALOGO
                MenuItem itCa = new MenuItem("Catalogos");

                MenuItem itn = new MenuItem("Ver");
                itn.Value = "CatalogoVer";
                itn.NavigateUrl = "CatalogoVer.aspx";
                itCa.ChildItems.Add(itn);

                MenuItem ite = new MenuItem("Nuevo");
                ite.Value = "CatalogoNuevo";
                ite.NavigateUrl = "CatalogoABM.aspx";
                itCa.ChildItems.Add(ite);


                divMenu.Items.Add(itCa);

                //PLANTILLA
                MenuItem itPla = new MenuItem("Plantillas");

                MenuItem itPlaE = new MenuItem("Ver");
                itPlaE.Value = "PlantillaVer";
                itPlaE.NavigateUrl = "PlantillaVer.aspx";
                itPla.ChildItems.Add(itPlaE);

                MenuItem itPlaN = new MenuItem("Nuevo");
                itPlaN.Value = "PlantillaNuevo";
                itPlaN.NavigateUrl = "PlantillaABM.aspx";
                itPla.ChildItems.Add(itPlaN);


                divMenu.Items.Add(itPla);



            }

            if ((user.Perfil == Usuario.PerfilesEnum.Vendedor) || (user.Perfil == Usuario.PerfilesEnum.Administrador))
            {
                //INSUMO
                MenuItem itIns = new MenuItem("Insumos");

                MenuItem itInsE = new MenuItem("Ver");
                itInsE.Value = "InsumoVer";
                itInsE.NavigateUrl = "InsumoVer.aspx";
                itIns.ChildItems.Add(itInsE);

                MenuItem itInsN = new MenuItem("Nuevo");
                itInsN.Value = "InsumoNuevo";
                itInsN.NavigateUrl = "InsumoABM.aspx";
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
                itProN.NavigateUrl = "ProductoABM.aspx";
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
                itDeN.NavigateUrl = "DescuentoABM.aspx";
                itDe.ChildItems.Add(itDeN);

                divMenu.Items.Add(itDe);
            }

            if ((user.Perfil == Usuario.PerfilesEnum.Vendedor) || (user.Perfil == Usuario.PerfilesEnum.Administrador) || (user.Perfil == Usuario.PerfilesEnum.Diseniador))
            {
                //DISENIO
                MenuItem itDiseno = new MenuItem("Diseño");

                MenuItem itDisPe = new MenuItem("Diseños Pendientes");
                itDisPe.Value = "DiseniosPendientes";
                itDisPe.NavigateUrl = "DiseniosPendientes.aspx";
                itDiseno.ChildItems.Add(itDisPe);

                //MenuItem itReg = new MenuItem("Registrar Diseño");
                //itReg.Value = "RegistrarDisenio";
                //itReg.NavigateUrl = "RegistrarDisenio.aspx";
                //itDiseno.ChildItems.Add(itReg);


                //if ((user.Perfil == Usuario.PerfilesEnum.Vendedor)|| (user.Perfil == Usuario.PerfilesEnum.Administrador))
                //{
                //    //ACEPTAR DISEÑO

                //    MenuItem itAcepDi = new MenuItem("Aceptar Diseño");
                //    itAcepDi.Value = "AceptarDisenio";
                //    itAcepDi.NavigateUrl = "AceptarDisenio.aspx";
                //    itDiseno.ChildItems.Add(itAcepDi);


                //}


                divMenu.Items.Add(itDiseno);


            }

            if ((user.Perfil == Usuario.PerfilesEnum.Vendedor) || (user.Perfil == Usuario.PerfilesEnum.Administrador) || (user.Perfil == Usuario.PerfilesEnum.JefeProduccion))
            {

                //CLIENTE
                MenuItem itCli = new MenuItem("Clientes");
                MenuItem ititCliE = new MenuItem("Ver");
                ititCliE.Value = "ClienteVer";
                ititCliE.NavigateUrl = "ClienteVer.aspx";
                itCli.ChildItems.Add(ititCliE);

                MenuItem ititCliV = new MenuItem("Nuevo");
                ititCliV.Value = "ClienteNuevo";
                ititCliV.NavigateUrl = "ClienteABM.aspx";
                itCli.ChildItems.Add(ititCliV);

                divMenu.Items.Add(itCli);

                //PEDIDO
                MenuItem itPe = new MenuItem("Pedidos");

                MenuItem itPeE = new MenuItem("Ver");
                itPeE.Value = "PedidoVer";
                itPeE.NavigateUrl = "PedidoVer.aspx";
                itPe.ChildItems.Add(itPeE);

                if ((user.Perfil != Usuario.PerfilesEnum.JefeProduccion))
                {
                    MenuItem itPeN = new MenuItem("Nuevo");
                    itPeN.Value = "PedidoNuevo";
                    itPeN.NavigateUrl = "PedidoABM.aspx";
                    itPe.ChildItems.Add(itPeN);
                }

                divMenu.Items.Add(itPe);
            }

           
                //LOGISTICA PRODUCCION
                MenuItem itLog = new MenuItem("Logistica Producción");


                if ((user.Perfil == Usuario.PerfilesEnum.JefeProduccion) || (user.Perfil == Usuario.PerfilesEnum.Administrador))
                {
                    MenuItem itInsN = new MenuItem("Crear Plan de Producción");
                    itInsN.Value = "Crear";
                    itInsN.NavigateUrl = "LogisticaProduccion.aspx";
                    itLog.ChildItems.Add(itInsN);
                }
                if ((user.Perfil == Usuario.PerfilesEnum.JefeProduccion) || (user.Perfil == Usuario.PerfilesEnum.Operario) || (user.Perfil == Usuario.PerfilesEnum.Administrador))
                {
                    MenuItem itPlp = new MenuItem("Registrar producción");
                    itPlp.Value = "VerLogistica";
                    itPlp.NavigateUrl = "VerLogistica.aspx";
                    itLog.ChildItems.Add(itPlp);
                }

                divMenu.Items.Add(itLog);
           

            if ((user.Perfil == Usuario.PerfilesEnum.Administrador))
            {
                //USUARIO
                MenuItem itIns = new MenuItem("Usuario");

                MenuItem itPeE = new MenuItem("Ver");
                itPeE.Value = "Ver";
                itPeE.NavigateUrl = "UsuarioVer.aspx";
                itIns.ChildItems.Add(itPeE);

                MenuItem itInsN = new MenuItem("Nuevo");
                itInsN.Value = "Nuevo";
                itInsN.NavigateUrl = "UsuarioABM.aspx";
                itIns.ChildItems.Add(itInsN);


                divMenu.Items.Add(itIns);
            }

            if ((user.Perfil == Usuario.PerfilesEnum.JefeSuperior) || (user.Perfil == Usuario.PerfilesEnum.Administrador))
            {
                //REPORTES
                MenuItem itRepo = new MenuItem("Reportes");

                MenuItem itReRan = new MenuItem("Ranking de Ventas");
                itReRan.Value = "RankingVentas";
                itReRan.NavigateUrl = "ReporteRanking.aspx";
                itRepo.ChildItems.Add(itReRan);

                MenuItem itReEs = new MenuItem("Estimaciones");
                itReEs.Value = "Estimaciones";
                itReEs.NavigateUrl = "ReporteEstimaciones.aspx";
                itRepo.ChildItems.Add(itReEs);


                divMenu.Items.Add(itRepo);
            }

            MenuItem itSalir = new MenuItem("Logout");

            itSalir.Value = "Salir";
            itSalir.NavigateUrl = "Login.aspx";

            divMenu.Items.Add(itSalir);

           
            MenuItem ituser = new MenuItem(" " + user.NombreCompleto + " - " + user.Perfil.ToString());
            ituser.Value = "user";
            ituser.ImageUrl = "~/Images/user_info.png";
            ituser.Selectable = false;
            divMenu.Items.Add(ituser);
        }
    }
}
