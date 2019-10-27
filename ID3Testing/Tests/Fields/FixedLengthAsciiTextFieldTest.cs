using System.IO;
using Achamenes.ID3.Fields;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Achamenes.ID3Tests
{
    [TestClass]
    //Tests the AsciiTextField class.
    public class FixedLengthAsciiTextFieldTest
    {
        private static string[] _asciiTestCases =
            {
                "",
                "a short string",
                "a long long long long long long long long long long long long long long long long string"
            };

        [TestMethod]
        //Tests the Parse and Write methods for ASCII strings.
        public void DoTest()
        {
            foreach (string testCase in _asciiTestCases) {
                FixedLengthAsciiTextField field = new FixedLengthAsciiTextField(testCase);
                MemoryStream stream = new MemoryStream();

                field.WriteToStream(stream);

                FixedLengthAsciiTextField field2 = new FixedLengthAsciiTextField(testCase.Length);
                field2.Parse(stream.GetBuffer(), 0);

                Assert.AreEqual(field.Text, field2.Text);
            }
        }
    }
}
