using System;

namespace Mowei.Common
{
    public class CommEnvironment
    {
        public static string SmtpHost { get; set; }
        public static int SmtpPort { get; set; }
        public static string SmtpAccount { get; set; }
        public static string SmtpPassWord { get; set; }

        public static string SmsAccount { get; set; }
        public static string SmsPassWord { get; set; }
    }
}
