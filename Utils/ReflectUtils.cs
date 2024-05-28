using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace DetectDetect.Utils {
    internal class ReflectUtils {
        internal static string GetPropertyValue(PropertyInfo property) {
            try {
                return Convert.ToString(property.GetValue(null));
            } catch (Exception e) {
                Console.WriteLine("Failed to Get Property Value: " + e);
                return "";
            }
        }

        internal static string GetFieldValue(FieldInfo field) {
            try {
                return Convert.ToString(field.GetValue(null));
            } catch(Exception e) {
                Console.WriteLine("Failed to Get Field Value: " + e);
                return "";
            }
        }

        internal static List<PropertyInfo> GetPublicStaticProperties(Type type) {
            List<PropertyInfo> props = new List<PropertyInfo>();
            try {
                var flds = type.GetProperties(BindingFlags.Public | BindingFlags.Static);
                Console.WriteLine("Props Size:" + flds.Count());
                foreach (var f in flds) { 
                    if(f.PropertyType == typeof(string))
                        props.Add(f);
                    else if(f.PropertyType == typeof(bool))
                        props.Add(f);
                    else if(f.PropertyType == typeof(int))
                        props.Add(f);
                    else if(f.PropertyType == typeof(long))
                        props.Add(f);
                }

                return props;
            }catch(Exception e) {
                Console.WriteLine("Failed to Resolve Fields: " + e);
                return props;
            }
        }

        internal static List<Type> GetTypesInheritInterface(string path, Type type) {
            List<Type> list = new List<Type>();
            try {
                Assembly assembly = Assembly.GetExecutingAssembly();
                foreach (var item in assembly.GetTypes()) 
                    if(path == null || item.Namespace.Equals(path, StringComparison.OrdinalIgnoreCase)) {
                        Type[] interfaces = item.GetInterfaces();
                        if(interfaces.Length > 0) {
                            if (interfaces[0] == type) {
                                list.Add(item);
                            }
                        }
                    }
                return list;
            }catch(Exception e) {
                Console.WriteLine($"Failed to Resolve Types: " + e);
                return list;
            }
        }
    }
}
