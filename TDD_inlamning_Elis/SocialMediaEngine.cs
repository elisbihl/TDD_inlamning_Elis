using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
                Console.WriteLine("Lost? Here are all the different commands you can run. \n " +
                                  "/post - To post a comment on your own or write @username to write on some other person wall \n" +
                                  "/timeline - And a name to see someones timeline \n" +
                                  "/wall - To see a list of the people following you \n" +
                                  "/follow - And a name to follow someone \n" +
                                  "/send_message - To send someone a message and use @ to select who. \n" +
                                  "/read_message - To read your incoming messages \n");

                RawInput = Console.ReadLine();
                SplitInput = SplitString(RawInput);
                if (SplitInput.Length > 1)
                {
                    CurrentUser = CheckIfUserExists(SplitInput[0]);
                    if (SplitInput.Length > 2)
                    {
                        CheckForTargetUser(SplitInput[2]);
                    }

                    switch (SplitInput[1].ToLower())
                    {
                        case "/post":
                            Console.WriteLine(Post(FormatedInput));
                            break;
                        case "/timeline":
                            Timeline(TargetUser.ListOfPosts).ForEach(a =>
                                Console.WriteLine(a.TimeOfPost.ToString("yy-MM-dd-HH-mm") + " " + a.UserName + ": " + a.PostMessage));
                            break;
                        case "/follow":
                            if (TargetUser != null)
                                Follow(TargetUser).ForEach(r => Console.WriteLine(r.UserName));
                            else
                                Console.WriteLine("User doesn't exist");
                            break;
                        case "/wall":
                            Wall(CurrentUser.ListOfFollowers).ForEach(a =>
                                Console.WriteLine(a.TimeOfPost.ToString("yy-MM-dd-HH-mm") + " " + a.UserName + ": " + a.PostMessage));
                            break;
                        case "/send_message":
                            Console.WriteLine(SendMessage(RawInput));
                            break;
                        case "/read_message":
                            ReadMessage(CurrentUser.ListOfMessages).ForEach(a =>
                                Console.WriteLine(a.TimeOfMessage.ToString("yy-MM-dd-HH-mm") + " " + a.UserName + ": " + a.Message));
                            break;
                        case "/exit":
                            exit = false;
                            break;
                        default:
                            Console.WriteLine("IIII, Wrong input! Try again....");
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
                TargetUser.AddPost(new Posts(message, DateTime.Now, TargetUser.UserName));
            }
            return CurrentUser.UserName + ": " + message;
        }

        public List<Posts> Timeline(List<Posts> targetsPosts)
        {
            if (!CurrentUser.ListOfFollowers.Contains(TargetUser))
            {
                Console.WriteLine("You must follow " + TargetUser.UserName + " to see posts");
                return new List<Posts>();
            }


            return targetsPosts.OrderByDescending(s => s.TimeOfPost).ToList();
        }

        public List<User> Follow(User followUser)
        {
            Console.WriteLine("These are the people you are following: ");
            CurrentUser.AddFollower(TargetUser);
            return CurrentUser.ListOfFollowers.OrderBy(user => user.UserName).ToList();
        }

        public List<Posts> Wall(List<User> followers)
        {
            List<Posts> sortedList = new List<Posts>();
            followers.ForEach(r => sortedList.AddRange(r.ListOfPosts.Where(x => x.UserName == r.UserName)));
            CurrentUser.ListOfPosts.ForEach(x => sortedList.Add(x));
            return sortedList.OrderByDescending(a => a.TimeOfPost).ToList();
        }

        public string SendMessage(string message)
        {
            if (TargetUser != null)
            {
                TargetUser.AddMessage(new Messages(message, DateTime.Now, TargetUser.UserName));
                return "You have sent " + TargetUser.UserName + " a message!";
            }
            return "User doesn't exist";
        }

        public List<Messages> ReadMessage(List<Messages> userMessages)
        {
            return userMessages.OrderByDescending(a => a.TimeOfMessage).ToList();
        }

        public bool CheckForTargetUser(string input)
        {
            input = input.TrimStart('@');

            foreach (var item in ListOfUsers)
            {
                if (item.UserName == input)
                {
                    TargetUser = ListOfUsers.FirstOrDefault(r => r.UserName == item.UserName);
                    return true;
                }
            }
            return false;
        }

        public string[] SplitString(string input)
        {
            FormatedInput = null;
            SplitInput = input.Split(' ', StringSplitOptions.RemoveEmptyEntries);

            for (int i = 2; i < SplitInput.Length; i++)
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
