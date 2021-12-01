using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDD_inlamning_Elis;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Security;
using System.Text;
using System.Threading.Tasks;

namespace TDD_inlamning_Elis.Tests
{
    [TestClass()]
    public class SocialMediaEngineTests
    {
        public SocialMediaEngine SocialMedia { get; set; }

        [TestInitialize]
        public void TestInitializer()
        {
            //arrange
            SocialMedia = new SocialMediaEngine();

            User bengt = new User("Bengt");
            User alice = new User("Alice");
            User charlie = new User("Charlie");
            User mallory = new User("Mallory");
            User bob = new User("Bob");
            SocialMedia.CurrentUser = bengt;
            SocialMedia.TargetUser = bob;
            SocialMedia.ListOfUsers = new List<User> {bengt, bob, alice, charlie, mallory};
            SocialMedia.SplitString("bob /post Wassup bitch");
            DateTime dt = new DateTime(2022, 12, 23);

            //act
            bengt.ListOfPosts.Add(new Posts("Hello", DateTime.Now, "Bengt"));
            bengt.ListOfPosts.Add(new Posts("hey", dt, "Bengt"));
            bob.ListOfPosts.Add(new Posts("hey", new DateTime(2016, 02, 1), "Bob"));

            bengt.AddFollower(bob);
        }

        [TestMethod()]
        public void PostTest()
        {
            Assert.AreEqual(SocialMedia.FormatedInput, "Wassup bitch ");
            Assert.AreEqual(SocialMedia.Post("Hello"), SocialMedia.CurrentUser.UserName + ": Hello");
            Assert.AreEqual(SocialMedia.Post("@bob hello"), SocialMedia.CurrentUser.UserName + ": @bob hello");
            Assert.AreNotEqual(SocialMedia.Post("@kasdas hello"), SocialMedia.TargetUser.UserName + ": asdasd");
        }

        [TestMethod()]
        public void WallTest()
        {
            //act
            SocialMedia.CurrentUser.UserName = "Bengt";
            var testWall = SocialMedia.Wall(SocialMedia.CurrentUser.ListOfFollowers);
            var TargetWall = SocialMedia.Wall(SocialMedia.TargetUser.ListOfFollowers);
            //assert
            Assert.AreEqual(SocialMedia.ListOfUsers[0].UserName, "Bengt");
            Assert.AreEqual(testWall[1],
                SocialMedia.CurrentUser.ListOfPosts[0]);
            Assert.AreEqual(testWall[2],
                SocialMedia.ListOfUsers[1].ListOfPosts[0]);
            Assert.AreNotEqual(TargetWall, testWall);
        }

        [TestMethod()]
        public void CheckForTargetUserTest()
        {
            SocialMedia.TargetUser = SocialMedia.ListOfUsers.First(r => r.UserName == "Alice");

            Assert.IsFalse(SocialMedia.CheckForTargetUser("asdasd"));
            Assert.IsTrue(SocialMedia.CheckForTargetUser("Alice"));
        }

        [TestMethod()]
        public void FollowTest()
        {
            User testUser = new User("test");
            User asd = new User("Klas");
            SocialMedia.CurrentUser = asd;
            SocialMedia.ListOfUsers.Add(testUser);

            var hello = asd.ListOfFollowers.OrderBy(name => name.UserName).ToList();
            Assert.AreNotEqual(hello, SocialMedia.Follow(testUser));
        }

        [TestMethod()]
        [DataRow(new string[] {"Alice", "/post", "What", "a", "wonderfully", "sunny", "day!"},
            "Alice /post What a wonderfully sunny day!")]

        public void SplitStringTest(string[] output, string input)
        {
            Assert.AreEqual(output[1], SocialMedia.SplitString(input)[1]);
        }

        [TestMethod()]
        public void TimelineTest()
        {
            Assert.AreEqual(SocialMedia.TargetUser.ListOfPosts[0],
                SocialMedia.Timeline(SocialMedia.TargetUser.ListOfPosts)[0]);
            SocialMedia.TargetUser = new User("Berra");
            Assert.AreNotEqual(new List<Posts>(), SocialMedia.Timeline(SocialMedia.TargetUser.ListOfPosts));
        }

        [TestMethod()]
        public void CheckIfUserExistsTest()
        {
            Assert.AreEqual(SocialMedia.CheckIfUserExists("Bengt"), SocialMedia.CurrentUser);
            Assert.AreEqual(SocialMedia.CheckIfUserExists("Lisa"),
                SocialMedia.ListOfUsers.First(r => r.UserName == "Lisa"));
        }

        [TestMethod()]
        public void ReadMessageTest()
        {

            Messages testMessage1 = new Messages("Hello bitch", new DateTime(2015, 01, 01), "Råbert");
            Messages testMessage2 = new Messages("Hello bitch", new DateTime(2015, 02, 01), "Jansch");
            SocialMedia.CurrentUser.AddMessage(testMessage1);
            SocialMedia.CurrentUser.AddMessage(testMessage2);
            Assert.AreEqual(SocialMedia.ReadMessage(SocialMedia.CurrentUser.ListOfMessages)[0],
                SocialMedia.CurrentUser.ListOfMessages[1]);
            Assert.AreEqual(SocialMedia.ReadMessage(SocialMedia.CurrentUser.ListOfMessages)[1],
                SocialMedia.CurrentUser.ListOfMessages[0]);
        }

        [TestMethod()]
        public void SendMessageTest()
        {
            Assert.AreEqual("You have sent " + SocialMedia.TargetUser.UserName + " a message!",
                SocialMedia.SendMessage("Hihi"));
            User lars = new User("Bengt");
            SocialMedia.TargetUser = null;
            Assert.AreEqual("User doesn't exist", SocialMedia.SendMessage("Fack off"));
        }
    }
}