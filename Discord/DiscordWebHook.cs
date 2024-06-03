using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace DetectDetect.Discord {
    internal class DiscordWebHook {
        const string SCHEME_TEXT = "text/plain";
        const string SCHEME_PNG = "image/png";
        const string SCHEME_MESSGAGE = "application/json";
        const string SCHEME_UNKNOWN = "*/*";

        private readonly string _webhookUrl;
        private readonly string _tag;
        private readonly string _id;

        public string GetID() => _id;
        public string GetTAG() => _tag;
        public string GetWEBHook() => _webhookUrl;
        public bool HasLastException() => _lastMessageError != null;


        private Exception _lastMessageError;

        public DiscordWebHook(string webhookUrl, string tag, string id) {
            _webhookUrl = webhookUrl;
            _tag = tag;
            _id = id;
        }

        public string GetHeader(string kind)
            => new StringBuilder()
                .Append("   =============[HEADER]=============\n")
                .Append("[TAG][").Append(_tag).Append("]\n")
                .Append("[UID][").Append(_id).Append("]\n")
                .Append("[TSK][").Append(kind).Append("]\n")
                .Append("   =============[DATA]=============\n\n")
                .ToString();

        public async Task<HttpStatusCode> SendTextDocument(string file, string message = null)
            => await SendFile(file, SCHEME_TEXT, message);

        public async Task<HttpStatusCode> SendTextDocument(byte[] data, string fileName, string message = null)
            => await SendFile(data, fileName, SCHEME_TEXT, message);

        public async Task<HttpStatusCode> SendImagePNG(string file, string message = null)
            => await SendFile(file, SCHEME_PNG, message);

        public async Task<HttpStatusCode> SendImagePNG(byte[] data, string fileName, string message = null)
            => await SendFile(data, fileName, SCHEME_PNG, message);

        public async Task<HttpStatusCode> SendFile(string file, string message = null)
            => await SendFile(file, SCHEME_UNKNOWN, message);

        public async Task<HttpStatusCode> SendFile(string file, string typeScheme, string message = null) {
            try {
                if (!string.IsNullOrEmpty(file) && File.Exists(file)) {
                    string fileName = new FileInfo(file).Name;
                    byte[] data = File.ReadAllBytes(file);
                    return await SendFile(data, fileName, typeScheme, message);
                }
            } catch (Exception e) { _lastMessageError = e; }
            return HttpStatusCode.NotFound;
        }

        public async Task<HttpStatusCode> SendFile(byte[] data, string fileName, string typeScheme, string message = null) {
            try {
                if(data != null &&  data.Length > 0) {
                    Console.WriteLine($"[+] Sending Bytes of Data Size({data.Length}) FileName: {fileName}");
                    message = message ?? string.Empty;
                    using (var client = new HttpClient()) {
                        using (var content = new MultipartFormDataContent()) {
                            content.Add(new StringContent(message), "content");
                            var fileContent = new ByteArrayContent(data);
                            fileContent.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue(typeScheme);
                            content.Add(fileContent, "file", fileName);
                            var response = await client.PostAsync(_webhookUrl, content);
                            return response.StatusCode;
                        }
                    }
                }
            } catch (Exception e) { _lastMessageError = e; }
            return HttpStatusCode.BadRequest;
        }

        public async Task<HttpStatusCode> SendMessage(string message, string headerKind = null) {
            try {
                var header = !string.IsNullOrEmpty(headerKind) ? GetHeader(headerKind) : string.Empty;
                foreach (var msg in DiscordUtil.SplitMessage(message, header)) {
                    using (var client = new HttpClient()) {
                        var payload = new DiscordPayload { content = msg };
                        var jsonPayload = DiscordUtil.SerializeToJson(payload);
                        var httpContent = new StringContent(jsonPayload, Encoding.UTF8, SCHEME_MESSGAGE);
                        Thread.Sleep(500);
                        var response = await client.PostAsync(_webhookUrl, httpContent);
                        return response.StatusCode;
                    }
                }
            } catch (Exception e) { _lastMessageError = e; }
            return HttpStatusCode.BadRequest;
        }

        public static bool IsRateLimitedResponse(HttpStatusCode response) => IsRateLimitedResponse((int)response);
        public static bool IsRateLimitedResponse(int code) => code == 429;
        public static bool IsSuccessful(HttpStatusCode httpStatusCode) => httpStatusCode == HttpStatusCode.OK;
    }
}
