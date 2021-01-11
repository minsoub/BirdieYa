#region ▶ Description & History
/* 
 * 프로그램ID : DBLogger
 * 프로그램명 : 로그 기록을 Database에 하는 클래스

 * 설      명 : Configuration/AppSettings의 키값 Log의 값이 "2"인 경우 DbLogger를 생성하여 해당 Database에 기록 한다.
 * 최초작성자 : 정성균

 * 최초작성일 : 2007-07-18
 * 최종수정자 : 정성균

 * 최종수정일 : 2007-07-18
 * 수정  내용 :
 *				날짜			작성자		이슈
 *				---------------------------------------------------------------------------------------------
 *				2007-07-18		정성균		최초 개발.
 *				
 * 
*/
#endregion   
using System;
using System.Collections.Generic;
using System.Text;

namespace SSFramework.Common.Diagnostics
{
    /// <summary>
    /// 로그 기록을 Database에 하는 클래스
    /// </summary>
    internal class DBLogger : ILogger
    {
        // TODO : Log Schema 정의 되어야 함.

        /// <summary>
        /// 
        /// </summary>
        public DBLogger()
        {
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="connectionString"></param>
        public DBLogger(string connectionString)
        {
        }

        #region ILogger Members

        public void Write(LogType m_SelectedLogType, LogLevel logLevel, string message, string additionalInfo, object[] messageArgs)
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion

        #region IDisposable Members

        public void Dispose()
        {
            throw new Exception("The method or operation is not implemented.");
        }

        #endregion
    }
}
