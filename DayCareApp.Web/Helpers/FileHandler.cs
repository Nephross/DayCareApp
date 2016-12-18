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
        string serverPath;
        
        public string SaveImage(FileUploadPacket uploadFile, string inputServerPath)
        {
            string profileImagePath = "empty";
            serverPath = inputServerPath;

            if (uploadFile == null)
            {
                profileImagePath = SaveDefaultImage();
            }
            else
            {                
                string filePath = SaveImage(uploadFile.UpFile.InputStream, 400);

                profileImagePath = filePath + ".jpg";
            }
                      
            return profileImagePath;
        }

        private string SaveDefaultImage()
        {
            byte[] defaultImage = File.ReadAllBytes(serverPath + "\\UserFiles\\DefaultProfileImage.jpg");
            Stream inputStream = new MemoryStream(defaultImage);
            string filePath = SaveImage(inputStream, 400);

            return filePath + ".jpg";
        }

        private string SaveImage(Stream imageBuffer, int maxSideSize)
        {
            string filePath = getFilePath();
            string targetPath = getTargetPath(filePath);

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

            newImage.Save(targetPath + ".jpg", ImageFormat.Jpeg);
            image.Dispose();
            newImage.Dispose();

            return filePath;
        }

        
        //Generates a unique filepath.
        private string getFilePath()
        {
            string fileFolder = "\\UserFiles\\ProfilePictures\\";
            string targetFolder = serverPath + fileFolder;

            if (!Directory.Exists(targetFolder))
            {
                Directory.CreateDirectory(targetFolder);
            }
            Guid fileName = Guid.NewGuid();
          
            return fileFolder + fileName.ToString();
        }

        private string getTargetPath(string filePath)
        {
            return serverPath + filePath;
        }

        public void deleteImage(string inputServerpath, string imagePath)
        {
            serverPath = inputServerpath;

            if (System.IO.File.Exists(getTargetPath(imagePath)))
            {
                System.IO.File.Delete(getTargetPath(imagePath));
            }
        }

    }
}