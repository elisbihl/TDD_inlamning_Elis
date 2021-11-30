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
    public class UserTests
    {

        [TestMethod()]
        public void UserTest()
        {
            User test = new User("Test");
            Assert.AreEqual(test.UserName, "Test");
        }

        [TestMethod()]
        public void AddMessageTest()
        {
            User test = new User("Henry");
            Messages testMessages = new Messages("Hello", new DateTime(2015, 02, 01), "Henry");
            test.AddMessage(testMessages);
            Assert.AreEqual(test.ListOfMessages[0], testMessages);
        }

        [TestMethod()]
        public void AddFollowerTest()
        {
            User test = new User("Henry");
            User followerTest = new User("Bob");
            test.AddFollower(followerTest);
            Assert.AreEqual(test.ListOfFollowers[0], followerTest);
        }

        [TestMethod()]
        public void AddPostTest()
        {
            User test = new User("Henry");
            Posts testPost = new Posts("Hello", new DateTime(2015, 02, 01), "Henry");
            test.AddPost(testPost);
            Assert.AreEqual(test.ListOfPosts[0], testPost);
        }
    }
}