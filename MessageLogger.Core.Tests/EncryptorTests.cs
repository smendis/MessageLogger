using MessageLogger.Core.Helper;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MessageLogger.Core.Tests
{
    [TestClass]
    public class EncryptorTests
    {
        [TestMethod]
        public void Test_Encrypt_Succeed()
        {
            var encrypted = Encryptor.Encrypt("206a73bc84554b84b004fd5aa");

            Assert.AreEqual("4c395f0b7ce8e482b76d5df05", encrypted);
        }

    }
}
