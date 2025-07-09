using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security.Policy;
using System.Text;
using System.Threading.Tasks;

namespace FarcardContract.Extensions
{
    public abstract class InIFile
    {
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern uint GetPrivateProfileSection(string lpAppName, IntPtr lpszReturnBuffer,
        uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern uint GetPrivateProfileSection(string lpAppName, StringBuilder lpszReturnBuffer,
        uint nSize, string lpFileName);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern uint GetPrivateProfileSectionNames(IntPtr lpszReturnBuffer,
      uint nSize, string lpFileName);
        [DllImport("kernel32.dll", CharSet = CharSet.Auto)]
        static extern uint GetPrivateProfileSectionNames(StringBuilder lpszReturnBuffer,
  uint nSize, string lpFileName);

        public static Dictionary<String, String> GetPrivateProfileSection(string fileName, string section)
        {
            var maxSize = UInt16.MaxValue;
            IntPtr buffer = IntPtr.Zero;
            try
            {
                buffer = Marshal.AllocHGlobal(maxSize);
              //  var builder = new StringBuilder(maxSize);

                var count = GetPrivateProfileSection(section, buffer, (uint)maxSize, fileName);
                if (count > 0)
                {
                    var resultData = Marshal.PtrToStringAuto(buffer, (int)count);
                  //  var resultData = builder.ToString();
                    resultData = resultData.Substring(0, resultData.Length - 1);
                    if (resultData.Count() > 0)
                    {
                        var values = resultData.Trim().Split('\0');
                        var r = values.ToDictionary(x => x.IndexOf('=') > 0 ? x.Substring(0, x.IndexOf('=')) : x,
                            x => x.IndexOf('=') > 0 ? x.Substring(x.IndexOf('=') + 1) : "");
                        for (int i = 0; i < r.Count; i++)
                        {
                            var v = r.ElementAt(i);
                            if (string.IsNullOrWhiteSpace(v.Value))
                            {
                                r[v.Key] = null;
                            }
                        }
                        return r;
                    }
                }
            }
            finally
            {
                if (buffer != IntPtr.Zero)
                    Marshal.FreeHGlobal(buffer);
            }
            return default;
        }

        public static string[] GetPrivateProfileSectionNames(string fileName)
        {
            var maxSize = UInt16.MaxValue;
           // var buffer = IntPtr.Zero;
            try
            {
               var buffer = new StringBuilder(maxSize); //  Marshal.AllocHGlobal(maxSize);
                var size = GetPrivateProfileSectionNames(buffer, (uint)maxSize, fileName);
                if (size > 0)
                {
                    var result = buffer.ToString().Split('\0'); // Marshal.PtrToStringAuto(buffer, (int)size).TrimEnd('\0').Split('\0');
                    return result;
                }
            }
            finally
            {
             //   if (buffer != IntPtr.Zero)
               //     Marshal.FreeCoTaskMem(buffer);
            }
            return new string[0];
        }



    }
    public class InIPropAttribute : Attribute
    {
        public string Section { get; private set; }
        public string Name { get; private set; }

        public InIPropAttribute(string section, string name)
        {
            Section = section;
            Name = name;
        }

    }
}
