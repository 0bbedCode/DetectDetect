using System;
using System.Collections.Generic;
using System.Linq;
using System.Management;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect.WMICore
{
    internal static class ManagementExtensions
    {
        /// <summary>
        /// Try Get WMI Instance Propertie Value
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="managementObject"></param>
        /// <param name="propertyName"></param>
        /// <returns></returns>
        internal static T TryGetProperty<T>(this ManagementObject managementObject, string propertyName)
        {
            T result;
            try
            {
                result = (T)((object)managementObject[propertyName]);
            }
            catch (Exception)
            {
                result = default(T);
            }
            return result;
        }
    }
}
