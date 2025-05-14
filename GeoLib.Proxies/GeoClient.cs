using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;
using GeoLib.Contracts;
// By Industry convention
// Call the proxy the same as the service contract that it correspondes to, but without the word "service", instead the word "client" and withoud the "I" for "Interface"
// IGeoService becomes GeoClient

namespace GeoLib.Proxies
{
    public class GeoClient : ClientBase<IGeoService>, IGeoService
    {
        public ZipCodeData GetZipInfo(string zip)
        {
            //return Channel.GetZipInfo(zip);
            var x = Channel.GetZipInfo(zip);
            return x;
        }

        public IEnumerable<string> GetStates(bool primaryOnly)
        {
            return Channel.GetStates(primaryOnly);
        }

        public IEnumerable<ZipCodeData> GetZips(string state)
        {
            return Channel.GetZips(state);
        }

        public IEnumerable<ZipCodeData> GetZips(string zip, int range)
        {
            return Channel.GetZips(zip, range);
        }
    }
}
