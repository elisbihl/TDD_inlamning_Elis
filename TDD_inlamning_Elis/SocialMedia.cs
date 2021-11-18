using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_inlamning_Elis
{
    public class SocialMedia
    {

        public string Input { get; set; }

        public string UserName { get; set; }

        public void Check()
        {
            UserName = Console.ReadLine();

            switch (Input)
            {
                case "/post":
                    break;
                case "/timeline":
                    break;
                case "/follow":
                    break;
                case "/wall":
                    break;
                case "/send_message":
                    break;
                case "/read_message":
                    break;
                default:
                    break;
            }
        }

        public void Post()
        {

        }

        public void Timeline()
        {

        }

        public void Follow()
        {

        }

        public void Wall()
        {

        }

        public string SendMessage(string message)
        {
            return message;
        }

        public string ReadMessage()
        {
            return Input;
        }

        public bool Tag(string message)
        {
            return false;
        }
    }
}
