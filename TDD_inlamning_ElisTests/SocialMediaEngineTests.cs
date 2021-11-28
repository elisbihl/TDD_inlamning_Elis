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
        [TestMethod()]
        public void PostTest()
        {
            User test = new User("bengt");
            
            Assert.Fail();
        }
    }
}