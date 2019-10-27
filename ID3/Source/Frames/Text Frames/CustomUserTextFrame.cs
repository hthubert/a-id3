using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
    [global::System.Serializable]
    public class CustomUserTextFrame : TextFrame
    {
        public CustomUserTextFrame(string text)
            : base(text)
        {
        }

        public static Achamenes.ID3.Frames.Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
        {
            if (version == ID3v2MajorVersion.Version2 && frameID == "TXX") {
                return new Parsers.CustomUserTextFrameParser();
            }
            if ((version == ID3v2MajorVersion.Version3 || version == ID3v2MajorVersion.Version4) && frameID == "TXXX") {
                return new Parsers.CustomUserTextFrameParser();
            }
            return null;
        }

        public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
        {
            if (version == ID3v2MajorVersion.Version2) {
                return new Writers.TextFrameWriter(this, "TXX", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
            }
            if (version == ID3v2MajorVersion.Version3 || version == ID3v2MajorVersion.Version4) {
                return new Writers.TextFrameWriter(this, "TXXX", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
            }
            return null;
        }
    }
}