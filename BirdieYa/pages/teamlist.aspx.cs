using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

using SSFramework.Common;
using SSFramework.Data;
using SSFramework.Socket;
using System.Data;
using System.Collections;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;

namespace BirdieYa.pages
{
    public partial class teamlist : WebBase
    {
        public Common comm = new Common();
        static QueryReader query = new QueryReader();

        /// <summary>
        /// 폼로드 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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

        /// <summary>
        /// 페이지 초기 설정
        /// </summary>
        protected void InitControls()
        {
            if (comm.CheckSession()) { Response.Redirect("/index.aspx"); }

            OnSearch();
        }

        /// <summary>
        /// 캐디 정보 조회
        /// </summary>
        protected void OnSearch()
        {
            string sSql = query.getQuery("Common.Caddy3.Search");

            DataTable dt = dbControl.GetDataTable(sSql, new ArrayList());

            rptList.DataSource = dt;
            rptList.DataBind();

            lblTotal.Text = dt.Rows.Count.ToString();
        }

        /// <summary>
        /// Repeater ItemDataBound
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void rptList_ItemDataBound(object sender, RepeaterItemEventArgs e)
        {
            Literal ltrCaddie = (Literal)e.Item.FindControl("ltrCaddie");

            DataRowView row = (DataRowView)e.Item.DataItem;

            ltrCaddie.Text = string.Empty;
            ltrCaddie.Text += string.Format("<tr>");
            ltrCaddie.Text += string.Format("    <td>{0}</td>", Convert.ToInt32(row["TEAM_NO"].ToString().Substring(1, 3)).ToString());
            ltrCaddie.Text += string.Format("    <td>{0}</td>", row["VISIT_NAME"].ToString());
            ltrCaddie.Text += string.Format("    <td>{0}</td>", row["START_TIME"].ToString());
            ltrCaddie.Text += string.Format("    <td>{0}</td>", row["COURSE_NAME"].ToString());
            ltrCaddie.Text += string.Format("    <td>{0}({1})</td>", row["CADDIE_NAME"].ToString(), row["CART_NO"].ToString());

            string sSql = query.getQuery("Common.Caddy3.Detail.Search");

            ArrayList array = new ArrayList();

            array.Add(new OracleParameter("team_no", row["TEAM_NO"].ToString()));

            DataTable dt = dbControl.GetDataTable(sSql, array);

            if (dt.Rows.Count > 0)
            {
                ltrCaddie.Text += "<td align=\"left\">";

                int iCnt = 0;

                foreach (DataRow rowDet in dt.Rows)
                {
                    if (row["VISIT_NAME"].ToString() != rowDet["VISIT_NAME"].ToString())
                    {
                        if (iCnt == 2) { ltrCaddie.Text += "<br />"; } else if (iCnt > 0) { ltrCaddie.Text += "&nbsp;"; }

                        ltrCaddie.Text += rowDet["VISIT_NAME"].ToString();

                        iCnt++;
                    }
                }

                ltrCaddie.Text += "</td>";
            }
            ltrCaddie.Text += "</tr>";
        }
    }
}