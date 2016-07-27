using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace jddc_TheWorld_2._0.Services
{
    public interface IMailService
    {
        void SendMail(string to, string from, string subject, string body);
    }
}
