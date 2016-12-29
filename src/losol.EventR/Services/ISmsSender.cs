using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace losol.EventR.Services
{
    public interface ISmsSender
    {
        //  tutorial https://www.elanderson.net/2016/03/sms-using-twilio-rest-api-in-asp-net-core/
        Task SendSmsAsync(string number, string message);
    }
}
