using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SSFramework.Common;
using SSFramework.Data;
using SSFramework.Socket;
using System.Data;

namespace BirdieYa
{
    public partial class index : WebBase
    {
        public Common comm = new Common();
        QueryReader query = new QueryReader();

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
            txtUserID.Text = comm.GetCookies("BirdieYa", "ID");
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            try
            {
                OnLogin();
            }
            catch (Exception ex)
            {
                this.Alert(string.Format("로그인 에러 : {0}", ex.Message));
            }
        }

        protected void OnLogin() 
        {
            DataTable dtUser = comm.GetUserInfo(txtUserID.Text);

            Console.WriteLine(txtUserID.Text);

            if (dtUser.Rows.Count > 0)
            {
                Console.WriteLine(dtUser.Rows[0]["PASSWORD"].ToString());
                if (dtUser.Rows[0]["PASSWORD"].ToString() == txtPassword.Text)
                {
                    comm.SetLogInfo(dtUser, true, true);

                    Response.Redirect("/pages/main.aspx");
                }
                else
                {
                    this.Alert("사용자 정보 또는 패스워드가 일치하지 않습니다.");
                }
            }
            else
           {
                this.Alert("사용자 정보 또는 패스워드가 일치하지 않습니다.");
            }
        }
    }
}