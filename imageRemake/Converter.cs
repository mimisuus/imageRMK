using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Media.Imaging;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;

namespace imageRemake
{
    class Converter
    {
        String picturesFolder = Environment.GetFolderPath(Environment.SpecialFolder.MyPictures);
        String filePath;
        long size;

        public long SaveAs(String fileType, Bitmap bmp, long quality, String nimi)
        {
                switch (fileType)
                {
                    case ("png"):
                        filePath = picturesFolder + @"\" + "RMK" + nimi + ".png";
                        bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Png);
                        break;
                    case ("jpeg"):
                        filePath = picturesFolder + @"\" + "RMK" + nimi + ".jpeg";
                            // Gets encoder from the jpg image format
                        ImageCodecInfo jpgEncoder = GetEncoder(ImageFormat.Jpeg);
                            // Creates an encoder and set it based on the Encoder.Quality parameter category
                        System.Drawing.Imaging.Encoder mEncoder = System.Drawing.Imaging.Encoder.Quality;
                            // Creates the EncoderParameters with only one EncoderParameter
                            // Then gives it our previously created encoder and desired jpeg quality
                        EncoderParameters mEncoderParams = new EncoderParameters(1);
                        EncoderParameter mEncoderParam = new EncoderParameter(mEncoder, quality);
                        mEncoderParams.Param[0] = mEncoderParam;
                        bmp.Save(filePath ,jpgEncoder,mEncoderParams);
                        break;
                    case ("bmp"):
                        filePath = picturesFolder + @"\" + "RMK" + nimi + ".bmp";
                        bmp.Save(filePath, System.Drawing.Imaging.ImageFormat.Bmp);
                        break;
            }
            size = new FileInfo(filePath).Length;
            return size;
        }

        private static ImageCodecInfo GetEncoder(ImageFormat format)
        {
            ImageCodecInfo[] codecs = ImageCodecInfo.GetImageDecoders();
            foreach (ImageCodecInfo codec in codecs)
            {
                if (codec.FormatID == format.Guid)
                {
                    return codec;
                }
            }
            return null;
        }
    }
}