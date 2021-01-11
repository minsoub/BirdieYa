using System;
using System.Web;
using System.Data;

using System.Collections;
using System.IO;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;
using SSFramework.Common;
using SSFramework.Data;

namespace BirdieYa
{
    public class Common : WebBase
    {
        QueryReader query = new QueryReader();
        System.Web.HttpContext _context = System.Web.HttpContext.Current;

        /// <summary>
        /// 사용자 정보 세션 적용
        /// </summary>
        /// <param name="dt">사용자 정보</param>
        public void SetLogInfo(DataTable dt, Boolean bId, Boolean bPass)
        {
            try
            {
                if (dt.Rows.Count > 0)
                {
                    Session.Timeout = 180;

                    InitCookies("BirdieYa");

                    Session["USERID"] = dt.Rows[0]["USERID"].ToString();

                    if (bId == true) { SetCookies("BirdieYa", "ID", dt.Rows[0]["USERID"].ToString()); }
                }
            }
            catch (Exception ex)
            {
                this.AlertNBack(ex.Message);
            }
        }

        /// <summary>
        /// 세션 유지 여부 확인
        /// </summary>
        /// <returns>세션유지 여부</returns>
        public Boolean CheckSession()
        {
            try
            {
                if (Session["USERID"] == null || Session["USERID"].ToString() == string.Empty)
                {
                    return true;
                }
            }
            catch (Exception e)
            {
                return true;
            }

            return false;
        }

        /// <summary>
        /// 쿠키값 가져오기
        /// </summary>
        /// <param name="strCookieName">쿠키그룹명</param>
        /// <param name="strUnitName">쿠키명</param>
        /// <returns>쿠키값</returns>
        public string GetCookies(string strCookieName, string strUnitName)
        {
            HttpCookie cookieS = _context.Request.Cookies[strCookieName];

            string strReturnValue = "";

            if (cookieS != null)
                strReturnValue = cookieS.Values[strUnitName];

            return strReturnValue;
        }

        /// <summary>
        /// 쿠키값 설정
        /// </summary>
        /// <param name="strCookieName">쿠키그룹명</param>
        /// <param name="strUnitName">쿠키명</param>
        /// <param name="strValue">쿠키값</param>
        private void SetCookies(string strCookieName, string strUnitName, string strValue)
        {
            HttpCookie cookieR = _context.Response.Cookies[strCookieName];

            cookieR.Values.Add(strUnitName, strValue);

            if (strCookieName == "LoginInfo")
            {
                cookieR.Expires = DateTime.Now.AddDays(3);
            }
            else
            {
                cookieR.Expires = DateTime.Now.AddHours(3);
            }

            _context.Response.Cookies.Add(cookieR);
        }

        /// <summary>
        /// 쿠키값 초기화.
        /// </summary>
        /// <param name="strCookieName">쿠키그룹명</param>
        private void InitCookies(string strCookieName)
        {
            HttpCookie cookieR = new HttpCookie(strCookieName);
            cookieR.Expires = DateTime.Now.AddDays(-1);
            _context.Response.Cookies.Add(cookieR);
        }

        /// <summary>
        /// 사용자 정보 가져오기
        /// </summary>
        /// <param name="sUserId">사용자 ID</param>
        /// <returns>사용자 정보 DataTable</returns>
        public DataTable GetUserInfo(string sUserId)
        {
            string sSql = query.getQuery("Common.login.UserInfo.Search");

            ArrayList array = new ArrayList();

            array.Add(new OracleParameter("userid", sUserId));

            DataTable dt = dbControl.GetDataTable(sSql, array);

            return dt;
        }
    }
}