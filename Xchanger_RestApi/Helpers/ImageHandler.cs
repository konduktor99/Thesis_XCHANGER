using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace Xchanger_RestApi.Helpers
{
    public class ImageHandler
    {
        public static IWebHostEnvironment _env;

        public ImageHandler( IWebHostEnvironment env)
        {
            
            _env = env;
        }
        public static void SaveImages(IFormFile[] files, int itemId)
        {
            int counter = 1;
            if (files != null)
            {
                var path = Path.Combine(_env.WebRootPath, "itemPics", itemId.ToString());
                if (Directory.Exists(path))
                    Directory.Delete(path, true);
                Directory.CreateDirectory(path);
                foreach (var file in files)
                {
                    string picName = "\\" + counter + "_itemPic_" + itemId + ".jpg";

                    if (System.IO.File.Exists(path + picName))
                        System.IO.File.Delete(path + picName);

                    using (FileStream fs = System.IO.File.Create(path + picName))
                    {
                        file.CopyTo(fs);
                        fs.Flush();
                    }
                    counter++;
                }

            }

        }
        public static List<byte[]> LoadImages(int itemId)
        {

            var imgBytesList = new List<byte[]>();
            var itemFolderPath = Path.Combine(_env.WebRootPath, "itemPics", itemId.ToString());

            if (Directory.Exists(itemFolderPath))
                foreach (string file in Directory.EnumerateFiles(itemFolderPath, "*.jpg"))
                {
                    imgBytesList.Add(System.IO.File.ReadAllBytes(file));
                }
            return imgBytesList;
        }

        public static byte[] LoadMainImage(int itemId)
        {

            byte[] imgBytes = null;
            var itemFolderPath = Path.Combine(_env.WebRootPath, "itemPics", itemId.ToString());

            string file;
            if (Directory.Exists(itemFolderPath))
            {
                file = Directory.EnumerateFiles(itemFolderPath, "*.jpg").FirstOrDefault();
                if (file != null)
                    imgBytes = System.IO.File.ReadAllBytes(file);
            }


            return imgBytes;
        }
    }
}
