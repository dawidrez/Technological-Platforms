using System;
using System.Collections.Generic;
using System.Net;
using System.Linq;
using System.Threading.Tasks;

namespace WpfApp1
{
    class DomainsChanger
    {
        private static readonly string[] hostNames = { "www.microsoft.com", "www.apple.com",
        "www.google.com", "www.ibm.com", "cisco.netacad.net",
        "www.oracle.com", "www.nokia.com", "www.hp.com", "www.dell.com",
        "www.samsung.com", "www.toshiba.com", "www.siemens.com",
        "www.amazon.com", "www.sony.com", "www.canon.com", "www.alcatel-lucent.com", "www.acer.com", "www.motorola.com" };


        public static List<Tuple<string, string>> ChangeDomains()
        {
            List<Tuple<string, string>> result = new List<Tuple<string, string>>();
            hostNames.AsParallel()
                .ForAll(hostName =>
                {
                    lock (result)
                    {
                        result.Add(Tuple.Create(hostName, Dns.GetHostAddresses(hostName).First().ToString()));
                    }
                
                });

            return result;
        }
    }
}
