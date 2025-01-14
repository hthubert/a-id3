using System.IO;
using Achamenes.ID3;
using Achamenes.ID3.Frames;
using Achamenes.ID3.Frames.Parsers;
using Achamenes.ID3.Frames.Writers;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Achamenes.ID3Tests
{
    [TestClass]
    public class TextFrameTest
    {
        private static string[] _asciiTestCases = {
                "",
                "a short string",
                "a long long long long long long long long long long long long long long long long string"
            };
        private static string[] _unicodeTestCases =
            {
                "",
                "a string with unicode text in it: εν αρχη ην...",
                "Even more Unicode characters: سلام",
                " تو آنی که گفتی که رویین تنم     بلند آسمان بر زمین بر زنم",
                "και η ζωη ην το φως των ανθρωπων."
            };

        private void TestTextFrame(TextFrame frame, EncodingScheme encoding, ID3v2MajorVersion version)
        {
            string frameId;
            FrameParserFactory factory = new FrameParserFactory();
            FrameWriter writer = frame.CreateWriter(version, encoding);
            if (writer == null) {
                return;
            }
            MemoryStream stream = new MemoryStream();


            writer.WriteToStream(stream);
            stream.Seek(0, SeekOrigin.Begin);

            Frame writtenFrame = FrameParser.Parse(stream, version, factory, out frameId);

            Assert.IsInstanceOfType(frame, writtenFrame.GetType());
            TextFrame textFrame = (TextFrame)writtenFrame;

            Assert.AreEqual(textFrame.Text, frame.Text);
        }

        private void TestVersion(EncodingScheme encoding, ID3v2MajorVersion version)
        {
            foreach (string testcase in _asciiTestCases) {
                TestTextFrame(new AlbumTextFrame(testcase), encoding, version);
                TestTextFrame(new ArtistTextFrame(testcase), encoding, version);
                TestTextFrame(new ComposerTextFrame(testcase), encoding, version);
                TestTextFrame(new CopyrightTextFrame(testcase), encoding, version);
                TestTextFrame(new CustomUserTextFrame(testcase), encoding, version);
                TestTextFrame(new DateTextFrame(testcase), encoding, version);
                TestTextFrame(new EncodedByTextFrame(testcase), encoding, version);
                TestTextFrame(new EncodingTimeTextFrame(testcase), encoding, version);
                TestTextFrame(new FileTypeTextFrame(testcase), encoding, version);
                TestTextFrame(new GenreTextFrame(testcase), encoding, version);
                TestTextFrame(new GroupingTextFrame(testcase), encoding, version);
                TestTextFrame(new InitialKeyTextFrame(testcase), encoding, version);
                TestTextFrame(new LanguageTextFrame(testcase), encoding, version);
                TestTextFrame(new LengthTextFrame(testcase), encoding, version);
                TestTextFrame(new MediaTypeTextFrame(testcase), encoding, version);
                TestTextFrame(new OrchestraTextFrame(testcase), encoding, version);
                TestTextFrame(new OriginalAlbumTextFrame(testcase), encoding, version);
                TestTextFrame(new OriginalArtistTextFrame(testcase), encoding, version);
                TestTextFrame(new OriginalReleaseTimeTextFrame(testcase), encoding, version);
                TestTextFrame(new OriginalReleaseYearTextFrame(testcase), encoding, version);
                TestTextFrame(new PublisherTextFrame(testcase), encoding, version);
                TestTextFrame(new RecordingTimeTextFrame(testcase), encoding, version);
                TestTextFrame(new ReleaseTimeTextFrame(testcase), encoding, version);
                TestTextFrame(new SoftwareSettingsTextFrame(testcase), encoding, version);
                TestTextFrame(new TaggingTimeTextFrame(testcase), encoding, version);
                TestTextFrame(new TitleTextFrame(testcase), encoding, version);
            }
            if (encoding != EncodingScheme.Ascii) {
                foreach (string testcase in _unicodeTestCases) {
                    TestTextFrame(new AlbumTextFrame(testcase), encoding, version);
                    TestTextFrame(new ArtistTextFrame(testcase), encoding, version);
                    TestTextFrame(new ComposerTextFrame(testcase), encoding, version);
                    TestTextFrame(new CopyrightTextFrame(testcase), encoding, version);
                    TestTextFrame(new CustomUserTextFrame(testcase), encoding, version);
                    TestTextFrame(new DateTextFrame(testcase), encoding, version);
                    TestTextFrame(new EncodedByTextFrame(testcase), encoding, version);
                    TestTextFrame(new EncodingTimeTextFrame(testcase), encoding, version);
                    TestTextFrame(new FileTypeTextFrame(testcase), encoding, version);
                    TestTextFrame(new GenreTextFrame(testcase), encoding, version);
                    TestTextFrame(new GroupingTextFrame(testcase), encoding, version);
                    TestTextFrame(new InitialKeyTextFrame(testcase), encoding, version);
                    TestTextFrame(new LanguageTextFrame(testcase), encoding, version);
                    TestTextFrame(new LengthTextFrame(testcase), encoding, version);
                    TestTextFrame(new MediaTypeTextFrame(testcase), encoding, version);
                    TestTextFrame(new OrchestraTextFrame(testcase), encoding, version);
                    TestTextFrame(new OriginalAlbumTextFrame(testcase), encoding, version);
                    TestTextFrame(new OriginalArtistTextFrame(testcase), encoding, version);
                    TestTextFrame(new OriginalReleaseTimeTextFrame(testcase), encoding, version);
                    TestTextFrame(new OriginalReleaseYearTextFrame(testcase), encoding, version);
                    TestTextFrame(new PublisherTextFrame(testcase), encoding, version);
                    TestTextFrame(new RecordingTimeTextFrame(testcase), encoding, version);
                    TestTextFrame(new ReleaseTimeTextFrame(testcase), encoding, version);
                    TestTextFrame(new SoftwareSettingsTextFrame(testcase), encoding, version);
                    TestTextFrame(new TaggingTimeTextFrame(testcase), encoding, version);
                    TestTextFrame(new TitleTextFrame(testcase), encoding, version);
                }
            }
            TestTextFrame(new PartOfSetTextFrame(4), encoding, version);
            TestTextFrame(new PartOfSetTextFrame(45, 123), encoding, version);
            TestTextFrame(new TrackTextFrame(415), encoding, version);
            TestTextFrame(new TrackTextFrame(15, 1234), encoding, version);
            TestTextFrame(new YearTextFrame(""), encoding, version);
            TestTextFrame(new YearTextFrame("14"), encoding, version);
            TestTextFrame(new YearTextFrame("144"), encoding, version);
            TestTextFrame(new YearTextFrame("1234"), encoding, version);
            TestTextFrame(new TimeTextFrame("134"), encoding, version);
            TestTextFrame(new BeatsPerMinuteTextFrame(134), encoding, version);
        }

        private void Test(EncodingScheme encoding)
        {
            TestVersion(encoding, ID3v2MajorVersion.Version2);
            TestVersion(encoding, ID3v2MajorVersion.Version3);
            TestVersion(encoding, ID3v2MajorVersion.Version4);
        }

        [TestMethod]
        //Tests the Parse and Write methods for ASCII strings.
        public void TestAscii()
        {
            Test(EncodingScheme.Ascii);
        }

        [TestMethod]
        //Tests the Parse and Write methods for UTF8 strings.
        public void TestUTF8()
        {
            Test(EncodingScheme.UTF8);
        }

        [TestMethod]
        //Tests the Parse and Write methods for UTF16 with BOM strings        
        public void TestUTF16BOM()
        {
            Test(EncodingScheme.UnicodeWithBOM);
        }

        [TestMethod]
        //Tests the Parse and Write methods for UTF16 without BOM strings.
        public void TestUTF16WithoutBOM()
        {
            Test(EncodingScheme.BigEndianUnicode);
        }
    }
}
