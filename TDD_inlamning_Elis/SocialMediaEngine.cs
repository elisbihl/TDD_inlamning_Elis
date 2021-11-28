using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace TDD_inlamning_Elis
{
    public class SocialMediaEngine
    {

        public string RawInput { get; set; }

        public string[] SplitInput { get; set; }

        public User CurrentUser { get; set; }

        public User TargetUser { get; set; }

        public List<User> ListOfUsers = new List<User>();

        public string FormatedInput { get; set; }

        public void RunSocialMedia()
        {
            bool exit = true;
            while (exit)
            {
                
                Console.WriteLine("Welcome to ConsoleMedia!");
                Help();
                RawInput = Console.ReadLine();
                SplitString(RawInput);
                CurrentUser = CheckIfUserExists(SplitInput[0]);
                if(SplitInput.Length > 2)
                {
                    CheckForTargetUser(SplitInput[2]);

                    switch (SplitInput[1].ToLower())
                    {
                        case "/post":
                            Post(FormatedInput);
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
                            Wall(CurrentUser.ListOfFollowers);
                            break;
                        case "/send_message":
                            SendMessage(RawInput);
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
                Console.WriteLine("Press any key to continue...");
                Console.ReadKey();
                Console.Clear();
            }
        }

        public string Post(string message)
        {
            CurrentUser.AddPost(new Posts(postMesssage: message, timeOfPost: DateTime.Now, username: CurrentUser.UserName));
            return message;
        }

        public void Timeline()
        {
            foreach (var post in TargetUser.ListOfPosts)
            {
                Console.WriteLine(post);
            }
        }

        public string Follow()
        {
            CurrentUser.AddFollower(TargetUser);
            return "You are now following " + TargetUser.UserName;
        }

        public void Wall(List<User> followers)
        {
            List<Posts> orderedList = new List<Posts>();
            foreach (var item in followers)
            {
                orderedList = item.ListOfPosts.Where(r => r.UserName == item.UserName).OrderBy(r => r.TimeOfPost).ToList();
            }

            foreach (var a in orderedList)
            {
                Console.WriteLine(a.TimeOfPost.ToString("yy-MM-dd-HH-mm") + " " + a.UserName + ": " + a.PostMessage);
            }
        }

        public string SendMessage(string message)
        {
            return message;
        }

        public string ReadMessage()
        {
            return RawInput;
        }

        public string CheckForTargetUser(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '@')
                {
                    input += input.Trim('@');
                }
            }

            foreach (var item in ListOfUsers)
            {
                if (item.UserName == input)
                {
                    TargetUser = ListOfUsers.First(r => r.UserName == input);
                }
            }
            return input;
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

        public void SplitString(string input)
        {
            FormatedInput = " ";
            SplitInput = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < SplitInput.Length; i++)
            {
                FormatedInput += SplitInput[i] + " ";
            }
        }

        public User CheckIfUserExists(string username)
        {
            foreach (var user in ListOfUsers)
            {
                if (user.UserName == username)
                {
                    return user;
                }
            }

            User newuser = new User(username: username);
            ListOfUsers.Add(newuser);
            return newuser;
        }
    }
}
