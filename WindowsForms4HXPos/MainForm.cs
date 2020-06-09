using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using Fleck;
using Newtonsoft.Json;

namespace WindowsForms4HXPos
{

    public partial class MainForm : Form
    {
        bool started = false;

        [StructLayout(LayoutKind.Sequential)]
        public struct DATA_IN
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] transtype ; //3交易类型
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public byte[] amout; //13交易金额
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] transflag; //2交易标志1，零售，2：批发
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] commport; //3串口号-保留
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3)]
            public byte[] paycnt; //3分期付款期数，如果不是分期付款，则传0
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] oldpostrace; //7原交易凭证号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] oldbatchcode; //7原交易批次号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public byte[] terminal; //9终端号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public byte[] transamount; //13退货，撤消时收银机通过amount传金额
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 9)]
            public byte[] oldtransdate; //9原交易日期（退货，撤销时用到）
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 7)]
            public byte[] oldauthornum; //7原交易授权号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] oldtranscardnum; //20原交易卡号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public byte[] clientMAC; //13瘦终端MAC地址
                                     //以下为新增内容4
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 5)]
            public byte[] branchno; //5门店号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
            public byte[] orderno; //21缴款订单号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] signature; // 2电子签名‘0’=不需要，'1'=需要
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public byte[] fqpayno; //13分期项目号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13)]
            public byte[] oldHostSer; //13原交易参考号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 21)]
            public byte[] payType; //21缴款类型
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 41)]
            public byte[] name;//41缴款人
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
            public byte[] beginTime;//15起始时间
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 15)]
            public byte[] endTime;//15终止时间
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 51)]
            public byte[] note;//51备注
            
           public DATA_IN(int inData)
            {
                transtype = new byte[3];
                amout = new byte[13];
                transflag = new byte[2];
                commport = new byte[3];
                paycnt = new byte[3];
                oldpostrace = new byte[7];
                oldbatchcode = new byte[7];
                terminal = new byte[9];
                transamount = new byte[13];
                oldtransdate = new byte[9];
                oldauthornum = new byte[7];
                oldtranscardnum = new byte[20];
                clientMAC = new byte[13];
                branchno = new byte[5];
                orderno = new byte[21];
                signature = new byte[2];
                fqpayno = new byte[13];
                oldHostSer = new byte[13];
                payType = new byte[21];
                name = new byte[41];
                beginTime = new byte[15];
                endTime = new byte[15];
                note = new byte[51];
            }
        };
        [StructLayout(LayoutKind.Sequential)]
        public struct DATA_OUT /*交易结果*/
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] RETCODE; //交易结果（00 表示交易成功）5
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 19)]
            public byte[] CARDNO; //卡号
                                  /*以下为需打印的内容*/
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] CARDTYPE; //卡种类
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] AMOUNT; //金额
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] DATE; //日期
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] TIME; //时间
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] TRACE; //流水号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] CHANNEL;// 交易渠道“01” 银行“02” 微信“03” 支付宝
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public byte[] CHANNELORDERNO; //渠道订单号微信和支付宝用到右补空格
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public byte[] MERCHANTORDERNO;// 商户订单号右补空格
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public byte[] NOTE; //备注
            public DATA_OUT(int outData)
            {
                RETCODE = new byte[2];
                CARDNO = new byte[19];
                CARDTYPE = new byte[20];
                AMOUNT = new byte[12];
                DATE = new byte[8];
                TIME = new byte[6];
                TRACE = new byte[6];
                CHANNEL = new byte[2];
                CHANNELORDERNO = new byte[50];
                MERCHANTORDERNO = new byte[50];
                NOTE = new byte[50];
            }
        };


        [DllImport("PaxMis.dll", EntryPoint = "CebMisPosInterface", CallingConvention = CallingConvention.StdCall)]
        public static extern long CebMisPosInterface(ref DATA_IN misinput, ref DATA_OUT returnMsg);

        public MainForm()
        {
            InitializeComponent();
        }

        private void requestBtn_Click(object sender, EventArgs e)
        {
            DATA_IN data_IN = new DATA_IN(1);
            Array.Copy(Encoding.UTF8.GetBytes("03"), data_IN.transtype, Encoding.UTF8.GetBytes("03").Length);
            Array.Copy(Encoding.UTF8.GetBytes("000000000001"), data_IN.amout, Encoding.UTF8.GetBytes("000000000001").Length);
            Array.Copy(Encoding.UTF8.GetBytes("1"), data_IN.orderno, Encoding.UTF8.GetBytes("1").Length);
            Array.Copy(Encoding.UTF8.GetBytes("1"), data_IN.name, Encoding.UTF8.GetBytes("1").Length);
            var dataTimeArray = Encoding.UTF8.GetBytes(DateTime.Now.ToString("yyyyMMddHHmmss"));
            Array.Copy(dataTimeArray, data_IN.beginTime, dataTimeArray.Length);
            Array.Copy(dataTimeArray, data_IN.endTime, dataTimeArray.Length);
            Array.Copy(Encoding.UTF8.GetBytes("1"), data_IN.note, Encoding.UTF8.GetBytes("1").Length);

            DATA_OUT data_OUT = new DATA_OUT(2);
            long result = CebMisPosInterface(ref data_IN, ref data_OUT);
            Console.WriteLine(Encoding.Default.GetString(data_OUT.RETCODE));
            Console.WriteLine(Encoding.Default.GetString(data_OUT.CARDNO));
            Console.WriteLine(Encoding.Default.GetString(data_OUT.CARDTYPE));
            Console.WriteLine(Encoding.Default.GetString(data_OUT.AMOUNT));
            Console.WriteLine(Encoding.Default.GetString(data_OUT.DATE));
            Console.WriteLine(Encoding.Default.GetString(data_OUT.TIME));
            Console.WriteLine(Encoding.Default.GetString(data_OUT.TRACE));
            Console.WriteLine(Encoding.Default.GetString(data_OUT.CHANNEL));
            Console.WriteLine(Encoding.Default.GetString(data_OUT.CHANNELORDERNO));
            Console.WriteLine(Encoding.Default.GetString(data_OUT.MERCHANTORDERNO));
            Console.WriteLine(Encoding.Default.GetString(data_OUT.NOTE));
            Console.WriteLine("-------------------------------------");
            Console.WriteLine(Encoding.UTF8.GetString(data_OUT.RETCODE));
            Console.WriteLine(Encoding.UTF8.GetString(data_OUT.CARDNO));
            Console.WriteLine(Encoding.UTF8.GetString(data_OUT.CARDTYPE));
            Console.WriteLine(Encoding.UTF8.GetString(data_OUT.AMOUNT));
            Console.WriteLine(Encoding.UTF8.GetString(data_OUT.DATE));
            Console.WriteLine(Encoding.UTF8.GetString(data_OUT.TIME));
            Console.WriteLine(Encoding.UTF8.GetString(data_OUT.TRACE));
            Console.WriteLine(Encoding.UTF8.GetString(data_OUT.CHANNEL));
            Console.WriteLine(Encoding.UTF8.GetString(data_OUT.CHANNELORDERNO));
            Console.WriteLine(Encoding.UTF8.GetString(data_OUT.MERCHANTORDERNO));
            Console.WriteLine(Encoding.UTF8.GetString(data_OUT.NOTE));
            Console.WriteLine("OK!");
        }

        public string pay(string message)
        {
            DATA_IN data_IN = new DATA_IN(1);
            Array.Copy(Encoding.UTF8.GetBytes("03"), data_IN.transtype, Encoding.UTF8.GetBytes("03").Length);

            var amoutArray = Encoding.UTF8.GetBytes((float.Parse(message) * 100).ToString().PadLeft(12, '0'));
            Array.Copy(amoutArray, data_IN.amout, amoutArray.Length);

            Array.Copy(Encoding.UTF8.GetBytes("1"), data_IN.orderno, Encoding.UTF8.GetBytes("1").Length);
            Array.Copy(Encoding.UTF8.GetBytes("1"), data_IN.name, Encoding.UTF8.GetBytes("1").Length);
            var dataTimeArray = Encoding.UTF8.GetBytes(DateTime.Now.ToString("yyyyMMddHHmmss"));
            Array.Copy(dataTimeArray, data_IN.beginTime, dataTimeArray.Length);
            Array.Copy(dataTimeArray, data_IN.endTime, dataTimeArray.Length);
            Array.Copy(Encoding.UTF8.GetBytes("1"), data_IN.note, Encoding.UTF8.GetBytes("1").Length);

            DATA_OUT data_OUT = new DATA_OUT(2);

            long result = CebMisPosInterface(ref data_IN, ref data_OUT);

            ReturnMsg returnMsg = new ReturnMsg();
            returnMsg.RETCODE = Encoding.Default.GetString(data_OUT.RETCODE).Trim();
            returnMsg.CARDNO = Encoding.Default.GetString(data_OUT.CARDNO).Trim();
            returnMsg.CARDTYPE = Encoding.Default.GetString(data_OUT.CARDTYPE).Trim();
            returnMsg.AMOUNT = Encoding.Default.GetString(data_OUT.AMOUNT).Trim();
            returnMsg.DATE = Encoding.Default.GetString(data_OUT.DATE).Trim();
            returnMsg.TIME = Encoding.Default.GetString(data_OUT.TIME).Trim();
            returnMsg.TRACE = Encoding.Default.GetString(data_OUT.TRACE).Trim();
            returnMsg.CHANNEL = Encoding.Default.GetString(data_OUT.CHANNEL).Trim();
            returnMsg.CHANNELORDERNO = Encoding.Default.GetString(data_OUT.CHANNELORDERNO).Trim();
            returnMsg.MERCHANTORDERNO = Encoding.Default.GetString(data_OUT.MERCHANTORDERNO).Trim();
            returnMsg.NOTE = Encoding.Default.GetString(data_OUT.NOTE).Trim();
            var returnInfo = JsonConvert.SerializeObject(returnMsg);
            Console.WriteLine("------------------" + returnInfo + "-------------------");
            return returnInfo;
        }

        public void startWebsocket()
        {
            FleckLog.Level = LogLevel.Debug;
            var allSockets = new List<IWebSocketConnection>();
            var server = new WebSocketServer("ws://0.0.0.0:7181");
            server.Start(socket =>
            {
                socket.OnOpen = () =>
                {
                    Console.WriteLine("Open!");
                    allSockets.Add(socket);
                };

                socket.OnClose = () =>
                {
                    Console.WriteLine("Close!");
                    allSockets.Remove(socket);
                };

                socket.OnMessage = message =>
                {
                    Console.WriteLine(message);

                    socket.Send("Echo: " + new MainForm().pay(message));
                };
            });
        }

        private void startWebsocketBtn_Click(object sender, EventArgs e)
        {
            if (!started)
            {
                startWebsocket();
                started = true;
                infoLbl.Text += "\n服务启动成功！！！";
            }

        }
    }
}
