using Microsoft.VisualStudio.TestTools.UnitTesting;
using Crittografia;

namespace CrittografiaTest
{
    [TestClass]
    public class EncrypterTests
    {
        [TestMethod]
        public void Encrypt_WithValidInput_ReturnsEncryptedText()
        {
            string text = "Hello World!";
            string key = "123";
            string expected = "Igmnp Yptmf!";

            var result = Encrypter.Encrypt(text, key);

            Assert.AreEqual(expected, result.Item1);
        }
    }

    [TestClass]
    public class DecrypterTests
    {
        [TestMethod]
        public void Decrypt_WithValidInput_ReturnsOriginalText()
        {
            string key = "123";
            string encryptedText = "Igmnp Yptmf!";
            string expected = "Hello World!";

            var result = Decrypter.Decrypt(encryptedText, key);

            Assert.AreEqual(expected, result);
        }
    }
}
