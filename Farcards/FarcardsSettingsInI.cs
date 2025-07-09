using FarcardContract.Extensions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Farcards
{
    internal class Farcards:InISettings<Farcards>
    {
        [InIProp("FarServer", "DLL")]
        public string ExtDll { get; set; }
        [InIProp("FarServer", "Type")]
        public int Type { get; set; }
        [InIProp("FarServer", "1")]
        public int? Data { get; set; }
    }
}
