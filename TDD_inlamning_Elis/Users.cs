using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_inlamning_Elis
{
    public class User
    {
        public string UserName { get; set; }

        public List<string> ListOfMessages { get; set; }

        public List<string> ListOfPosts { get; set; }

        public List<string> ListOfFollowers { get; set; }

        public User(string username)
        {
            UserName = username;
        }

        public void AddMessage()
        {
            
        }

        public void AddUser()
        {

        }

        public void AddPost(string message)
        {
            ListOfPosts.Add(message);
        }

        public void ViewMessage()
        {
            foreach(var message in ListOfMessages)
            {
                Console.WriteLine(message);
            }
        }
    }
}
