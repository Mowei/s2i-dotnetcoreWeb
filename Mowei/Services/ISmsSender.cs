using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Mowei.Services
{
    public interface ISmsSender
    {
        Task<bool> SendSmsAsync(string number, string subject, string message);
        Task<string> GetCreditAsync();
    }
}
