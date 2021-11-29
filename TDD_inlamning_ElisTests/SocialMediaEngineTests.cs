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
            SocialMedia.ListOfUsers.Add(bengt);
            bengt.ListOfPosts.Add(new Posts("Hello",DateTime.Now,"Bengt"));
        }
        [TestMethod()]
        public void PostTest()
        {
            User test = new User("bengt");

            Assert.Fail();
        }

        [TestMethod()]
        public void WallTest()
        {
            Assert.AreEqual(SocialMedia.ListOfUsers[0].UserName, "Bengt");
        }

        [TestMethod()]
        public void CheckForTargetUserTest()
        {
            string name = "@hey";
            Assert.AreEqual("hey", SocialMedia.CheckForTargetUser(name));
        }

        [TestMethod()]
        public void FollowTest()
        {
            User test = new User("bengt");
            SocialMedia.TargetUser = test;
            string hello = SocialMedia.Follow();
            Assert.AreEqual(hello, "You are now following " + test.UserName);
        }
    }
}