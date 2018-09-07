using System;
using System.Net;
using System.IO;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;
namespace healthagram.Server
{
    public class ReciveFromServerJson
    {
        public ReciveFromServerJson()
        {
        }
        public JObject Recive(string url)
        {
            try
            {
                var httpWebRequest = (HttpWebRequest)WebRequest.Create(url);
                httpWebRequest.ContentType = "application/json";
                httpWebRequest.Method = "GET";

                JObject jsonDoc;
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    jsonDoc = JObject.Parse(streamReader.ReadToEnd());
                }  

                return jsonDoc;
            }
            catch(Exception ex)
            {
                return null;
            }
            //using (WebResponse response = await httpWebRequest.GetResponseAsync())
            //{
            //    // Get a stream representation of the HTTP web response:
            //    using (Stream stream = response.GetResponseStream())
            //    {
            //        // Use this stream to build a JSON document object:
            //        JsonValue jsonDoc = await Task.Run(() => JsonObject.Load(stream));
            //        Console.Out.WriteLine("Response: {0}", jsonDoc.ToString());

            //        // Return the JSON document:
            //        return jsonDoc;
            //    }
            //}
        }
    }
}
