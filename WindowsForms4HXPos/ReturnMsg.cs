using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsForms4HXPos
{
    class ReturnMsg
    {
        public string RETCODE; //交易结果（00 表示交易成功）5
        public string CARDNO; //卡号
                              /*以下为需打印的内容*/
        public string CARDTYPE; //卡种类
        public string AMOUNT; //金额
        public string DATE; //日期
        public string TIME; //时间
        public string TRACE; //流水号
        public string CHANNEL;// 交易渠道“01” 银行“02” 微信“03” 支付宝
        public string CHANNELORDERNO; //渠道订单号微信和支付宝用到右补空格
        public string MERCHANTORDERNO;// 商户订单号右补空格
        public string NOTE; //备注
    }
}
