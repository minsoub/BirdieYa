#region ▶ Description & History
/* 
 * 프로그램ID : LogManager 
 * 프로그램명 : Logger 클레스의 인터페이스.

 * 설      명 : Logger 클레스 생성시 사용되는 인터페이스.
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
    /// Logger 클레스의 인터페이스.
    /// </summary>
    internal interface ILogger : IDisposable
    {
        void Write(LogType m_SelectedLogType, LogLevel logLevel, string message, string additionalInfo, object[] messageArgs);
    }
}
