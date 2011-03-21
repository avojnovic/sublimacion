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
using System.Collections.Generic;
using sublimacion.BussinesObjects;
using sublimacion.DataAccessObjects.DataAccessObjects;
using sublimacion.BussinesObjects.BussinesObjects;

namespace sublimacion
{
    public partial class VerLogistica : System.Web.UI.Page
    {
        Usuario user;
        private Dictionary<long, BussinesObjects.BussinesObjects.Pedido> _dicPedidos;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                _dicPedidos = new Dictionary<long, sublimacion.BussinesObjects.BussinesObjects.Pedido>();
               
            }
            user = (Usuario)Session["usuario"];

            cargarGrilla();
            setearGrillaSiEstaVacia();
        }

        private void cargarGrilla()
        {

            Dictionary<long, PlanDeProduccion> _listaPlan = new Dictionary<long, PlanDeProduccion>();
            _dicPedidos = PedidoDAO.Instancia.obtenerTodosConIdPlanProd();
            foreach (BussinesObjects.BussinesObjects.Pedido ped in _dicPedidos.Values.ToList())
            {
                if (!_listaPlan.ContainsKey(ped.PlanDeProduccion.IdPlan))
                {
                    _listaPlan.Add(ped.PlanDeProduccion.IdPlan, ped.PlanDeProduccion);
                }
            }

            List <PlanDeProduccion> listP = _listaPlan.Values.ToList();
            listP.Sort(delegate(BussinesObjects.BussinesObjects.PlanDeProduccion p1, BussinesObjects.BussinesObjects.PlanDeProduccion p2)
            {
                return p1.Fecha_inicio.CompareTo(p2.Fecha_inicio);
            });


            GridViewPlanif.DataSource = listP;
            GridViewPlanif.DataBind();
        }
        private void setearGrillaSiEstaVacia()
        {
            if (GridViewPlanif.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("Id");
                dt.Columns.Add("Fecha_inicio_str");
                dt.Columns.Add("Fecha_fin_str");
  

                dt.Rows.Add(new object[] { "", "", ""});

                GridViewPlanif.DataSource = dt;
                GridViewPlanif.DataBind();
            }

           

        }

        protected void GridViewPlanif_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridViewPlanif.PageIndex = e.NewPageIndex;
            GridViewPlanif.DataBind();
        }
    }
}
