using System;
using System.Web;
using System.Collections.Generic;
using System.Text;

// 개발자 추가 using 구문
using System.Configuration;
using SSFramework.Common.Diagnostics;
using SSFramework.Data;
using System.Web.UI;
using System.Collections;
using System.Data;

namespace SSFramework.Common
{
    public class WebBase : System.Web.UI.Page
    {
        public ClientScriptManager csm;
        public Type pageType;
        public DbControl dbControl = new DbControl();
        /// <summary>
        /// Script Block 추가시 파라미터로 필요한 Page Type 개체
        /// </summary>

        #region -- Error 처리 --


        /// <summary>
        /// 예외처리를 합니다.
        /// </summary>
        /// <param name="ex">Exception 입니다.</param>
        /// <remarks>
        ///		<para>Real : Alert 로 간단한 메시지를 보여줍니다.</para>
        ///		<para>Dev : 전체 에러내용을 보여줍니다.</para>
        /// </remarks>
        public void AlertError(Exception ex)
        {
            this.AlertError(ex, true);
        }

        /// <summary>
        /// 예외처리를 합니다.
        /// </summary>
        /// <param name="ex">Exception 입니다.</param>
        /// <param name="msg">전달 될 메시지 입니다. 예외정보는 Skip 됩니다.</param>
        public void AlertError(Exception ex, string msg)
        {
            this.AlertError(ex, msg, true);
        }

        /// <summary>
        /// 예외처리를 합니다.
        /// </summary>
        /// <param name="ex">Exception 입니다.</param>
        /// <param name="msg">전달 될 메시지 입니다. 예외정보는 Skip 됩니다.</param>
        /// <param name="b_SaveErrorLog">예외를 저장할지 여부 입니다.</param>
        public void AlertError(Exception ex, string msg, Boolean b_SaveErrorLog)
        {
            this.Alert(msg);

            if (b_SaveErrorLog)
                LogManager.WriteError(ex);
        }

        /// <summary>
        /// 예외처리를 합니다.
        /// </summary>
        /// <param name="ex">Exception 입니다.</param>
        /// <param name="b_SaveErrorLog">예외를 저장할지 여부 입니다.</param>
        /// <remarks>
        ///		<para>Real : Alert 로 간단한 메시지를 보여줍니다.</para>
        ///		<para>Dev : 전체 에러내용을 보여줍니다.</para>
        /// </remarks>
        public void AlertError(Exception ex, Boolean b_SaveErrorLog)
        {
            if (b_SaveErrorLog)
                LogManager.WriteError(ex);

            //// 실서버 운영 모드 일 경우 Alert 로 간단한 메시지를 보여주고, 테스트나 개발모드일 경우 전체 에러내용을 보여주자.
            //if (ConfigurationManager.AppSettings.Get("OperationMode") == GlobalCode.OperationMode.Real.ToString())
                System.Web.HttpContext.Current.Response.Write(string.Format("<script language='javascript'> alert('Error : {0}') </script>", this.CheckMsg(ex.Message).Replace(Environment.NewLine, "\\n\\n")));
            //else
            //    System.Web.HttpContext.Current.Response.Write(ex.ToString());
        }

        #endregion

        public WebBase()
		{
			csm = this.Page.ClientScript;
			pageType = this.Page.GetType();
	    }

        #region -- Script MsgBox --

        /// <summary>
        /// JavaScript 를 통한 Alert 메시지를 띄워주는 메서드 입니다. 
        /// </summary>
        /// <param name="strMsg">전달될 메시지 입니다.</param>
        public void Alert(string strMsg)
        {
            string strScript = string.Format(@"alert('{0}');", this.CheckMsg(strMsg));
            csm.RegisterStartupScript(pageType, Guid.NewGuid().ToString(), strScript, true);
        }

        /// <summary>
        /// JavaScript 를 통한 이전페이지로 되돌아가게 해주는 메서드 입니다.
        /// </summary>
        public void Back()
        {
            string strScript = @"history.back();";
            csm.RegisterStartupScript(pageType, Guid.NewGuid().ToString(), strScript, true);
        }

        /// <summary>
        /// JavaScript 를 통한 Alert 메시지후 이전페이지로 되돌아가게 해주는 메서드 입니다.
        /// </summary>
        /// <param name="strMsg">전달될 메시지 입니다.</param>
        public void AlertNBack(string strMsg)
        {
            string strScript = string.Format(@"alert('{0}'); history.back();", this.CheckMsg(strMsg));
            csm.RegisterStartupScript(pageType, Guid.NewGuid().ToString(), strScript, true);
        }

        /// <summary>
        /// JavaScript 를 통한 Alert 메시지후 지정한 경로로 이동하게 해주는 메서드 입니다.
        /// </summary>
        /// <param name="strMsg">전달될 메시지 입니다.</param>
        /// <param name="strUrl">이동할 경로 입니다.</param>
        public void AlertNGo(string strMsg, string strUrl)
        {
            string strScript = string.Format(@"alert('{0}'); location.href = '{1}';", this.CheckMsg(strMsg), strUrl);
            csm.RegisterStartupScript(pageType, Guid.NewGuid().ToString(), strScript, true);
        }

        /// <summary>
        /// JavaScript 를 통한 Alert 메시지후 윈도우를 닫아주는 메서드 입니다.
        /// </summary>
        /// <param name="strMsg">전달될 메시지 입니다.</param>
        public void AlertNClose(string strMsg)
        {
            string strScript = string.Format(@"alert('{0}'); window.close();", this.CheckMsg(strMsg));
            csm.RegisterStartupScript(pageType, Guid.NewGuid().ToString(), strScript, true);
        }

        /// <summary>
        /// JavaScript 를 통한 Alert 메시지후 해당 스크립트를 실행합니다.
        /// </summary>
        /// <param name="strMsg">전달될 메시지 입니다.</param>
        /// <param name="strPostScript">실행할 스크립트 문장입니다.</param>
        public void AlertNScript(string strMsg, string strPostScript)
        {
            string strScript = string.Format(@"alert('{0}'); {1}", this.CheckMsg(strMsg), strPostScript);
            csm.RegisterStartupScript(pageType, Guid.NewGuid().ToString(), strScript, true);
        }

        /// <summary>
        /// >전달될 메시지에 대한 유효성 체크를 합니다.
        /// </summary>
        /// <param name="strMsg"></param>
        /// <returns></returns>
        public string CheckMsg(string strMsg)
        {
            string rtnMsg = "";
            if (!string.IsNullOrEmpty(strMsg))
            {
                rtnMsg = strMsg.Replace("'", "");
                rtnMsg = rtnMsg.Replace("(", "[");
                rtnMsg = rtnMsg.Replace(")", "]");
                rtnMsg = rtnMsg.Replace(";", "");
                rtnMsg = rtnMsg.Replace("\"", "");
            }
            else
                rtnMsg = "No Message !!";

            return rtnMsg;
        }

        #endregion
    }
}
