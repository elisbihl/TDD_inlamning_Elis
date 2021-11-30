using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace TDD_inlamning_Elis
{
    public class User
    {
        public string UserName { get; set; }

        public List<Messages> ListOfMessages = new List<Messages>();

        public List<Posts> ListOfPosts = new List<Posts>();

        public List<User> ListOfFollowers = new List<User>();

        public User(string username)
        {
            UserName = username;
        }

        public void AddMessage(Messages message)
        {
            ListOfMessages.Add(message);
        }

        public void AddFollower(User newFollower)
        {
            ListOfFollowers.Add(newFollower);
        }

        public void AddPost(Posts post)
        {
            ListOfPosts.Add(post);
        }
    }
}
