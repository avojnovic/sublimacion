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

namespace GestPro.controls
{
    public partial class DatePicker : System.Web.UI.UserControl
    {
        public DateTime? SelectedDate 
            {
                get
                {
                    int day = 0;
                    int month = 0;
                    int year = 0;
                    if (this.txtDay.Text.Length > 0)
                    {
                        day = int.Parse(this.txtDay.Text);
                        if (this.txtMonth.Text.Length > 0)
                        {
                            month = int.Parse(this.txtMonth.Text);
                            if (this.txtYear.Text.Length > 0)
                            {
                                year = int.Parse(this.txtYear.Text);
                                try
                                {
                                    DateTime dt = new DateTime(year, month, day);
                                    return dt;
                                }
                                catch
                                {
                                    return null;
                                }
                            }
                            else
                            {
                                return null;
                            }
                        }
                        else
                        {
                            return null;
                        }
                    }
                    else
                    {
                        return null;
                    }
                }
            }



        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                this.pnlCalendar.Visible = false;
            }
        }

        public void setDate(DateTime? date)
        {
            if (date != null)
            {
                this.txtDay.Text = date.Value.Day.ToString();
                this.txtMonth.Text = date.Value.Month.ToString();
                this.txtYear.Text = date.Value.Year.ToString();
            }
            else
            {
                this.txtDay.Text = "";
                this.txtMonth.Text = "";
                this.txtYear.Text = "";
            }   
        }

        protected void btnShowCalendar_Click(object sender, EventArgs e)
        {
            this.pnlCalendar.Visible = !this.pnlCalendar.Visible;
        }

        protected void lnkClear_Click(object sender, EventArgs e)
        {
            this.txtDay.Text = string.Empty;
            this.txtMonth.Text = string.Empty;
            this.txtYear.Text = string.Empty;
            this.pnlCalendar.Visible = false;
        }

        protected void calDate_OnSelectionChanged(object sender, EventArgs e)
        {
            this.txtDay.Text = ((Calendar)sender).SelectedDate.Day.ToString("00");
            this.txtMonth.Text = ((Calendar)sender).SelectedDate.Month.ToString("00");
            this.txtYear.Text = ((Calendar)sender).SelectedDate.Year.ToString("0000");
            this.pnlCalendar.Visible = false;
        }
    }
}