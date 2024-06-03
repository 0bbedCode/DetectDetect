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
