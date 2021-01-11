#region ▶ Description & History
/* 
 * 프로그램ID : FileLogger
 * 프로그램명 : 로그를 지정된 폴더하위에 파일를 생성 후 작성하는 클래스
 * 설      명 : Configuration/AppSettings의 키값 Log의 값이 "1"인 경우 키값 "FileLogDirecotory"에 지정된 위치에 일자별 파일을 생성 후 작성 한다. 
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
using System.IO;

namespace GSFramework.Common.Diagnostics
{
    /// <summary>
    /// 로그를 지정된 폴더하위에 파일를 생성 후 작성하는 클래스
    /// </summary>
    internal class FileLogger : ILogger
    {
        private const string LogFileDirectoryKey = "LogDirectory";
        private const string LogFilePrefixKey = "LogPrefix";

        internal string LogFileDirectory
        {
            get
            {
                return ConfigurationManager.AppSettings.Get(LogFileDirectoryKey);
            }
        }

        internal string LogFilePrefix
        {
            get
            {
                return ConfigurationManager.AppSettings.Get(LogFilePrefixKey);
            }
        }


        #region ILogger Members

        public void Write(LogType m_SelectedLogType, LogLevel logLevel, string message, string additionalInfo, object[] messageArgs)
        {
            StreamWriter writer = null;

            try
            {
                if (!Directory.Exists(LogFileDirectory))
                {
                    Directory.CreateDirectory(LogFileDirectory);
                }

                if (messageArgs != null)
                {
                    message = String.Format(message, messageArgs);
                }
                string path = String.Format("{0}\\{1}_{2}.log", LogFileDirectory, LogFilePrefix, DateTime.Now.ToString("yyyyMMdd"));
                string timeStr = String.Format("{0}.{1:000}", DateTime.Now.ToString("HH:mm:ss"), DateTime.Now.Millisecond);
                string newLine = Environment.NewLine;
				string msg = String.Format("≫ {0} : {1} {3}{2} {3}≫ AdditionalInfo : {3} {4} {3}", logLevel.ToString(), timeStr, message, newLine, additionalInfo, newLine);
               
                FileStream stream = new FileStream(path, FileMode.Append, FileAccess.Write, FileShare.ReadWrite);
                writer = new StreamWriter(stream);

				writer.WriteLine("*****************************************************************************");
				writer.WriteLine("▶ " + DateTime.Now.ToString());
				writer.WriteLine("*****************************************************************************");

				writer.WriteLine(msg);
                writer.Close();
            }
            finally
            {
                if (writer != null)
                    writer.Close();
            }
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
