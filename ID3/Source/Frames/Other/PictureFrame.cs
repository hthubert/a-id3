using System;
using System.Collections.Generic;
using System.Text;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Formats;

namespace Achamenes.ID3.Frames
{
    [global::System.Serializable]
    public class PictureFrame : Frame
    {
        private string _description;
        public string Description
        {
            get {
                return _description;
            }
            set {
                _description = value;
            }
        }

        private byte[] _rawData = null;
        public byte[] RawData
        {
            get {
                if (this._rawData == null) {
                    this.LoadRawDataFromImage();
                }
                return this._rawData;
            }
        }

        private Image _image;
        public Image Picture
        {
            get {
                return _image;
            }
        }

        private IImageFormat _imageFormat;
        public IImageFormat ImageFormat
        {
            get {
                return _imageFormat;
            }
        }

        private PictureType _pictureType;
        public PictureType PictureType
        {
            get {
                return _pictureType;
            }
            set {
                _pictureType = value;
            }
        }

        protected void LoadRawDataFromImage()
        {
            System.IO.MemoryStream memoryBuffer = new System.IO.MemoryStream();
            //this._image.Save(memoryBuffer, this._image.);
            this._image.Save(memoryBuffer, _imageFormat);
            this._rawData = memoryBuffer.GetBuffer();
            memoryBuffer.Close();
        }

        protected PictureFrame(string description, PictureType pictureType)
            : base()
        {
            this.Description = description;
            this.PictureType = pictureType;
        }

        public PictureFrame(string fileName, string description, PictureType pictureType)
            : this(description, pictureType)
        {
            this._image = Image.Load(fileName, out _imageFormat);
        }

        public PictureFrame(Image image, string description, PictureType pictureType)
            : this(description, pictureType)
        {
            if (image == null) {
                throw new ArgumentNullException("The passed image object can not be null.");
            }
            this._image = image;
        }

        public PictureFrame(byte[] raw_data, Image image, string description, PictureType pictureType)
            : this(image, description, pictureType)
        {
            if (raw_data == null) {
                throw new ArgumentNullException("The passed image raw data can not be null.");
            }
            this._rawData = raw_data;
        }


        public static Achamenes.ID3.Frames.Parsers.FrameParser CreateParser(ID3v2MajorVersion version, string frameID)
        {
            if (version == ID3v2MajorVersion.Version2) {
                if (frameID == "PIC") {
                    return new Parsers.PictureFrameParserM2();
                }
            }
            else if (version == ID3v2MajorVersion.Version3 || version == ID3v2MajorVersion.Version4) {
                if (frameID == "APIC") {
                    return new Parsers.PictureFrameParserM3and4();
                }
            }
            return null;
        }

        public override Achamenes.ID3.Frames.Writers.FrameWriter CreateWriter(ID3v2MajorVersion version, EncodingScheme encoding)
        {
            if (version == ID3v2MajorVersion.Version2) {
                return new Writers.PictureFrameWriterM2(this, "PIC", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
            }
            else if (version == ID3v2MajorVersion.Version3 || version == ID3v2MajorVersion.Version4) {
                return new Writers.PictureFrameWriterM3And4(this, "APIC", Writers.FrameHeaderWriter.CreateFrameHeaderWriter(version), encoding);
            }
            else {
                return null;
            }
        }
    }
}
