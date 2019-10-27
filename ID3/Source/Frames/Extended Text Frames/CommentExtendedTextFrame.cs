using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Frames
{
    [global::System.Serializable]
    public class CommentExtendedTextFrame : ExtendedTextFrame
    {
        public CommentExtendedTextFrame(string text, string description, LanguageCode language)
            : base(text, description, language)
        {
        }

        public static Achamenes.ID3.Frames.Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
        {
            if (version == ID3v2MajorVersion.Version2 && frameID == "COM") {
                return new Parsers.CommentExtendedTextFrameParser();
            }
            if ((version == ID3v2MajorVersion.Version3 || version == ID3v2MajorVersion.Version4) && frameID == "COMM") {
                return new Parsers.CommentExtendedTextFrameParser();
            }
            return null;
        }

        public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
        {
            if (version == ID3v2MajorVersion.Version2) {
                return new Writers.ExtendedTextFrameWriter(this, "COM", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
            }
            if (version == ID3v2MajorVersion.Version3 || version == ID3v2MajorVersion.Version4) {
                return new Writers.ExtendedTextFrameWriter(this, "COMM", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
            }
            return null;
        }
    }
}

