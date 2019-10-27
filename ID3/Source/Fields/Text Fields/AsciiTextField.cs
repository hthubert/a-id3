using System;
using System.Collections.Generic;
using System.Text;

namespace Achamenes.ID3.Fields
{
    class AsciiTextField : TextField
    {
        public AsciiTextField(bool isNullTerminated)
            : base(isNullTerminated)
        {

        }

        public AsciiTextField(string text)
            : base(text)
        {

        }

        public override int Length
        {
            get {
                return Encoding.GetEncoding("ISO-8859-1").GetByteCount(this.Text) + 1;
            }
        }

        public override int Parse(byte[] data, int offset)
        {
            base.Parse(data, offset);
            if (this.IsNullTerminated) {
                int nullTerminator = -1;
                for (int i = offset; i < data.Length; i++) {
                    if (data[i] == 0) {
                        nullTerminator = i;
                        break;
                    }
                }
                if (nullTerminator == -1 || nullTerminator == data.Length) // null-terminator was not found.
                {
                    throw new FieldParsingException(this.GetType(), "Invalid ASCII text field: not null terminated.");
                }

                this.Text = Encoding.GetEncoding("ISO-8859-1").GetString(data, offset, nullTerminator - offset);
                return nullTerminator - offset + 1;
            }
            else {
                this.Text = System.Text.Encoding.GetEncoding("ISO-8859-1").GetString(data, offset, data.Length - offset);
                int nullTerminator = this.Text.IndexOf('\0');
                if (nullTerminator != -1) {
                    this.Text = this.Text.Substring(0, nullTerminator);
                }
                return data.Length - offset;
            }
        }

        public override void WriteToStream(System.IO.Stream stream)
        {
            byte[] buffer = Encoding.GetEncoding("ISO-8859-1").GetBytes(this.Text);
            stream.Write(buffer, 0, buffer.Length); // char data
            stream.WriteByte(0);                    // null terminator
        }

        public override EncodingScheme EncodingScheme
        {
            get {
                return EncodingScheme.Ascii;
            }
        }
    }
}

