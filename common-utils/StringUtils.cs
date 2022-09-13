using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bins_PcQuickStart
{
    class StringUtils
    {
        // 从 "C:\\_skpclog"  获取到 _skpclog
        public static string getLastText(string str)
        {
            string[] arr = str.Split('\\');
            if (arr == null || arr.Length == 0)
            {
                return str;
            }
            else
            {
                int length = arr.Length;
                return arr[length - 1];
            }
        }

        // 从 "C:\\_skpclog"  获取到 C:\
        public static string geBeforetLastText(string str, string subStr)
        {
            int index = str.IndexOf(subStr);
            return str.Substring(0, index);
        }

    }
}
