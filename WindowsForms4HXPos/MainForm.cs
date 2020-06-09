using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Runtime.InteropServices;

namespace WindowsForms4HXPos
{

    public partial class MainForm : Form
    {
        /// </summary>
        [StructLayout(LayoutKind.Sequential)]
        public struct DATA_IN
        {
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] transtype ; //3交易类型
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] amout; //13交易金额
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] transflag; //2交易标志1，零售，2：批发
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] commport; //3串口号-保留
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2)]
            public byte[] paycnt; //3分期付款期数，如果不是分期付款，则传0
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] oldpostrace; //7原交易凭证号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] oldbatchcode; //7原交易批次号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] terminal; //9终端号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] transamount; //13退货，撤消时收银机通过amount传金额
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 8)]
            public byte[] oldtransdate; //9原交易日期（退货，撤销时用到）
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 6)]
            public byte[] oldauthornum; //7原交易授权号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 19)]
            public byte[] oldtranscardnum; //20原交易卡号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] clientMAC; //13瘦终端MAC地址
                                     //以下为新增内容4
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 4)]
            public byte[] branchno; //5门店号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] orderno; //21缴款订单号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 1)]
            public byte[] signature; // 2电子签名‘0’=不需要，'1'=需要
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] fqpayno; //13分期项目号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 12)]
            public byte[] oldHostSer; //13原交易参考号
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 20)]
            public byte[] payType; //21缴款类型
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 40)]
            public byte[] name;//41缴款人
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public byte[] beginTime;//15起始时间
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 14)]
            public byte[] endTime;//15终止时间
            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 50)]
            public byte[] note;//51备注
            
           public DATA_IN(int inData)
            {
                transtype = new byte[2];
                amout = new byte[12];
                transflag = new byte[1];
                commport = new byte[2];
                paycnt = new byte[2];
                oldpostrace = new byte[6];
                oldbatchcode = new byte[6];
                terminal = new byte[8];
                transamount = new byte[12];
                oldtransdate = new byte[8];
                oldauthornum = new byte[6];
                oldtranscardnum = new byte[19];
                clientMAC = new byte[12];
                branchno = new byte[4];
                orderno = new byte[20];
                signature = new byte[1];
                fqpayno = new byte[12];
                oldHostSer = new byte[12];
                payType = new byte[20];
                name = new byte[40];
                beginTime = new byte[14];
                endTime = new byte[14];
                note = new byte[50];
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
            data_IN.transtype = Encoding.UTF8.GetBytes("03".PadRight(2, '\0'));
            data_IN.amout = Encoding.UTF8.GetBytes("000000000001");
            data_IN.orderno = Encoding.UTF8.GetBytes("1".PadRight(20, '\0'));
            data_IN.name = Encoding.UTF8.GetBytes("1".PadRight(40, '\0'));
            data_IN.beginTime = Encoding.UTF8.GetBytes("20200516124700".PadRight(14, '\0'));
            data_IN.endTime = Encoding.UTF8.GetBytes("20200516124700".PadRight(14, '\0'));
            data_IN.note = Encoding.UTF8.GetBytes("1".PadRight(50, '\0'));
            DATA_OUT data_OUT = new DATA_OUT(2);
            long result = CebMisPosInterface(ref data_IN, ref data_OUT);
            Console.WriteLine("OK!");
        }

     
        private void axmainFrame1_Enter(object sender, EventArgs e)
        {

        }
    }
}
