using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Drawing;
using System.Data;
using System.Configuration;
using SSFramework.Data;
using SSFramework.Common;
using System.Collections;
using System.Data.SqlClient;
//using System.Data.OracleClient;
using Oracle.ManagedDataAccess.Client;
using SSFramework.Socket;

namespace BirdieYa.pages
{
    public partial class pinposition : WebBase
    {
        public Common comm = new Common();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack) InitControls();
        }
        protected void InitControls()
        {
            if (comm.CheckSession()) { Response.Redirect("../index.aspx"); }

            SetImage(iCourse.Text, iHole.Text);
        }
        /**
         * 이미지지를 그린다
         **/
        protected void SetBtnImage(object sender, EventArgs e)
        {
            SetImage(iCourse.Text, iHole.Text);
        }

        // Image call
        private void SetImage(string sCourse, string sHole)
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

            GetPosition();

        }

        protected void GetPosition()
        {
            string sSql = @"SELECT * FROM PIN_INFO2 WHERE COURSE = :course AND HOLE = :hole AND GUBUN = :lrgb ";

            ArrayList array = new ArrayList();

            array.Add(new OracleParameter("course", iCourse.Text));

            //테스트 위한 홀 정보 변경
            //if (ddlCourse.SelectedValue == "O")
            //{
            array.Add(new OracleParameter("hole", iHole.Text));
            array.Add(new OracleParameter("lrgb", "0"));

            //}
            //else
            //{
            //    string sHole = (Convert.ToInt32(ddlHole.SelectedValue == string.Empty ? "0" : ddlHole.SelectedValue) + 9).ToString();

            //    array.Add(new OracleParameter("hole", sHole));
            //}

            DataTable dt = dbControl.GetDataTable(sSql, array);

            if (dt.Rows.Count > 0)
            {
                int iOriginXPos = Convert.ToInt32(dt.Rows[0]["PIN_X"].ToString());
                int iOriginYPos = Convert.ToInt32(dt.Rows[0]["PIN_Y"].ToString());

                double dWidth = Convert.ToInt32(hdnWidth.Value == string.Empty ? "0" : hdnWidth.Value);

                int iXPos = Convert.ToInt32(iOriginXPos); // * Math.Round((dWidth / 280) - 0.04, 1));
                int iYPos = Convert.ToInt32(iOriginYPos); //  * Math.Round((dWidth / 280) - 0.04, 1));

                hdnPosX.Value = (iXPos +10).ToString();
                hdnPosY.Value = (iYPos + 175).ToString();
            }
            else
            {
                hdnPosX.Value = "10";
                hdnPosY.Value = "175";
            }
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            string sSql = @"UPDATE PIN_INFO2 SET PIN_X=:pin_x, PIN_Y=:pin_y, GUBUN=:lrgb, LAST_UPDATE=SYSDATE
                             WHERE COURSE = :course AND HOLE = :hole ";

            ArrayList array = new ArrayList();

            int iOriginXPos = Convert.ToInt32(hdnPosX.Value) - 0;
            int iOriginYPos = Convert.ToInt32(hdnPosY.Value) - 110;

            double dWidth = Convert.ToInt32(hdnWidth.Value == string.Empty ? "0" : hdnWidth.Value);

            int iXPos = Convert.ToInt32(iOriginXPos * Math.Round((280 / dWidth) + 0.004, 2));
            int iYPos = Convert.ToInt32(iOriginYPos * Math.Round((280 / dWidth) + 0.004, 2));

            array.Add(new OracleParameter("pin_x", iXPos.ToString()));
            array.Add(new OracleParameter("pin_y", iYPos.ToString()));
            array.Add(new OracleParameter("lrgb", "0"));

            array.Add(new OracleParameter("course", iCourse.Text));
            array.Add(new OracleParameter("hole", iHole.Text));
            
           

            string sMessage = dbControl.Execute(sSql, array);

            if (sMessage != string.Empty)
            {
                this.Alert(sMessage.Replace("'", "''").Replace("\n", string.Empty));
            }
            else
            {
                onPinHistory(iXPos.ToString(), iYPos.ToString());

                SendSocket();
            }
        }

        protected void onPinHistory(string sXPos, string sYPos)
        {
            int Hole = 1;

            if (iCourse.Text == "A")
            {
                Hole = Convert.ToInt32(iHole.Text);
            }
            else if (iCourse.Text == "B")
            {
                Hole = 9 + Convert.ToInt32(iHole.Text);
            }
            else if (iCourse.Text == "C")
            {
                Hole = 18 + Convert.ToInt32(iHole.Text);
            }

            string sSql = @"INSERT INTO PIN_INFO_HISTORY (SEQ, VISIT_DATE, HOLE_NO, PIN_X, PIN_Y, GUBUN) 
                                                              VALUES (PIN_HISTORY_SEQ.NEXTVAL, TO_CHAR(SYSDATE,'YYYYMMDD'), :hole, :pin_x, :pin_y, :lrgb)";

            ArrayList array = new ArrayList();

            array.Add(new OracleParameter("hole", Hole.ToString()));
            array.Add(new OracleParameter("pin_x", sXPos));
            array.Add(new OracleParameter("pin_y", sYPos));
            array.Add(new OracleParameter("lrgb",  "0"));

            string sMessage = dbControl.Execute(sSql, array);

            //            string sSql = @"INSERT INTO PIN_INFO_HISTORY (SEQ, VISIT_DATE, COURSE, HOLE, PIN_X, PIN_Y, GUBUN) 
            //                                                  VALUES (PIN_HISTORY_SEQ.NEXTVAL, TO_CHAR(SYSDATE,'YYYYMMDD'), :course, :hole, :pin_x, :pin_y, :lrgb)";

            //            ArrayList array = new ArrayList();

            //            array.Add(new OracleParameter("course", ddlCourse.SelectedValue));
            //            array.Add(new OracleParameter("hole", ddlHole.SelectedValue));
            //            array.Add(new OracleParameter("pin_x", sXPos));
            //            array.Add(new OracleParameter("pin_y", sYPos));
            //            array.Add(new OracleParameter("lrgb", ddlLRgb.Enabled == true ? ddlLRgb.SelectedValue : "0"));

            //            string sMessage = dbControl.Execute(sSql, array);
        }

        protected void SendSocket()
        {
            PinSocket socket = new PinSocket();

            socket.ConnIP = ConfigurationSettings.AppSettings.Get("SocketIP");
            socket.ConnPort = Convert.ToInt32(ConfigurationSettings.AppSettings.Get("SocketPort"));

            socket.ServerConnect();

            string sSql = @"SELECT * FROM PIN_INFO2 WHERE COURSE = :course AND HOLE = :hole ";

            ArrayList array = new ArrayList();

            array.Add(new OracleParameter("course", iCourse.Text));
            array.Add(new OracleParameter("hole", iHole.Text));

            DataTable dt = dbControl.GetDataTable(sSql, array);

            socket.ServerSend(dt);
        }
    }
}