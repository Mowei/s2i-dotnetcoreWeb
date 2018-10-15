using System.Threading.Tasks;
using Mowei.Common;

namespace Mowei.Services
{
    // This class is used by the application to send email for account confirmation and password reset.
    // For more details see https://go.microsoft.com/fwlink/?LinkID=532713
    public class SmsSender : ISmsSender
    {
        public async Task<bool> SendSmsAsync(string number, string subject, string message)
        {
            SMSHttp smsHttp = new SMSHttp();
            return await smsHttp.SendSMSAsync(CommEnvironment.SmsAccount, CommEnvironment.SmsPassWord, $@"{subject}", message, number, "");
        }
        public async Task<string> GetCreditAsync()
        {
            SMSHttp smsHttp = new SMSHttp();
            if (await smsHttp.GetCreditAsync(CommEnvironment.SmsAccount, CommEnvironment.SmsPassWord))
            {
                return smsHttp.Credit.ToString();
            }
            else
            {
                return "µo¥Í¿ù»~:" + smsHttp.ProcessMsg;
            }
        }
    }
}
