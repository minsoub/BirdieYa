using System;
using System.Collections.Generic;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

using SSFramework.Common;
using SSFramework.Data;
using SSFramework.Socket;
using System.Data;
using System.Collections;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;

namespace BirdieYa.pages
{
    public class CartInfo
    {
        public string cart_no { get; set; }
        public string cart_type { get; set; }
        public string caddie_name { get; set; }
        public string start_time { get; set; }
        public string member { get; set; }
    }

    public partial class cartinfo : WebBase
    {
        public Common comm = new Common();
        static QueryReader query = new QueryReader();

        /// <summary>
        /// 코스 정보
        /// </summary>
        string[] sCourse1 = { "5", "4", "4", "4", "3", "4", "3", "5", "4" };
        string[] sCourse2 = { "4", "3", "4", "5", "3", "5", "5", "3", "4" };
        string[] sCourse3 = { "5", "4", "4", "4", "5", "3", "4", "3", "4" };

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
            if (comm.CheckSession()) { Response.Redirect("../index.aspx"); }

            //OnSearch();
        }

        /// <summary>
        /// 코스 선택 이벤트
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        protected void btnSelCourse_Click(object sender, EventArgs e)
        {
            try
            {
                Console.WriteLine("btnSelCourse_Click called..");
                OnSearch();
            }
            catch (Exception ex)
            {
                this.Alert(string.Format("폼 로드 에러 : {0}", ex.Message));
            }
        }

        /// <summary>
        /// 관제 정보 조회
        /// </summary>
        protected void OnSearch()
        {
            string[] sTmpCourse = sCourse1;

            if (hdnSelCourse.Value == "1") { sTmpCourse = sCourse1; }
            if (hdnSelCourse.Value == "2") { sTmpCourse = sCourse2; }
            if (hdnSelCourse.Value == "3") { sTmpCourse = sCourse3; }

            for (int i = 0; i < sTmpCourse.Length; i++)
            {
                Literal ltrHole = (Literal)form1.Controls[1].FindControl("ltrHole" + (i + 1).ToString());

                DataTable dtHole = GetHolePosition((i + 1).ToString());

                ltrHole.Text = GetParHoleHtml((i + 1).ToString(), sTmpCourse[i], dtHole);
            }
            lblCartNo.Text = string.Empty;
            lblCartType.Text = string.Empty;
            lblCaddie.Text = string.Empty;
            lblStartTime.Text = string.Empty;
            lblMember.Text = string.Empty;
        }

        protected DataTable GetHolePosition(string sHole)
        {
            DataTable dt = new DataTable();

            try
            {
                string sSql = query.getQuery("Common.Cart2.Search");

                ArrayList array = new ArrayList();

                array.Add(new OracleParameter("course_no", hdnSelCourse.Value));
                array.Add(new OracleParameter("hole_no", sHole));

                dt = dbControl.GetDataTable(sSql, array);
            }
            catch (Exception)
            { 

            }

            return dt;
        }

        protected string GetParHoleHtml(string sHole, string sPar, DataTable dtPos)
        {
            string sHtml = string.Empty;
            string sBlock1 = string.Empty; string sBlock2 = string.Empty; string sBlock3 = string.Empty; string sBlock4 = string.Empty;

            foreach (DataRow row in dtPos.Rows)
            {
                if (row["BLOCK_NO"].ToString() == "1") { sBlock1 += GetCartHtml(row); }
                if (row["BLOCK_NO"].ToString() == "2") { sBlock2 += GetCartHtml(row); }
                if (row["BLOCK_NO"].ToString() == "3") { sBlock3 += GetCartHtml(row); }
                if (row["BLOCK_NO"].ToString() == "4") { sBlock4 += GetCartHtml(row); }
            }

            //sBlock1 += "<a href=\"javascript:void(0); javascript:SetSelDetail('1', '2', '3');\" class=\"detail-car\">";
            //sBlock1 += "    <div class=\"car-no car-6\">7</div>";
            //sBlock1 += "</a>";
            
            sHtml += string.Format("<div class=\"left-hole par-{0}\">{1}", sPar, sHole);
            sHtml += string.Format("    <div class=\"car-wrap\">");
            if (sPar == "3")
            {
                sHtml += string.Format("    <div class=\"car-first\">{0}</div>", sBlock1);
                sHtml += string.Format("    <div class=\"car-last\">{0}</div>", sBlock2);
            }
            else if (sPar == "4")
            {
                sHtml += string.Format("    <div class=\"car-first\">{0}</div>", sBlock1);
                sHtml += string.Format("    <div class=\"car-mid-1\">{0}</div>", sBlock2);
                sHtml += string.Format("    <div class=\"car-last\">{0}</div>", sBlock3);
            }
            else if (sPar == "5")
            {
                sHtml += string.Format("    <div class=\"car-first\">{0}</div>", sBlock1);
                sHtml += string.Format("    <div class=\"car-mid-1\">{0}</div>", sBlock2);
                sHtml += string.Format("    <div class=\"car-mid-2\">{0}</div>", sBlock3);
                sHtml += string.Format("    <div class=\"car-last\">{0}</div>", sBlock4);
            }
            sHtml += string.Format("    </div>");
            sHtml += string.Format("</div>");

            return sHtml;
        }

        protected string GetCartHtml(DataRow row)
        {
            string sHtml = string.Empty;

            sHtml += string.Format("<a href=\"javascript:void(0); javascript:SetSelDetail('{0}', '{1}', '{2}');\" class=\"detail-car\">", row["CART_NO"].ToString(), row["CART_TYPE"].ToString(), row["TEAM_NO"].ToString());
            sHtml += string.Format("    <div class=\"car-no car-{0}\">{1}</div>", row["CART_TYPE"].ToString(), row["CART_NO"].ToString());
            sHtml += string.Format("</a>");
            
            return sHtml;
        }

        [System.Web.Services.WebMethod]
        public static CartInfo GetCartDetail(string sCartNo, string sCartType, string sTeamNo)
        {
            ArrayList array = new ArrayList();
            ArrayList arrResult = new ArrayList();

            try
            {
                string sSql = query.getQuery("Common.Cart.Detail.Search");

                using (WebBase wBase = new WebBase())
                {
                    array.Add(new OracleParameter("cart_no", sCartNo));
                    array.Add(new OracleParameter("team_no", sTeamNo));

                    DataTable dt = wBase.dbControl.GetDataTable(sSql, array);

                    if (dt.Rows.Count > 0)
                    {
                        string sMember = string.Empty;
                        foreach (DataRow row in dt.Rows)
                        {
                            if (sMember != string.Empty) { sMember += ", "; }

                            sMember += row["VISIT_NAME"].ToString() + "(" + row["MEMBER_NAME"].ToString() + ")";
                        }

                        arrResult.Add(dt.Rows[0]["CART_NO"].ToString());
                        arrResult.Add(GetCartType(sCartType));
                        arrResult.Add(dt.Rows[0]["CADDIE_NAME"].ToString());
                        arrResult.Add(dt.Rows[0]["START_TIME"].ToString());
                        arrResult.Add(sMember);
                    }
                    else
                    {
                        arrResult.Add(sCartNo);
                        arrResult.Add(sTeamNo);
                        arrResult.Add(string.Empty);
                        arrResult.Add(string.Empty);
                        arrResult.Add(string.Empty);
                    }
                }
            }
            catch (Exception ex)
            {
                arrResult.Add(string.Empty);
                arrResult.Add(string.Empty);
                arrResult.Add(string.Empty);
                arrResult.Add(string.Empty);
                arrResult.Add(ex.Message);
            }

            CartInfo info = new CartInfo();
            info.cart_no = arrResult[0].ToString();
            info.cart_type = arrResult[1].ToString();
            info.caddie_name = arrResult[2].ToString();
            info.start_time = arrResult[3].ToString();
            info.member = arrResult[4].ToString();

            return info;
            //System.Web.Script.Serialization.JavaScriptSerializer serializer = new System.Web.Script.Serialization.JavaScriptSerializer();
            //return serializer.Serialize(arrResult);
            //return arrResult;
        }

        protected static string GetCartType(string sCartType)
        {
            string sTypeName = string.Empty;

            if (sCartType == "0")
            {
                sTypeName = "일반";
            }
            else if (sCartType == "1")
            {
                sTypeName = "첫팀";
            }
            else if (sCartType == "2")
            {
                sTypeName = "마지막팀";
            }
            else if (sCartType == "3")
            {
                sTypeName = "VIP";
            }
            else if (sCartType == "4")
            {
                sTypeName = "단체";
            }
            else if (sCartType == "5")
            {
                sTypeName = "교육";
            }
            else if (sCartType == "6")
            {
                sTypeName = "주의팀";
            }
            else if (sCartType == "7")
            {
                sTypeName = "9H추가";
            }
            else if (sCartType == "8")
            {
                sTypeName = "마샬";
            }
            else
            {
                sTypeName = string.Empty;
            }

            return sTypeName;
        }
    }
}