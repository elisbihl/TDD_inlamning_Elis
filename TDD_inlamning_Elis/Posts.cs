using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TDD_inlamning_Elis
{
    public class Posts
    {
        public string PostMessage { get; set; }
        public DateTime TimeOfPost { get; set; }

        public string UserName { get; set; }

        public Posts(string postMesssage, DateTime timeOfPost, string username)
        {
            PostMessage = postMesssage;
            TimeOfPost = timeOfPost;
            UserName = username;
        }

        internal string ToList()
        {
            throw new NotImplementedException();
        }
    }
}
