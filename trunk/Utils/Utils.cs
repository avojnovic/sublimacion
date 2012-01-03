using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.UI.WebControls;
using System.Net.Mail;
using System.IO;
using System.Data;
using System.Text.RegularExpressions;


namespace Fwk.Utils
{
    public class Utils
    {
        public static void FillCboMes(DropDownList ddl)
        {
            Utils.FillCboNumbersRange(ddl, 1, 12, false, string.Empty);
        }
        public static void FillCboMes(DropDownList ddl, string selectedValue)
        {
            Utils.FillCboNumbersRange(ddl, 1, 12, false, selectedValue);
        }
        public static void FillCboAno(DropDownList ddl, int initYear, int endYear, bool desc)
        {
            Utils.FillCboNumbersRange(ddl, initYear, endYear, desc, string.Empty);
        }
        public static void FillCboAno(DropDownList ddl, int initYear, int endYear, bool desc, string selectedValue)
        {
            Utils.FillCboNumbersRange(ddl, initYear, endYear, desc, selectedValue);
        }
        public static void FillCboNumbersRange(DropDownList ddl, int bottom, int top, bool desc)
        {
            FillCboNumbersRange(ddl, bottom, top, desc, string.Empty);
        }
        public static void FillCboNumbersRange(DropDownList ddl, int bottom, int top, bool desc, string selectedValue)
        {
            ddl.Items.Clear();
            if (desc)
            {
                for (int i = top; i >= bottom; i--)
                {
                    ddl.Items.Add(new ListItem(i.ToString("00"), i.ToString("00")));
                }
            }
            else
            {
                for (int i = bottom; i <= top; i++)
                {
                    ddl.Items.Add(new ListItem(i.ToString("00"), i.ToString("00")));
                }
            }
            if (selectedValue.Length>0)
            {
                ddl.SelectedValue = selectedValue;
            }
        }
        public static void FillCboFromSource(DropDownList ddl, object Source, string IDName, string ValueName, bool BlankValue)
        {
            ddl.DataSource = Source;
            ddl.DataTextField = ValueName;
            ddl.DataValueField = IDName;
            ddl.DataBind();
            if (BlankValue)
            {
                ddl.Items.Insert(0, new ListItem(string.Empty, string.Empty));
            }
        }
        public static void FillCboFromSourceDefault(DropDownList ddl, object Source, bool BlankValue)
        {
            FillCboFromSource(ddl, Source, "id", "name", BlankValue);
        }

        public static DataTable ObjectListToDatatable(List<object> list)
        {
            DataTable dt = new DataTable();
            if (list != null)
            {
                if (list.Count > 0)
                {
                    object o = list[0];
                    Type ot = o.GetType();
                    System.Reflection.FieldInfo[] fieldInfo = ot.GetFields();
                    foreach (System.Reflection.FieldInfo fi in fieldInfo)
                    {
                        dt.Columns.Add(fi.Name);
                    }
                    foreach (var item in list)
                    {
                        DataRow dr = dt.NewRow();
                        Type t = item.GetType();
                        System.Reflection.FieldInfo[] fieldInfoItem = t.GetFields();
                        foreach (System.Reflection.FieldInfo fii in fieldInfoItem)
                        {
                            dr[fii.Name] = fii.GetValue(item);
                        }
                    }    
                }
            }
            return dt;
        }

        public static string DoubleToStringFormat(double value, int decimalLenght)
        {
            string rv = string.Empty;
            rv = value.ToString().Replace(',', '.');
            int lioComma = rv.LastIndexOf('.');
            if (decimalLenght > 0 )
            {
                string subNum = rv.Substring(lioComma + 1, rv.Length - lioComma - 1);
                if (decimalLenght < subNum.Length)
                {
                    if (lioComma > 0 && subNum.Length > decimalLenght - 1)
                    {
                        rv = rv.Remove(lioComma + 1 + decimalLenght);
                    }
                }
            }
            else
            {
                if (lioComma > 0)
                {
                    rv = rv.Substring(0, lioComma);
                }
            }
            return rv;
        }

        public static void SendEmail(string from, string to, string server, string body, string subject, string userCredential, string passwordCredential, string filePath, bool IsHtmlBody)
        {
            try
            {
                string systemMode = string.Empty;
                if (System.Web.HttpContext.Current != null)
                {
                    systemMode = System.Configuration.ConfigurationManager.AppSettings["AppMode"];
                }
                if (systemMode != null)
                {
                    if (systemMode.Length > 0 && systemMode.ToLower() == "testing")
                    {
                        to = System.Configuration.ConfigurationManager.AppSettings["TestingEmail"];
                    }
                }
                
                    MailAddress fromMail = new MailAddress(from);
                    MailAddress toMail = new MailAddress(to);
                    MailMessage msg = new MailMessage();
                    msg.From = fromMail;
                    msg.To.Add(toMail);
                    msg.IsBodyHtml = IsHtmlBody;
                    msg.Body = body;
                    msg.Subject = subject;
                    if (filePath.Length > 0)
                    {
                        Attachment attachment = new Attachment(filePath);
                        msg.Attachments.Add(attachment);
                    }
                    SmtpClient smtp = new SmtpClient(server);
                    smtp.Credentials = new System.Net.NetworkCredential(userCredential, passwordCredential);
                    smtp.Send(msg);
                
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        


        public static string EncodeCharacterUTF8(string value)
        {
            Regex match = new Regex("=..=..");
            MatchCollection results =  match.Matches(value);
            foreach (Match m in results)
            {
                switch (m.Value)
                {
                    case "=C3=B3":
                        value = value.Replace("=C3=B3", "ó");
                        break;
                    case "=C3=93":
                        value = value.Replace("=C3=93", "Ó");
                        break;
                    case "=C3=B1":
                        value = value.Replace("=C3=B1", "ñ");
                        break;
                    case "=C3=A1":
                        value = value.Replace("=C3=A1", "á");
                        break;
                    case "=C3=81":
                        value = value.Replace("=C3=81", "Á");
                        break;
                    case "=C3=A9":
                        value = value.Replace("=C3=A9", "é");
                        break;
                    case "=C3=89":
                        value = value.Replace("=C3=A9", "É");
                        break;
                    case "=C3=BA":
                        value = value.Replace("=C3=BA", "ú");
                        break;
                    case "=C3=9A":
                        value = value.Replace("=C3=9A", "Ú");
                        break;
                    case "=C3=AD":
                        value = value.Replace("=C3=AD", "í");
                        break;
                    case "=C3=8D":
                        value = value.Replace("=C3=8D", "Í");
                        break;
                    default:
                        break;
                }
            }
            value = value.Replace("=20", "");
            value = value.Replace("=5F", "_");
            value = value.Replace("=3D", "=");
            value = value.Replace("=\r\n", "");
            return value;
        }
        public static string EncodeCharacterISO88591(string value)
        {
            value = value.Replace("iso-8859-1", "");
            Regex match = new Regex("=..");
            MatchCollection results = match.Matches(value);
            foreach (Match m in results)
            {
                switch (m.Value)
                {
                    case "=F3":
                        value = value.Replace("=F3", "ó");
                        break;
                    case "=F1":
                        value = value.Replace("=F1", "ñ");
                        break;
                    case "=E1":
                        value = value.Replace("=E1", "á");
                        break;
                    case "=E9":
                        value = value.Replace("=E9", "é");
                        break;
                    case "=BA":
                        value = value.Replace("=BA", "ú");
                        break;
                    case "=ED":
                        value = value.Replace("=ED", "í");
                        break;
                    default:
                        break;
                }
            }
            value = value.Replace("=?", "");
            value = value.Replace("?=", "");
            value = value.Replace("?Q?", "");
            value = value.Replace("?q?", "");
            value = value.Replace("=20", "");
            value = value.Replace("=5F", "_");
            value = value.Replace("=3D", "=");
            
            return value;
        }

        public static string RemoveDangerousHTMLTags(string htmlString)
        {
            string result = string.Empty;
            string pattern = @"<.*script.*>";
            result = Regex.Replace(htmlString, pattern, string.Empty);
            pattern = @"</.*script.*>";
            result = Regex.Replace(result, pattern, string.Empty);
            return result;
        }

        public static string ConvertToBR(string value)
        {
            string rv = value.Replace("\r\n", "<br/>");
            rv = rv.Replace("\n", "<br/>");
            return rv;
        }

        public static int GetNetHS(DateTime? init, List<string> hsPerDay)
        {
            List<string> hsPerDaySorted = new List<string>();
            int firstDayIndex = 0;
            switch (init.Value.DayOfWeek)
            {
                case DayOfWeek.Sunday:
                    firstDayIndex = 0;
                    break;
                case DayOfWeek.Monday:
                    firstDayIndex = 1;
                    break;
                case DayOfWeek.Tuesday:
                    firstDayIndex = 2;
                    break;
                case DayOfWeek.Wednesday:
                    firstDayIndex = 3;
                    break;
                case DayOfWeek.Thursday:
                    firstDayIndex = 4;
                    break;
                case DayOfWeek.Friday:
                    firstDayIndex = 5;
                    break;
                case DayOfWeek.Saturday:
                    firstDayIndex = 6;
                    break;
            }
            for (int i = 0; i < 7; i++)
            {
                int value = firstDayIndex + i;
                if (value >= 7)
                {
                    value = (firstDayIndex + i) - 7;
                }
                hsPerDaySorted.Add(hsPerDay[value]);
            }
            int days = (int)(DateTime.Today.Subtract(init.Value)).TotalDays;
            int weekStep = 0;
            int countDays = 0;
            int totalHS = 0;
            if (days == 0)
            {
                if (DateTime.Today.Day == init.Value.Day)
                {
                    days = 1;
                }
                else
                {
                    days = 2;
                }
            }
            else if (days == 1)
            {
                days = 2;
            }
            for (int i = 0; i < days; i++)
            {
                bool isLast = false;
                bool isFirst = false;
                if (i == days - 1) { isLast = true; };
                if (i == 0) { isFirst = true; };
                int value = i - 7 * weekStep;
                totalHS += GetHoursForDay(init.Value, hsPerDaySorted[value], isLast, isFirst);
                countDays++;
                if (countDays == 7)
                {
                    countDays = 0;
                    weekStep++;
                }
            }
            if (totalHS > 0)
            {
                totalHS--;
            }
            return totalHS;
        }
        public static int GetHoursForDay(DateTime init, string period, bool isLast, bool isFirst)
        {
            int hours = 0;
            if (period.Length > 0)
            {
                string[] values = period.Split('-');
                string[] fromValues = values[0].Split(':');
                string[] toValues = values[1].Split(':');
                string fromHour = fromValues[0];
                string fromMinute = fromValues[0];
                string toHour = toValues[0];
                string toMinute = toValues[1];
                int year = DateTime.Today.Year;
                int month = DateTime.Today.Month;
                int day = DateTime.Today.Day;
                int hourTo = int.Parse(toHour);
                int minuteTo = int.Parse(toMinute);
                int hourFrom = int.Parse(fromHour);
                int minuteFrom = int.Parse(fromMinute);
                DateTime from;
                DateTime to;
                if (isFirst)
                {
                    from = new DateTime(year, month, day, init.Hour, init.Minute, 0);
                    to = new DateTime(year, month, day, hourTo, minuteTo, 0);
                    if (isLast)
                    {
                        to = DateTime.Now;
                    }
                }
                else if (isLast)
                {
                    from = new DateTime(year, month, day, hourFrom, minuteFrom, 0);
                    to = new DateTime(year, month, day, DateTime.Now.Hour, DateTime.Now.Minute, 0);
                }
                else
                {
                    from = new DateTime(year, month, day, hourFrom, minuteFrom, 0);
                    to = new DateTime(year, month, day, hourTo, minuteTo, 0);
                }
                hours = (int)(to.Subtract(from)).TotalHours + 1;
            }
            return hours;
        }
        public static int GetHoursForDay(DateTime init, string period)
        {
            bool isFirst = false;
            if (init.Year == DateTime.Today.Year && init.Month == DateTime.Today.Month && init.Day == DateTime.Today.Day)
            {
                isFirst = true;
            }
            int hours = 0;
            if (period.Length > 0)
            {
                string[] values = period.Split('-');
                string[] fromValues = values[0].Split(':');
                string[] toValues = values[1].Split(':');
                string fromHour = fromValues[0];
                string fromMinute = fromValues[0];
                string toHour = toValues[0];
                string toMinute = toValues[1];
                int year = DateTime.Today.Year;
                int month = DateTime.Today.Month;
                int day = DateTime.Today.Day;
                int hourTo = int.Parse(toHour);
                int minuteTo = int.Parse(toMinute);
                int hourFrom = int.Parse(fromHour);
                int minuteFrom = int.Parse(fromMinute);
                DateTime from;
                DateTime to;
                if (isFirst)
                {
                    from = new DateTime(year, month, day, init.Hour, init.Minute, 0);
                    to = new DateTime(year, month, day, hourTo, minuteTo, 0);
                }
                else
                {
                    from = new DateTime(year, month, day, hourFrom, minuteFrom, 0);
                    to = new DateTime(year, month, day, hourTo, minuteTo, 0);
                }
                hours = (int)(to.Subtract(from)).TotalHours + 1;
            }
            return hours;
        }

    }
}
