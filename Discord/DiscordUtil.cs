using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Json;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Discord {
    [DataContract]
    internal class DiscordPayload {
        [DataMember(Name = "content")]
        public string content { get; set; }
    }

    internal class DiscordUtil {
        internal static string SerializeToJson(DiscordPayload payload) {
            try {
                var serializer = new DataContractJsonSerializer(typeof(DiscordPayload));
                using (var memoryStream = new MemoryStream()) {
                    serializer.WriteObject(memoryStream, payload);
                    return Encoding.UTF8.GetString(memoryStream.ToArray());
                }
            } catch {
                return "{ \"content\":\"\" }";
            }
        }

        internal static IEnumerable<string> SplitMessage(string message, string header) {
            int maxSize = 1900;
            int sizeCanTake = maxSize - header.Length;
            if (message.Length + header.Length < maxSize) {
                yield return header + message;
            } else {
                int leftToSend = message.Length;
                int taken = 0;
                while (leftToSend > 0) {
                    int szTake = leftToSend > sizeCanTake ? sizeCanTake : leftToSend;
                    string block = header + message.Substring(taken, szTake);
                    yield return block;
                    taken += szTake;
                    leftToSend -= szTake;
                }
            }
        }
    }
}
