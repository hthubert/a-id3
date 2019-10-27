using SixLabors.ImageSharp.Formats;

namespace Achamenes.ID3.Utils
{
    public class ImagingHelpers
    {
        public static string ImageFormatToMimeType(IImageFormat imageFormat)
        {
            //foreach (ImageCodecInfo codec in ImageCodecInfo.GetImageDecoders()) 
            //{
            //	if (codec.FormatID == imageFormat.Guid) 
            //	{
            //		return codec.MimeType;
            //	}
            //}
            //return "application/octet-stream";
            return imageFormat.DefaultMimeType;
        }

        // Taken from http://www.java2s.com/Code/CSharp/File-Stream/ImageFormattoExtension.htm
        // And modified.
        public static string ImageFormatToExtension(IImageFormat imageFormat)
        {
            switch (imageFormat.Name) {
                case "JPEG":
                    return "JPG";
                case "":
                    return "";
                default:
                    return imageFormat.Name;
            }
            //         if (imageFormat.Equals(BmpFormat.Bmp))
            //{
            //	return "BMP";
            //}
            //else if (imageFormat.Equals(ImageFormat.MemoryBmp))
            //{
            //	return "BMP";
            //}
            //else if (imageFormat.Equals(ImageFormat.Wmf))
            //{
            //	return "EMF";
            //}
            //else if (imageFormat.Equals(ImageFormat.Wmf))
            //{
            //	return "WMF";
            //}
            //else if (imageFormat.Equals(ImageFormat.Gif))
            //{
            //	return "GIF";
            //}
            //else if (imageFormat.Equals(ImageFormat.Jpeg))
            //{
            //	return "JPG";
            //}
            //else if (imageFormat.Equals(ImageFormat.Png))
            //{
            //	return "PNG";
            //}
            //else if (imageFormat.Equals(ImageFormat.Tiff))
            //{
            //	return "TIF";
            //}
            //else if (imageFormat.Equals(ImageFormat.Exif))
            //{
            //	return "EXF";
            //}
            //else if (imageFormat.Equals(ImageFormat.Icon))
            //{
            //	return "ICO";
            //}
            //return "";
        }
    }
}

