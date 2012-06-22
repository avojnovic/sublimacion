﻿using System;
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
using sublimacion.DataAccessObjects.DataAccessObjects;

namespace sublimacion
{
    public partial class InsumoVer : System.Web.UI.Page
    {

        private Dictionary<long, BussinesObjects.Insumo> _dicInsumo;

        protected void Page_Load(object sender, EventArgs e)
        {
            cargarGrilla();
            setearGrillaSiEstaVacia();
        }

        private void cargarGrilla()
        {
            _dicInsumo = InsumoDAO.obtenerInsumoTodos();

            GridView1.DataSource = _dicInsumo.Values.ToList();
            GridView1.DataBind();
        }


        private void setearGrillaSiEstaVacia()
        {

            if (GridView1.Rows.Count == 0)
            {
                DataTable dt = new DataTable();
                dt.Columns.Add("IdInsumoStr");
                dt.Columns.Add("Nombre");
                dt.Columns.Add("NombreFab");
                dt.Columns.Add("CostoStr");
                dt.Columns.Add("StockStr");
                dt.Columns.Add("FechaActStr");
                


                dt.Rows.Add(new object[] { "", "", "", "", "", ""});

                GridView1.DataSource = dt;
                GridView1.DataBind();
            }

        }

        protected void BtnEditar_Click(object sender, EventArgs e)
        {

            GridViewRow row = GridView1.SelectedRow;
            if (row != null)
            {
              
                long id = long.Parse(row.Cells[1].Text);
                Response.Redirect("Insumo.aspx?id=" + id.ToString());
           }
        }

        protected void BtnNuevo_Click(object sender, EventArgs e)
        {

            Response.Redirect("Insumo.aspx");
        }

       

        protected void GridView1_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            GridView1.PageIndex = e.NewPageIndex;
            GridView1.DataBind();
        }
    }
}
