using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
    [global::System.Serializable]
    public class InitialKeyTextFrame : TextFrame
    {
        public InitialKeyTextFrame(string text)
            : base(text)
        {
        }

        public static Achamenes.ID3.Frames.Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
        {
            if (version == ID3v2MajorVersion.Version2 && frameID == "TKE") {
                return new Parsers.InitialKeyTextFrameParser();
            }
            if ((version == ID3v2MajorVersion.Version3 || version == ID3v2MajorVersion.Version4) && frameID == "TKEY") {
                return new Parsers.InitialKeyTextFrameParser();
            }
            return null;
        }

        public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
        {
            if (version == ID3v2MajorVersion.Version2) {
                return new Writers.TextFrameWriter(this, "TKE", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
            }
            if (version == ID3v2MajorVersion.Version3 || version == ID3v2MajorVersion.Version4) {
                return new Writers.TextFrameWriter(this, "TKEY", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
            }
            return null;
        }
    }
}