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
using Fwk.Utils;

namespace GestPro.controls
{
    public partial class DateComboPicker : System.Web.UI.UserControl
    {
        public DateTime? SelectedDate
        {
            get
            {
                try
                {
                    int day = 0;
                    int month = 0;
                    int year = 0;
                    DateTime? dt = null;
                    if (this.ddlDay.SelectedValue.Length > 0 && this.ddlMonth.SelectedValue.Length > 0 && this.ddlYear.SelectedValue.Length > 0)
                    {
                        day = int.Parse(this.ddlDay.SelectedValue);
                        month = int.Parse(this.ddlMonth.SelectedValue);
                        year = int.Parse(this.ddlYear.SelectedValue);
                         dt = new DateTime(year, month, day);
                    }
                    return dt;
                }
                catch
                {
                    return null;
                }
            }
        }


        public void setDate(DateTime? date)
        {
            this.ddlDay.SelectedValue = date.Value.Day.ToString();
             this.ddlMonth.SelectedValue=date.Value.Month.ToString();
             this.ddlYear.SelectedValue = date.Value.Year.ToString();
            
        }
        protected void Page_Load(object sender, EventArgs e)
        {
            Utils.FillCboNumbersRange(this.ddlDay, 1, 31, false);
            this.ddlDay.Items.Insert(0, new ListItem("--", string.Empty));
            this.ddlDay.SelectedValue = string.Empty;
            Utils.FillCboMes(this.ddlMonth);
            this.ddlMonth.Items.Insert(0, new ListItem("--", string.Empty));
            this.ddlMonth.SelectedValue = string.Empty;
            Utils.FillCboAno(this.ddlYear, DateTime.Today.Year, DateTime.Today.Year +1, true);
            this.ddlYear.Items.Insert(0, new ListItem("----", string.Empty));
            this.ddlYear.SelectedValue = string.Empty;
        }
    }
}