using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DetectDetect
{
    internal static class StrUtils {

        public static string CharMultiply(char c, int count) {
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < count; i++) {
                sb.Append(c);
            }

            return sb.ToString();
        }

        public static string BytesToHexString(byte[] bytes)
        {
            if (bytes == null)
                throw new ArgumentNullException(nameof(bytes));

            char[] c = new char[bytes.Length * 2];
            byte b;
            for (int i = 0; i < bytes.Length; i++)
            {
                b = ((byte)(bytes[i] >> 4));
                c[i * 2] = (char)(b > 9 ? b + 0x37 : b + 0x30);
                b = ((byte)(bytes[i] & 0xF));
                c[i * 2 + 1] = (char)(b > 9 ? b + 0x37 : b + 0x30);
            }
            return new string(c);
        }

        public static string TrimEx(this string str, char removeChar, bool removeFromStart, bool removeFromEnd) {
            StringBuilder sb = new StringBuilder();
            if(removeFromStart && str.StartsWith(removeChar.ToString())) {
                char[] chrs = str.ToCharArray();
                bool isOut = false;
                for (int i = 0; i < chrs.Length; i++) {
                    if (!isOut && chrs[i] == removeChar)
                        continue;

                    isOut = true;
                    sb.Append(chrs[i]);
                }
            }

            string newStr = sb.ToString();
            sb.Clear();
            if (string.IsNullOrEmpty(newStr)) return newStr;
            if(removeFromEnd && newStr.EndsWith(removeChar.ToString())) {
                char[] chrs = str.ToCharArray();
                bool isOut = false;
                for (int i = chrs.Length; i-- > 0;) {
                    if (!isOut && chrs[i] == removeChar)
                        continue;

                    isOut = true;
                    sb.Append(chrs[i]);
                }

                newStr = sb.ToString();
                chrs = newStr.ToCharArray();
                Array.Reverse(chrs);
                return new string(chrs);
            } return newStr;
        }

        public unsafe static string FixedCharToString(char* fixedCharArray, int maxLength) {
            int length = 0;
            while (length < maxLength && fixedCharArray[length] != '\0') {
                length++;
            }
            return new string(fixedCharArray, 0, length);
        }

        //public static string ListToString(List<string> )
    }
}
