using System;
using System.IO;
using Achamenes.ID3.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Achamenes.ID3Tests
{
    [TestClass]
    //Tests the SingleByteField class.
    public class SingleByteFieldTest
    {
        private Random _randomNumberGenerator = null;

        [TestInitialize]
        public void SetUp()
        {
            _randomNumberGenerator = new Random();
        }

        [TestMethod]
        //Tests the Write method of the class.
        public void TestWrite()
        {
            for (int testCase = 0; testCase < 100; testCase++) {
                byte[] randomData = new byte[1];
                _randomNumberGenerator.NextBytes(randomData);
                SingleByteField field = new SingleByteField(randomData[0]);
                MemoryStream stream = new MemoryStream();
                field.WriteToStream(stream);
                Assert.AreEqual(stream.Length, 1);
                Assert.AreEqual(randomData[0], stream.GetBuffer()[0]);
            }
        }

        [TestMethod]
        //Tests the Read method of the class.
        public void TestRead()
        {
            for (int testCase = 0; testCase < 100; testCase++) {
                byte[] randomData = new byte[_randomNumberGenerator.Next(1, 50000)];
                int offset = _randomNumberGenerator.Next(0, randomData.Length - 1);
                _randomNumberGenerator.NextBytes(randomData);
                SingleByteField field = new SingleByteField();
                field.Parse(randomData, offset);
                Assert.AreEqual(randomData[offset], field.Value);
            }
        }
    }
}
