using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SSFramework.Common;
using SSFramework.Data;
using SSFramework.Socket;
using System.Data;

namespace BirdieYa.pages
{
    public partial class control : WebBase
    {
        public Common comm = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!IsPostBack) InitControls();
            }
            catch (Exception ex)
            {
                this.Alert(string.Format("폼 로드 에러 : {0}", ex.Message));
            }
        }

        protected void InitControls()
        {
            //if (comm.CheckSession()) { Response.Redirect("/login.aspx"); }

            lblUser.Text = Session["USERID"] == null ? string.Empty : Session["USERID"].ToString();
        }
    }
}