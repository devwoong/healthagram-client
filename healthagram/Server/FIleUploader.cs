using System;
using System.Collections.Generic;
using System.Net;
using System.IO;
using System.Text;
using System.Security.Cryptography;
namespace healthagram.Server
{
    public class FIleUploader
    {
        public FIleUploader()
        {
        }
        public string UploadFilesToRemoteUrl(string url, List<byte[]> files)
        {
            string boundary = "-------------------------" + DateTime.Now.Ticks.ToString("x");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "multipart/form-data; boundary" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;

            Stream memStream = new MemoryStream();
            var boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            var endBoundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--");

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                "Content-Type: multipart/form-data\r\n\r\n";
            for (int i = 0; i < files.Count; i++)
            {
                memStream.Write(boundarybytes, 0, boundarybytes.Length);
                MD5 fileHash = MD5.Create();
                byte[] hasingName = fileHash.ComputeHash(files[i]);
                StringBuilder hashedName = new StringBuilder();

                foreach (byte b in hasingName)
                    hashedName.Append(hasingName[i].ToString("X2"));
                
                var header = string.Format(headerTemplate, "uplTheFile", hashedName);
                var headerBytes = Encoding.UTF8.GetBytes(header);

                memStream.Write(headerBytes, 0, headerBytes.Length);

                using(var ByteStream = new MemoryStream(files[i]))
                {
                    var buffer = new byte[1024];
                    var bytesRead = 0;
                    while((bytesRead = ByteStream.Read(buffer, 0, buffer.Length)) != 0)
                    {
                        memStream.Write(buffer, 0, bytesRead);
                    }

                }
            }

            memStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            request.ContentLength = memStream.Length;

            using(Stream requestStream = request.GetRequestStream())
            {
                memStream.Position = 0;
                byte[] tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);
                memStream.Close();
                requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            }
            using(var response = request.GetResponse() )
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }


        }


        public string UploadFilesToRemoteUrl(string url, byte[] file)
        {
            string boundary = "-------------------------" + DateTime.Now.Ticks.ToString("x");

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.ContentType = "multipart/form-data; boundary=" + boundary;
            request.Method = "POST";
            request.KeepAlive = true;

            Stream memStream = new MemoryStream();
            var boundarybytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n");
            var endBoundaryBytes = Encoding.ASCII.GetBytes("\r\n--" + boundary + "--");

            string headerTemplate = "Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\n" +
                "Content-Type: multipart/form-data\r\n\r\n";
            memStream.Write(boundarybytes, 0, boundarybytes.Length);
            MD5 fileHash = MD5.Create();
            byte[] hasingName = fileHash.ComputeHash(file);
            StringBuilder hashedName = new StringBuilder();

            foreach (byte b in hasingName)
                hashedName.Append(b.ToString("X2"));

            var header = string.Format(headerTemplate, "uplTheFile", hashedName + ".png");
            var headerBytes = Encoding.UTF8.GetBytes(header);

            memStream.Write(headerBytes, 0, headerBytes.Length);

            using (var ByteStream = new MemoryStream(file))
            {
                var buffer = new byte[1024];
                var bytesRead = 0;
                while ((bytesRead = ByteStream.Read(buffer, 0, buffer.Length)) != 0)
                {
                    memStream.Write(buffer, 0, bytesRead);
                }

            }

            memStream.Write(endBoundaryBytes, 0, endBoundaryBytes.Length);
            request.ContentLength = memStream.Length;

            using (Stream requestStream = request.GetRequestStream())
            {
                memStream.Position = 0;
                byte[] tempBuffer = new byte[memStream.Length];
                memStream.Read(tempBuffer, 0, tempBuffer.Length);
                memStream.Close();
                requestStream.Write(tempBuffer, 0, tempBuffer.Length);
            }
            using (var response = request.GetResponse())
            {
                Stream stream = response.GetResponseStream();
                StreamReader reader = new StreamReader(stream);
                return reader.ReadToEnd();
            }


        }
    }
}
