using System;
using System.Collections.Generic;
using System.Text;
using Achamenes.ID3.Fields;
using SixLabors.ImageSharp;

namespace Achamenes.ID3.Frames.Parsers
{
    class PictureFrameParserM2 : FrameParser
    {
        protected override Frame ParseFrame(byte[] data)
        {
            int place = 0;

            SingleByteField encodingField = new SingleByteField();
            place += encodingField.Parse(data, place);

            FixedLengthAsciiTextField imageFormatField = new FixedLengthAsciiTextField(3);
            place += imageFormatField.Parse(data, place);

            SingleByteField pictureTypeField = new SingleByteField();
            place += pictureTypeField.Parse(data, place);

            TextField descriptionField = TextField.CreateTextField(true, (EncodingScheme)encodingField.Value);
            place += descriptionField.Parse(data, place);

            BinaryField dataField = new BinaryField();
            place += dataField.Parse(data, place);


            System.IO.MemoryStream memoryImageBuffer = new System.IO.MemoryStream(dataField.Data);

            try {
                var image = Image.Load(memoryImageBuffer);
                return new PictureFrame(dataField.Data, image, descriptionField.Text, (PictureType)pictureTypeField.Value);
            }
            catch (ArgumentException) {
                throw new FrameParsingException("Unrecognized picture format found in Picture frame.");
            }
        }
    }

    class PictureFrameParserM3and4 : FrameParser
    {
        protected override Frame ParseFrame(byte[] data)
        {
            int place = 0;

            SingleByteField encodingField = new SingleByteField();
            place += encodingField.Parse(data, place);

            TextField imageFormatField = TextField.CreateTextField(true, EncodingScheme.Ascii);
            place += imageFormatField.Parse(data, place);

            SingleByteField pictureTypeField = new SingleByteField();
            place += pictureTypeField.Parse(data, place);

            TextField descriptionField = TextField.CreateTextField(true, (EncodingScheme)encodingField.Value);
            place += descriptionField.Parse(data, place);

            BinaryField dataField = new BinaryField();
            place += dataField.Parse(data, place);


            System.IO.MemoryStream memoryImageBuffer = new System.IO.MemoryStream(dataField.Data);
            try {
                Image image = Image.Load(memoryImageBuffer);
                return new PictureFrame(dataField.Data, image, descriptionField.Text, (PictureType)pictureTypeField.Value);
            }
            catch (ArgumentException) {
                throw new FrameParsingException("Unrecognized picture format found in Picture frame.");
            }
        }
    }
}


