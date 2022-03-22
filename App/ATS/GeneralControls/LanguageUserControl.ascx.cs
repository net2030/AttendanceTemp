using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace WebApplication1.UserControl
{
    public partial class LanguageUserControl : System.Web.UI.UserControl
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            { 
                HttpCookie cookie = Request.Cookies["CurrentLanguage"];
                if (cookie != null && cookie.Value != null)
                {
                    if (cookie.Value.IndexOf("en-") >= 0)
                    {
                        ImgBtn_En.Enabled = false;
                        ImgBtn_Ar.Enabled = true;
                    }
                    else
                    {
                        ImgBtn_En.Enabled = true;
                        ImgBtn_Ar.Enabled = false;
                    }
                }
            }
        }
        protected void ImgBtn_En_Click(object sender, ImageClickEventArgs e)
        {
            Session["Lang"] = "en";
            HttpCookie cookie = new HttpCookie("CurrentLanguage");
            cookie.Value = "en-US";
            cookie.Expires = DateTime.Now.AddMonths(6);
            Response.SetCookie(cookie);
            Response.Redirect(Request.RawUrl);
           
        }
        protected void ImgBtn_Fr_Click(object sender, ImageClickEventArgs e)
        {
            Session["Lang"] = "ar";
            HttpCookie cookie = new HttpCookie("CurrentLanguage");
            cookie.Value = "ar-SA";
            cookie.Expires = DateTime.Now.AddMonths(6);
            Response.SetCookie(cookie);
            Response.Redirect(Request.RawUrl);
            
        }
    }
}