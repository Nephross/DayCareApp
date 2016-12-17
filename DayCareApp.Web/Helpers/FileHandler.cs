using System;
using System.Collections.Generic;
using System.IO;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.Helpers
{
    public class FileHandler
    {
        public string SaveImage(HttpPostedFileBase inputImage, string userId, string serverPath)
        {
            if (inputImage == null) return "empty";


            string filePath = getFilePath(userId, serverPath);
            string targetPath = serverPath + filePath;
            SaveImage(inputImage.InputStream, targetPath, 400);

            return filePath + ".jpg";
        }

        public string SaveDefaultImage(string userId, string serverPath)
        {
            byte[] defaultImage = System.IO.File.ReadAllBytes(serverPath + "\\UserFiles\\DefaultProfileImage.jpg");
            Stream inputStream = new MemoryStream(defaultImage);
            string filePath = getFilePath(userId, serverPath);
            string targetPath = serverPath + filePath;
            SaveImage(inputStream, targetPath, 400);

            return filePath + ".jpg";
        }

        private void SaveImage(Stream imageBuffer, string savePath, int maxSideSize)
        {
            int newWidth;
            int newHeight;
            Image image = Image.FromStream(imageBuffer);
            int oldWidth = image.Width;
            int oldHeight = image.Height;
            int maxSide = oldWidth >= oldHeight ? oldWidth : oldHeight;

            if (maxSide > maxSideSize)
            {
                double coeficient = maxSideSize / (double)maxSide;
                newWidth = Convert.ToInt32(coeficient * oldWidth);
                newHeight = Convert.ToInt32(coeficient * oldHeight);
            }
            else
            {
                newWidth = oldWidth;
                newHeight = oldHeight;
            }
            Bitmap newImage = new Bitmap(image, newWidth, newHeight);
            newImage.Save(savePath + ".jpg", ImageFormat.Jpeg);
            image.Dispose();
            newImage.Dispose();
        }

        

        private string getFilePath(string userId, string serverPath)
        {
            string baseFolder = "\\UserFiles\\";
            string userFolder = "\\" + userId + "\\";
            string targetFolder = serverPath + baseFolder + userFolder;

            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }
            Guid fileName = Guid.NewGuid();
            string fileFolder = baseFolder + userFolder;

            return fileFolder + fileName.ToString();

        }

    }
}