using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Security.Cryptography.X509Certificates;
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
                TargetUser = null;
                CurrentUser = null;
                Console.WriteLine("Welcome to ConsoleMedia!");
                Help();
                RawInput = Console.ReadLine();
                SplitInput = SplitString(RawInput);
                CurrentUser = CheckIfUserExists(SplitInput[0]);
                if(SplitInput.Length > 2)
                {
                    CheckForTargetUser(SplitInput[2]);

                    switch (SplitInput[1].ToLower())
                    {
                        case "/post":
                            Console.WriteLine(Post(FormatedInput));
                            break;
                        case "/timeline":
                            Timeline(TargetUser.ListOfPosts).ForEach(a => Console.Write(a.TimeOfPost.ToString("yy-MM-dd-HH-mm") + " " + a.UserName + ": " + a.PostMessage));
                            break;
                        case "/follow":
                            Console.WriteLine(Follow());
                            break;
                        case "/wall":
                            Wall(CurrentUser.ListOfFollowers).ForEach(a => Console.WriteLine(a.TimeOfPost.ToString("yy-MM-dd-HH-mm") + " " + a.UserName + ": " + a.PostMessage));
                            break;
                        case "/send_message":
                            Console.WriteLine(SendMessage(RawInput));
                            break;
                        case "/read_message":
                            ReadMessage(CurrentUser.ListOfMessages).ForEach(a => Console.WriteLine(a.TimeOfMessage.ToString("yy-MM-dd-HH-mm") + " " + a.UserName + ": " + a.Message));
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
            if (!CheckForTargetUser(SplitInput[2]))
            {
                CurrentUser.AddPost(new Posts(message, DateTime.Now, CurrentUser.UserName));
            }
            else
            {
                TargetUser.AddPost(new Posts(message,DateTime.Now, TargetUser.UserName));
            }
            return message;
        }

        public List<Posts> Timeline(List<Posts> targetsPosts)
        {
            if (!CurrentUser.ListOfFollowers.Contains(TargetUser))
            {
                Console.WriteLine("You must follow" + TargetUser.UserName + " to see posts");
                return new List<Posts>();
            }
            return targetsPosts.OrderByDescending(s => s.TimeOfPost).ToList();
        }

        public string Follow()
        {
            CurrentUser.AddFollower(TargetUser);
            return "You are now following " + TargetUser.UserName;
        }

        public List<Posts> Wall(List<User> followers)
        {
            List<Posts> sortedList = new List<Posts>();
            followers.ForEach(x => sortedList = x.ListOfPosts.Where(r => r.UserName == x.UserName && CurrentUser.UserName == r.UserName).ToList());
            CurrentUser.ListOfPosts.ForEach(a => sortedList.Add(a));
            foreach (var item in CurrentUser.ListOfPosts)
            {
                if (!sortedList.Contains(item))
                {
                    sortedList.Add(item);
                }
            }
            return sortedList.OrderByDescending(a => a.TimeOfPost).ToList();
        }

        public string SendMessage(string message)
        {
            TargetUser.ListOfMessages.Add(new Messages(message, DateTime.Now, TargetUser.UserName));
            return "You have sent " + TargetUser.UserName + " a message!";
        }

        public List<Messages> ReadMessage(List<Messages> userMessages)
        {
            return userMessages.OrderByDescending(a => a.TimeOfMessage).ToList();
        }

        public bool CheckForTargetUser(string input)
        {
            for (int i = 0; i < input.Length; i++)
            {
                if (input[i] == '@')
                {
                    input = input.TrimStart('@');
                }
            }
            foreach (var item in ListOfUsers)
            {
                if (item.UserName == input)
                {
                    TargetUser = ListOfUsers.First(r => r.UserName == item.UserName);
                    return true;
                }
            }
            return false;
        }

        public void Help()
        {
            Console.WriteLine("Lost? Here are all the different commands you can run. \n " +
                "/post - To post a comment on your own or write @username to write on some other person wall \n" +
                "/timeline - And a name to see someones timeline \n" +
                "/wall - To see a list of the people following you \n" +
                "/follow - And a name to follow someone \n" +
                "/send_message - To send someone a message and use @ to select who. \n" +
                "/read_message - To read your incoming messages \n");
        }

        public string[] SplitString(string input)
        {
            FormatedInput = " ";
            SplitInput = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 1; i < SplitInput.Length; i++)
            {
                FormatedInput += SplitInput[i] + " ";
            }

            return SplitInput;
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
