using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore.Objects
{
    internal class WmiUserAccounts : IWmiObject
    {
        private List<WmiUserAccount> _accounts = new List<WmiUserAccount>();

        public static WmiUserAccounts Get()
        {
            WmiUserAccounts accounts = new WmiUserAccounts();
            IEnumerable<ManagementObject> objs = WmiController.GetObjects("Win32_UserAccount");
            if (objs == null) return accounts;
            accounts.Init(objs);
            return accounts;
        }

        public virtual void Init(IEnumerable<ManagementObject> elements)
        {
            foreach (ManagementObject obj in elements)
            {
                WmiUserAccount p = new WmiUserAccount();
                p.Init(obj);
                _accounts.Add(p);
            }
        }

        public virtual void Init(ManagementObject obj) { }

        public virtual string ConvertToString()
        {
            StringBuilder sb = new StringBuilder();
            foreach (WmiUserAccount P in _accounts)
            {
                sb.Append(P.ConvertToString());
            }

            return sb.ToString();
        }
    }
}
