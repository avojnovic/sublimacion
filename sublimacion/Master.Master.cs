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
                generarMenu();
            }
        }



        private void generarMenu()
        {

            if ((user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.Diseniador) || (user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.Administrador) )
            {
                MenuItem itCa = new MenuItem("Catalogos");


                MenuItem itn = new MenuItem("Nuevo");
                itn.Value = "CatalogoNuevo";
                itn.NavigateUrl = "Catalogo.aspx";
                itCa.ChildItems.Add(itn);

                MenuItem ite = new MenuItem("Editar");
                ite.Value = "CatalogoEditar";
                ite.NavigateUrl = "Catalogo.aspx";
                itCa.ChildItems.Add(ite);

                MenuItem itd = new MenuItem("Eliminar");
                itd.Value = "CatalogoEliminar";
                itd.NavigateUrl = "Catalogo.aspx";
                itCa.ChildItems.Add(itd);

                Menu1.Items.Add(itCa);


                MenuItem itPla = new MenuItem("Plantillas");


                MenuItem itPlaN = new MenuItem("Nuevo");
                itPlaN.Value = "PlantillaNuevo";
                itPlaN.NavigateUrl = "Plantilla.aspx";
                itPla.ChildItems.Add(itPlaN);

                MenuItem itPlaE = new MenuItem("Editar");
                itPlaE.Value = "PlantillaEditar";
                itPlaE.NavigateUrl = "Plantilla.aspx";
                itPla.ChildItems.Add(itPlaE);

                MenuItem itPlaD = new MenuItem("Eliminar");
                itPlaD.Value = "PlantillaEliminar";
                itPlaD.NavigateUrl = "Plantilla.aspx";
                itPla.ChildItems.Add(itPlaD);

                Menu1.Items.Add(itPla);

                MenuItem itReg = new MenuItem("Registrar Diseño");

                itReg.Value = "RegistrarDisenio";
                itReg.NavigateUrl = "RegistrarDisenio.aspx";

                Menu1.Items.Add(itReg);

            }

            if ((user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.Vendedor) || (user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.Administrador) )
            {
                //INSUMO
                MenuItem itIns = new MenuItem("Insumos");

                MenuItem itInsN = new MenuItem("Nuevo");
                itInsN.Value = "InsumoNuevo";
                itInsN.NavigateUrl = "Insumo.aspx";
                itIns.ChildItems.Add(itInsN);

                MenuItem itInsE = new MenuItem("Ver");
                itInsE.Value = "InsumoVer";
                itInsE.NavigateUrl = "InsumoVer.aspx";
                itIns.ChildItems.Add(itInsE);

                //MenuItem itInsE = new MenuItem("Editar");
                //itInsE.Value = "InsumoEditar";
                //itInsE.NavigateUrl = "Insumo.aspx";
                //itIns.ChildItems.Add(itInsE);

                //MenuItem itInsD = new MenuItem("Eliminar");
                //itInsD.Value = "InsumoEliminar";
                //itInsD.NavigateUrl = "Insumo.aspx";
                //itIns.ChildItems.Add(itInsD);

                Menu1.Items.Add(itIns);

                //PRODUCTO
                MenuItem itPro = new MenuItem("Productos");

                MenuItem itProN = new MenuItem("Nuevo");
                itProN.Value = "ProductoNuevo";
                itProN.NavigateUrl = "Producto.aspx";
                itPro.ChildItems.Add(itProN);

                MenuItem itProE = new MenuItem("Editar");
                itProE.Value = "ProductoEditar";
                itProE.NavigateUrl = "Producto.aspx";
                itPro.ChildItems.Add(itProE);

                MenuItem itProD = new MenuItem("Eliminar");
                itProD.Value = "ProductoEliminar";
                itProD.NavigateUrl = "Producto.aspx";
                itPro.ChildItems.Add(itProD);

                Menu1.Items.Add(itPro);

                //DESCUENTO
                MenuItem itDe = new MenuItem("Descuentos");

                MenuItem itDeN = new MenuItem("Nuevo");
                itDeN.Value = "DescuentoNuevo";
                itDeN.NavigateUrl = "Descuento.aspx";
                itDe.ChildItems.Add(itDeN);

                MenuItem itDeE = new MenuItem("Editar");
                itDeE.Value = "DescuentoEditar";
                itDeE.NavigateUrl = "Descuento.aspx";
                itDe.ChildItems.Add(itDeE);

                MenuItem itDeD = new MenuItem("Eliminar");
                itDeD.Value = "DescuentoEliminar";
                itDeD.NavigateUrl = "Descuento.aspx";
                itDe.ChildItems.Add(itDeD);


                Menu1.Items.Add(itDe);

               


                //ACEPTAR DISENIO
                MenuItem itAcep = new MenuItem("Aceptar Diseño");

                itAcep.Value = "AceptarDisenio";
                itAcep.NavigateUrl = "AceptarDisenio.aspx";

                Menu1.Items.Add(itAcep);


            }

            if ((user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.Vendedor) || (user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.Administrador) || (user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.JefeProduccion))
            {
             //PEDIDO
                MenuItem itPe = new MenuItem("Pedidos");

                if ((user.Perfil != sublimacion.BussinesObjects.Usuario.PerfilesEnum.JefeProduccion))
                {
                    MenuItem itPeN = new MenuItem("Nuevo");
                    itPeN.Value = "PedidoNuevo";
                    itPeN.NavigateUrl = "Pedido.aspx";
                    itPe.ChildItems.Add(itPeN);
                }

                MenuItem itPeE = new MenuItem("Ver");
                itPeE.Value = "PedidoVer";
                itPeE.NavigateUrl = "PedidoVer.aspx";
                itPe.ChildItems.Add(itPeE);

                //MenuItem itPeD = new MenuItem("Eliminar");
                //itPeD.Value = "PedidoEliminar";
                //itPeD.NavigateUrl = "Pedido.aspx";
                //itPe.ChildItems.Add(itPeD);

                Menu1.Items.Add(itPe);
            }

            if ((user.Perfil == sublimacion.BussinesObjects.Usuario.PerfilesEnum.JefeProduccion))
            {
                
                MenuItem itIns = new MenuItem("Logistica Producción");

                MenuItem itInsN = new MenuItem("Crear");
                itInsN.Value = "Crear";
                itInsN.NavigateUrl = "LogisticaProduccion.aspx";
                itIns.ChildItems.Add(itInsN);

                MenuItem itPeE = new MenuItem("Ver");
                itPeE.Value = "VerLogistica";
                itPeE.NavigateUrl = "VerLogistica.aspx";
                itIns.ChildItems.Add(itPeE);

                Menu1.Items.Add(itIns);
            }






            MenuItem itSalir = new MenuItem("Logout");

            itSalir.Value = "Salir";
            itSalir.NavigateUrl = "Login.aspx";

            Menu1.Items.Add(itSalir);

        }
    }
}
