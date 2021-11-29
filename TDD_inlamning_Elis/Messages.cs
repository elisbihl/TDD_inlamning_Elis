using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_inlamning_Elis
{
    public class Messages
    {
        public string Message { get; set; }
        public DateTime TimeOfMessage { get; set; }

        public string UserName { get; set; }

        public Messages(string message, DateTime timeOfMessage, string username)
        {
            Message = message;
            TimeOfMessage = timeOfMessage;
            UserName = username;
        }
    }
}
