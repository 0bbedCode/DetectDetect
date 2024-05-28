using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.Info {
    internal class NetCard {
        private NetworkInterface _n;

        internal int Index;
        internal string Name;
        internal string ID;
        internal string Description;
        internal string MAC;
        internal string DnsSuffix;

        internal List<string> DNS = new List<string>();
        internal List<string> Gateway = new List<string>();
        internal List<string> Address = new List<string>();
        internal List<AddressFamily> AddressFamily = new List<AddressFamily>();

        internal NetCard(NetworkInterface neti) :this(neti, 0) { }
        internal NetCard(NetworkInterface neti, int index) {
            Index = index;
            _n = neti;
            init(neti);
        }

        private void init(NetworkInterface neti) {
            try {
                Name = neti.Name;
                ID = neti.Id;
                Description = neti.Description;
                MAC = neti.GetPhysicalAddress().ToString();

                var ipProps = neti.GetIPProperties();
                if(ipProps != null) {
                    DnsSuffix = ipProps.DnsSuffix;
                    if(ipProps.DnsAddresses != null) {
                        foreach(var dns in ipProps.DnsAddresses) 
                            DNS.Add(dns.ToString());
                    }

                    if(ipProps.GatewayAddresses != null) {
                        foreach(var gate in ipProps.GatewayAddresses)
                            Gateway.Add(gate.Address.ToString());
                    }

                    if(ipProps.UnicastAddresses != null) {
                        foreach(var addr in ipProps.UnicastAddresses) {
                            Address.Add(addr.Address.ToString());
                            AddressFamily.Add(addr.Address.AddressFamily);
                        }
                    }
                }
            }catch(Exception e) {
                Console.WriteLine("Network Card Error: " + e.Message);
            }
        }

        internal static List<NetCard> GetCards() {
            List<NetCard> cards = new List<NetCard>();
            int i = 1;
            foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces()) {
                cards.Add(new NetCard(nic, i));
                i++;
            } return cards;
        }

        public static string GetExternalIPAddress() {
            string result = string.Empty;
            try {
                using (var client = new WebClient()) {
                    client.Headers["User-Agent"] =
                    "Mozilla/4.0 (Compatible; Windows NT 5.1; MSIE 6.0) " +
                    "(compatible; MSIE 6.0; Windows NT 5.1; " +
                    ".NET CLR 1.1.4322; .NET CLR 2.0.50727)";

                    try {
                        byte[] arr = client.DownloadData("http://checkip.amazonaws.com/");

                        string response = System.Text.Encoding.UTF8.GetString(arr);

                        result = response.Trim();
                    } catch (WebException) {
                    }
                }
            } catch {
            }

            if (string.IsNullOrEmpty(result)) {
                try {
                    result = new WebClient().DownloadString("https://ipinfo.io/ip").Replace("\n", "");
                } catch {
                }
            }

            if (string.IsNullOrEmpty(result)) {
                try {
                    result = new WebClient().DownloadString("https://api.ipify.org").Replace("\n", "");
                } catch {
                }
            }

            if (string.IsNullOrEmpty(result)) {
                try {
                    result = new WebClient().DownloadString("https://icanhazip.com").Replace("\n", "");
                } catch {
                }
            }

            if (string.IsNullOrEmpty(result)) {
                try {
                    result = new WebClient().DownloadString("https://wtfismyip.com/text").Replace("\n", "");
                } catch {
                }
            }

            if (string.IsNullOrEmpty(result)) {
                try {
                    result = new WebClient().DownloadString("http://bot.whatismyipaddress.com/").Replace("\n", "");
                } catch {
                }
            }

            if (string.IsNullOrEmpty(result)) {
                try {
                    string url = "http://checkip.dyndns.org";
                    System.Net.WebRequest req = System.Net.WebRequest.Create(url);
                    System.Net.WebResponse resp = req.GetResponse();
                    System.IO.StreamReader sr = new System.IO.StreamReader(resp.GetResponseStream());
                    string response = sr.ReadToEnd().Trim();
                    string[] a = response.Split(':');
                    string a2 = a[1].Substring(1);
                    string[] a3 = a2.Split('<');
                    result = a3[0];
                } catch (Exception) {
                }
            }

            return result;
        }

        public override string ToString() {
            var sb = new StringBuilder()
                .AppendLine($"[Index][{Index}]")
                .AppendLine($"[Name][{Name}]")
                .AppendLine($"[ID][{ID}]")
                .AppendLine($"[Desc][{Description}]")
                .AppendLine($"[Mac][{MAC}]")
                .AppendLine($"[DnsSuffix][{DnsSuffix}]");

            foreach (var s in DNS)  sb.AppendLine($"[DNS][{s}]");
            foreach (var s in Gateway)  sb.AppendLine($"[Gateway][{s}]");
            for(int i = 0; i < Address.Count; i++) {
                string addr = Address[i];
                AddressFamily faml = AddressFamily[i];
                sb.AppendLine($"[Address][{addr}][{Enum.GetName(typeof(AddressFamily), faml)}]");
            }  return sb.ToString();
        }
    }
}
