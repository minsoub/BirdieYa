#region ▶ Description & History
/* 
 * 프로그램ID : LogManager 
 * 프로그램명 : 어플리케이션 공통 로그 관리 클레스.

 * 설      명 : 웹어플리케이션의 공통 로그 처리를 위한 클레스 입니다. 
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
using System.Configuration;

namespace GSFramework.Common.Diagnostics
{
    /// <summary>
    /// 웹 어플리케이션 공통 로그 관리 클레스.
    /// </summary>
    /// <remarks></remarks>
    /// <example>
    /// 다음은 LogManager 사용 예제 입니다.
    /// <code>
    /// LogManager.WriteError(ex);
    /// LogManager.WriteInformation("Message {0}, {1}", "추가 정보", str1, str2);
    /// </code>
    /// </example>
    public class LogManager
    {
        const string LogTypeKey = "Log";
        const string LimitLogLevelKey = "LogLevel";


        static LogType m_SelectedLogType = LogType.None;
        static ILogger myLogger = null;

        static LogType ConfigLogType
        {
            get
            {
                return (LogType)Enum.Parse(typeof(LogType), (ConfigurationManager.AppSettings.Get(LogTypeKey) ?? "0"));
            }
        }

        static LogLevel LimitLogLevel
        {
            get
            {
				return (LogLevel)Enum.Parse(typeof(LogLevel), (ConfigurationManager.AppSettings.Get(LimitLogLevelKey) ?? "0"));
            }
        }

        static void CreateLogger()
        {
            if (ConfigLogType == LogType.None)
            {
                myLogger = null;
            }
            else if (ConfigLogType == LogType.File)
            {
                myLogger = new FileLogger();
            }
            else if (ConfigLogType == LogType.Db)
            {
                myLogger = new DBLogger();
            }

            m_SelectedLogType = ConfigLogType;
        }

        /// <summary>
        /// 어플리케이션 에러가 발생 했을 때 Exception 정보를 받아 로그를 기록한다.
        /// </summary>
        /// <param name="ex">예외 객체</param>
        public static void WriteError(Exception ex)
        {
            WriteError(ex, string.Empty);
        }

        /// <summary>
        /// 어플리케이션 에러가 발생 Exception정보와 추가 정보를 받아 로그를 기록 한다.
        /// </summary>
        /// <param name="ex">예외 개체</param>
        /// <param name="additionalInfo">추가 정보</param>
        public static void WriteError(Exception ex, string additionalInfo)
        {
            StringBuilder message = new StringBuilder(300);

            Exception tmpException = ex;
            while (tmpException != null)
            {
				message.Append(string.Format("≫ Exception Message : {0}", tmpException.Message));
                message.Append(Environment.NewLine);
				message.Append(string.Format("≫ Exception Type : {0}", ex.GetType()));
                message.Append(Environment.NewLine);
				message.Append("≫ Static Trace : ");
                message.Append(Environment.NewLine);
                message.Append(ex.StackTrace);
                message.Append(Environment.NewLine);
				message.Append("-----------------------------------------------------------------------");
                message.Append(Environment.NewLine);

                tmpException = tmpException.InnerException;
            }

            Write(LogLevel.Error, message.ToString(), additionalInfo, null);
        }


        /// <summary>
        /// 어플리케이션 흐름상 필요한 정보를 받아 로그를 기록 한다.
        /// </summary>
        /// <param name="infoMessage">로그 정보</param>
        /// <param name="messageArgs">InfoMessage에 필요한 인자</param>
        public static void WriteInfomation(string infoMessage, params object[] messageArgs)
        {
            WriteInfomation(infoMessage, string.Empty, messageArgs);
        }

        /// <summary>
        /// 어플리케이션 흐름상 필요한 정보를 받아 로그를 기록 한다.
        /// </summary>
        /// <param name="infoMessage">로그 정보</param>
        /// <param name="additionalInfo">추가 정보</param>
        /// <param name="messageArgs">InfoMessage에 필요한 인자</param>
        public static void WriteInfomation(string infoMessage, string additionalInfo, params object[] messageArgs)
        {
            Write(LogLevel.Information, infoMessage, additionalInfo, messageArgs);
        }

        /// <summary>
        /// 어플리케이션의 흐름상의 Warning 메세지를 로그에 기록 한다. 
        /// </summary>
        /// <param name="warningMessage">위험 메세지</param>
        /// <param name="messageArgs">메세지 인자 값</param>
        public static void WriteWarning(string warningMessage, params object[] messageArgs)
        {
            WriteWarning(warningMessage, string.Empty, messageArgs);
        }

        /// <summary>
        /// 어플리케이션의 흐름상의 Warning 메세지를 로그에 기록 한다. 
        /// </summary>
        /// <param name="warningMessage">위험 메세지</param>
        /// <param name="additionalInfo">추가 정보</param>
        /// <param name="messageArgs">메세지 인자 값</param>
        public static void WriteWarning(string warningMessage, string additionalInfo, params object[] messageArgs)
        {
            Write(LogLevel.Warning, warningMessage, additionalInfo, messageArgs);
        }

        /// <summary>
        /// 사용자 로그 기록
        /// </summary>
        /// <param name="logLevel">로그 위험 레벨</param>
        /// <param name="message">로그 메시지</param>
        /// <param name="additionalInfo">추가 정보</param>
        /// <param name="messageArgs">로그 메시지 인자값</param>
        public static void Write(LogLevel logLevel, string message, string additionalInfo, params object[] messageArgs)
        {
            if (logLevel < LimitLogLevel)
            {
                return;
            }

            if (m_SelectedLogType != ConfigLogType)
            {
                CreateLogger();
            }

            if (myLogger != null)
            {
                myLogger.Write(m_SelectedLogType, logLevel, message, additionalInfo, messageArgs);
            }
        }
    }

    /// <summary>
    /// 로그 타입.
    /// </summary>
    enum LogType
    {
        None = 0,
        File = 1, 
        Db = 2
    }

    /// <summary>
    /// 로그의 위험 순위 타입.
    /// </summary>
    public enum LogLevel
    {
        /// <summary>
        /// 제한 없음.
        /// </summary>
        None = 0,
        /// <summary>
        /// 일반 정보
        /// </summary>
        Information = 1,
        /// <summary>
        /// 경고
        /// </summary>
        Warning = 2,
        /// <summary>
        /// 에러
        /// </summary>
        Error =3
    }
}
