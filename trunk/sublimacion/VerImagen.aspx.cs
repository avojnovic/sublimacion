using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace sublimacion
{
    public partial class VerImagen : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            string imagen = Request.QueryString["Imagen"];

            if (imagen != null && imagen.Trim() != "")
            {
                ImgImagen.ImageUrl = "Data\\" + imagen;
            }
                
        }
    }
}