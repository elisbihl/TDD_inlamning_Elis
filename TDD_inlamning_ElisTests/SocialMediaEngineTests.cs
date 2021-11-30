using Microsoft.VisualStudio.TestTools.UnitTesting;
using TDD_inlamning_Elis;
using System;
using System.Collections.Generic;
using System.Linq;
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
            SocialMedia = new SocialMediaEngine();

            User bengt = new User("Bengt");
            User alice = new User("Alice");
            User charlie = new User("Charlie");
            User mallory = new User("Mallory");
            User bob = new User("Bob");
            SocialMedia.CurrentUser = bengt;
            SocialMedia.TargetUser = bob;
            SocialMedia.ListOfUsers = new List<User> { bengt, bob, alice, charlie, mallory };
            SocialMedia.SplitString("bob /post Wassup bitch");
            DateTime dt = new DateTime(2022, 12, 23);

            bengt.ListOfPosts.Add(new Posts("Hello", DateTime.Now, "Bengt"));
            bengt.ListOfPosts.Add(new Posts("hey", dt, "Bengt"));
            bob.ListOfPosts.Add(new Posts("hey", new DateTime(2016, 02, 1), "Bob"));

            bengt.AddFollower(bob);
        }

        [TestMethod()]
        public void PostTest()
        {
            //string[] test1 = SocialMedia.SplitString("Alice /post What a wonderfully sunny day!");
            Assert.AreEqual(SocialMedia.FormatedInput, "Wassup bitch ");
        }

        [TestMethod()]
        public void WallTest()
        {
            Assert.AreEqual(SocialMedia.ListOfUsers[0].UserName, "Bengt");
            SocialMedia.Wall(SocialMedia.CurrentUser.ListOfFollowers);
            Assert.AreEqual(SocialMedia.Wall(SocialMedia.CurrentUser.ListOfFollowers)[1],
                SocialMedia.CurrentUser.ListOfPosts[0]);
            Assert.AreEqual(SocialMedia.Wall(SocialMedia.CurrentUser.ListOfFollowers)[2], SocialMedia.ListOfUsers[1].ListOfPosts[0]);
        }

        [TestMethod()]
        public void CheckForTargetUserTest()
        {
            Assert.IsFalse(SocialMedia.CheckForTargetUser("asdasd"));
            Assert.IsTrue(SocialMedia.CheckForTargetUser("Alice"));
        }

        [TestMethod()]
        public void FollowTest()
        {

            string hello = SocialMedia.Follow();
            Assert.AreEqual(hello, "Bengt is now following " + SocialMedia.TargetUser.UserName);
            SocialMedia.TargetUser = null;
            Assert.AreEqual(SocialMedia.Follow(), "User doesn't exist");
        }

        [TestMethod()]
        [DataRow(new string[] { "Alice", "/post", "What", "a", "wonderfully", "sunny", "day!" },
            "Alice /post What a wonderfully sunny day!")]

        public void SplitStringTest(string[] output, string input)
        {
            Assert.AreEqual(output[1], SocialMedia.SplitString(input)[1]);
        }
    }
}