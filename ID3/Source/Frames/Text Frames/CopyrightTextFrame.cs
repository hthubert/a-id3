using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
    [global::System.Serializable]
    public class CopyrightTextFrame : TextFrame
    {
        public CopyrightTextFrame(string text) : base(text)
        {
        }

        public static Achamenes.ID3.Frames.Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
        {
            if (version == ID3v2MajorVersion.Version2 && frameID == "TCR") {
                return new Parsers.CopyrightTextFrameParser();
            }
            if ((version == ID3v2MajorVersion.Version3 || version == ID3v2MajorVersion.Version4) && frameID == "TCOP") {
                return new Parsers.CopyrightTextFrameParser();
            }
            return null;
        }

        public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
        {
            if (version == ID3v2MajorVersion.Version2) {
                return new Writers.TextFrameWriter(this, "TCR", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
            }
            if (version == ID3v2MajorVersion.Version3 || version == ID3v2MajorVersion.Version4) {
                return new Writers.TextFrameWriter(this, "TCOP", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
            }
            return null;
        }
    }
}

