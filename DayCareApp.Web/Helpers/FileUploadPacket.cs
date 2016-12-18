using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DayCareApp.Web.Helpers
{
    public class FileUploadPacket
    {
        public FileUploadPacket() { }

        public int FileID { get; set; }

        public HttpPostedFileBase UpFile { get; set; }
    }
}