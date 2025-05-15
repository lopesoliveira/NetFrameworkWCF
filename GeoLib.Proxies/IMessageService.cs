using System;
using System.Collections.Generic;
using System.Linq;
using System.ServiceModel;
using System.Text;
using System.Threading.Tasks;

namespace GeoLib.Client.Contracts
{
    [ServiceContract(Namespace = "http://github.com/lopesoliveira/NetFrameworkWCF")]
    public interface IMessageService
    {
        [OperationContract]
        void ShowMessage(string message);
    }
}
