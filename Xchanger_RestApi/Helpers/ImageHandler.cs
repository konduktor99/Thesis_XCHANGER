using Microsoft.AspNetCore.Hosting;
using SixLabors.ImageSharp;
using System;



namespace Xchanger_RestApi.Helpers
{
    public class ImageHandler
    {
        public static IWebHostEnvironment _env;

        public ImageHandler( IWebHostEnvironment env)
        {
            
            _env = env;
        }



        public static void GetImgDims(Image img, double mWidth, double mHeight, ref int outWidth, ref int outHeight)
        {
            double heightR = 0d;
            double widthR = 0d;
            if(img.Height > mHeight )
            {
                heightR = (double)img.Height / mHeight;
            }
            if (img.Width > mWidth)
            {
                widthR = (double)img.Width / mWidth;
            }
             var r = Math.Max(widthR, heightR);
            if(r != 0)
            {
                outHeight = (int)(img.Height / r);
                outWidth = (int)(img.Width / r);
            }
            else
            {
                outHeight = img.Height;
                outWidth = img.Width;
            }
            
        }
    }
}
