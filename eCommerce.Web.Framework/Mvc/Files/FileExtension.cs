using System;
using System.Web;

namespace eCommerce.Web.Framework.Mvc.Files
{
    public static class FileExtension
    {
        /// <summary>
        /// get file bytes
        /// </summary>
        /// <param name="file"></param>
        /// <param name="callback"></param>
        /// <returns></returns>
        public static byte[] GetFileBytes(this HttpPostedFileBase file, Action<byte[]> callback)
        {
            var uploadedBytes = new byte[file.ContentLength];
            file.InputStream.Read(uploadedBytes, 0, file.ContentLength);
            callback(uploadedBytes);
            return uploadedBytes;
        }

        /// <summary>
        /// Upload file and save
        /// </summary>
        /// <param name="file"></param>
        /// <param name="fileName"></param>
        public static void Upload(this HttpPostedFileBase file, string fileName)
        {
            file.SaveAs(fileName);
        }
    }
}
