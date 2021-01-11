using System;
using System.Collections.Generic;
using System.Data;
using System.Text;
using System.Net;
using System.Net.Sockets;
using System.Runtime.InteropServices;

namespace SSFramework.Socket
{
    public class PinSocket
    {
        string _ip;
        int _port;
        int _sendChk;
        System.Net.Sockets.Socket _client;

        public string ConnIP
        {
            get
            {
                return _ip;
            }
            set
            {
                _ip = value;
            }
        }

        public int ConnPort
        {
            get
            {
                return _port;
            }
            set
            {
                _port = value;
            }
        }

        public string ServerConnect()
        {
            try
            {
                _client = new System.Net.Sockets.Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

                if (!_client.Connected)
                {
                    //_client.Close();
                    
                    IPEndPoint _ipep = new IPEndPoint(IPAddress.Parse(_ip), _port);

                    _client.Connect(_ipep);

                    if (_client.Connected)
                    {
                        TempPacket tmpPacket = new TempPacket();
                        string smsg = "12345678900987654321";// textBox1.Text;

                        tmpPacket.Command = 0x40;
                        smsg = smsg.PadRight(100, ' ');
                        tmpPacket.Length = (ushort)(Encoding.Default.GetBytes(smsg).Length + 2);
                        tmpPacket.Data = Encoding.Default.GetBytes(smsg);

                        //보내기                     
                        byte[] buffer = new byte[Marshal.SizeOf(tmpPacket)];
                        //tmpPacket.Length = 102;

                        unsafe
                        {
                            fixed (byte* fixed_buffer = buffer)
                            {
                                Marshal.StructureToPtr(tmpPacket, (IntPtr)fixed_buffer, false);
                            }
                        }

                        NetworkStream ns = new NetworkStream(_client);

                        ns.Write(buffer, 0, Marshal.SizeOf(tmpPacket));

                        ns.Close();
                        //client.Close();

                        //timer1.Stop();
                        ////timer1.Interval = 60000; //1분
                        //timer1.Interval = 10000; //10 초
                        //timer1.Start();
                        return string.Empty;
                    }
                    else
                    {
                        return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] Error : Not Connected...";
                    }
                }
                else
                {
                    return string.Empty;
                }
            }
            catch (Exception ex)
            {
                _client.Close();
                return DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] Error : Not Connected..."; 
            }
        }

        public string ServerSend(DataTable dt)
        {
            string sSendMessage = string.Empty;
            try
            {
                if (_client.Connected)
                {
                    for (int i = 0; i < dt.Rows.Count; i++)
                    {
                        string hole = dt.Rows[i]["HOLE_NO"].ToString();
                        string pinx = dt.Rows[i]["PIN_X"].ToString();
                        string pixy = dt.Rows[i]["PIN_Y"].ToString();
                        string lrgb = dt.Rows[i]["GUBUN"].ToString();

                        TempPacket2 tmpPacket2 = new TempPacket2();

                        tmpPacket2.Length = 9;
                        tmpPacket2.Command = 0x62;
                        tmpPacket2.hole = Convert.ToUInt16(hole);
                        tmpPacket2.x = Convert.ToUInt16(pinx);
                        tmpPacket2.y = Convert.ToUInt16(pixy);
                        tmpPacket2.gb = Convert.ToByte(Convert.ToInt32(lrgb) + 1);
                        
                        //보내기                     
                        byte[] buffer = new byte[Marshal.SizeOf(tmpPacket2)];

                        unsafe
                        {
                            fixed (byte* fixed_buffer = buffer)
                            {
                                Marshal.StructureToPtr(tmpPacket2, (IntPtr)fixed_buffer, false);
                            }
                        }
                        
                        NetworkStream ns = new NetworkStream(_client);

                        ns.Write(buffer, 0, Marshal.SizeOf(tmpPacket2));

                        ns.Close();
                        //client.Close();

                        sSendMessage += "\r\n" + i.ToString() + " : [" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] Hole : " + hole + "     X = " + pinx + ", Y =" + pixy;
                    }

                    if (_sendChk == 0 && dt.Rows.Count > 0)
                    {
                        /////////////////////////////////////////////////////////////
                        // Test 2/2  아래 한줄 주석처리
                        /////////////////////////////////////////////////////////////
                        sSendMessage += "\r\n[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] Transmission OK To Server";
                    }
                }
                else
                {
                    ServerConnect();
                }
            }
            catch
            {
                _sendChk = 1;
                _client.Close();

                sSendMessage += "\r\n[" + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "] Error : Not Connected...";
            }

            return sSendMessage;
        }

        [System.Runtime.InteropServices.DllImport("kernel32.dll")]
        private static extern int GetPrivateProfileString(string section, string key, string def, StringBuilder retVal, int size, string filePath);

        // 클래스(구조체) 샘플.
        [StructLayout(LayoutKind.Sequential)]
        public class TempPacket
        {
            public ushort Length;
            public ushort Command;
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 100)]
            public byte[] Data;  //바이트 배열 사용시....
        }

        // 클래스(구조체) 샘플.
        [StructLayout(LayoutKind.Sequential)]
        public class TempPacket2
        {
            public ushort Length;
            public ushort Command;
            public ushort hole;
            public ushort x;
            public ushort y;
            public byte gb;
        }
    }
}
