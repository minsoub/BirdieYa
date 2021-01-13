using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Collections;
using System.Configuration;
using System.Data.Common;
using System.Data.SqlClient;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;
using SSFramework.Data;
using SSFramework.Common;
using SSFramework.Socket;

namespace BirdieYa.pages
{
    public partial class pin_search : WebBase
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                txtDate1.Text = DateTime.Today.AddMonths(-1).ToString("yyyy-MM-dd");
                txtDate2.Text = DateTime.Today.ToString("yyyy-MM-dd");

                //SetImage(iCourse.Text, iHole.Text);
            }
        }
        protected void SetImage(string sCourse, string sHole)
        {
            int iHole = 1;
            if (sCourse == "A")
            {
                iHole = Convert.ToInt32(sHole);
            }
            else if (sCourse == "B")
            {
                iHole = 9 + Convert.ToInt32(sHole);
            }
            else if (sCourse == "C")
            {
                iHole = 18 + Convert.ToInt32(sHole);
            }
            imgHole.ImageUrl = "../images/holes/green_" + iHole.ToString() + ".bmp";

            GetPosition(iHole);
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            //GetPosition();
            SetImage(iCourse.Text, iHole.Text);
        }

        protected void GetPosition(int iHole)
        {
            string sDate1 = txtDate1.Text == string.Empty ? "19000101" : txtDate1.Text.Trim().Replace("-", string.Empty);
            string sDate2 = txtDate2.Text == string.Empty ? "29000101" : txtDate2.Text.Trim().Replace("-", string.Empty);

            string sSql = @"SELECT * FROM PIN_INFO_HISTORY WHERE HOLE_NO = :hole AND VISIT_DATE BETWEEN :date1 AND :date2 ";

            ArrayList array = new ArrayList();

            array.Add(new OracleParameter("hole", iHole.ToString()));
            array.Add(new OracleParameter("date1", sDate1));
            array.Add(new OracleParameter("date2", sDate2));

            DataTable dt = dbControl.GetDataTable(sSql, array);

            rptList.DataSource = dt;
            rptList.DataBind();
        }

        protected void rptList_ItemDataBound(object sender, System.Web.UI.WebControls.RepeaterItemEventArgs e)
        {
            Literal ltrPosition = (Literal)e.Item.FindControl("ltrPosition");

            DataRowView row = (DataRowView)e.Item.DataItem;

            int iOriginXPos = Convert.ToInt32(row["PIN_X"].ToString());
            int iOriginYPos = Convert.ToInt32(row["PIN_Y"].ToString());

            double dWidth = Convert.ToInt32(hdnWidth.Value == string.Empty ? "0" : hdnWidth.Value);

            int iXPos = Convert.ToInt32(iOriginXPos * Math.Round((dWidth / 280) - 0.04, 1));
            int iYPos = Convert.ToInt32(iOriginYPos * Math.Round((dWidth / 280) - 0.04, 1));

            ltrPosition.Text += "<div id=\"hole\" style=\"position:absolute; top:" + (iYPos + 140).ToString() + "px; left:" + (iXPos + 0).ToString() + "px;\"><font color=\"Red\">●</div>";
        }
    }
}