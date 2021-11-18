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

        public User UserName { get; set; }

        public List<User> ListOfUsers = new List<User>() {
            new User(username: "McNeilty"),
            new User(username: "Avon"),
            new User(username: "Karin"),
            new User(username: "Dennis")
            };

        public void RunSocialMedia()
        {
            bool exit = true;
            Console.WriteLine("Welcome to ConsoleMedia!");
            Console.WriteLine("Username: ");
            UserName.UserName = Console.ReadLine();
            Console.WriteLine("You can always write /help to get a list of commands");
            while (exit)
            {
                Console.WriteLine(UserName + ":");

                UserName.UserName = Console.ReadLine();

                switch (Input.ToLower())
                {
                    case "/post":
                        Post();
                        break;
                    case "/help":
                        break;
                    case "/timeline":
                        Timeline();
                        break;
                    case "/follow":
                        Follow();
                        break;
                    case "/wall":
                        Wall();
                        break;
                    case "/send_message":
                        SendMessage(Input);
                        break;
                    case "/read_message":
                        ReadMessage();
                        break;
                    case "/exit":
                        exit = false;
                        break;
                    default:
                        Console.WriteLine("IIII, Couldn't understand you! Try again....");
                        break;
                }
            }
        }

        public string Post()
        {
            Console.WriteLine("/post Write your post: ");
            Console.WriteLine("/post ");
            string message = Console.ReadLine();
            UserName.AddPost(message);
            return "New post: " + message;
        }

        public void Timeline()
        {

        }

        public string Follow(User user)
        {
            user.ListOfFollowers.Add(UserName.UserName);
            return "You are now following " + user.UserName;
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

        public void Tag(string message)
        {
            int num = 0;
            foreach (char character in message)
            {
                if (character == '@')
                {

                }
                num++;
            }
        }

        public void Help()
        {
            Console.WriteLine("Lost? Here are all the different commands you ca run. \n " +
                "/post - To post a comment on your own or someone elses wall \n" +
                "/timeline - To see someones timeline \n" +
                "/wall - To see a list of the people following you \n" +
                "/follow - To follow someone \n" +
                "/send_message - To send someone a message \n" +
                "/read_message - To read your incoming messages \n" +
                "Don't forget to use @username to specify who you want to use these commands on.");
        }

        public void NewUser()
        {
            foreach (var user in ListOfUsers)
            {
                if (user.UserName != UserName)
                {
                    ListOfUsers.Add(new User(username: UserName));

                }
            }
        }
    }
}
