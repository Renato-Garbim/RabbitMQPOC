using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Publisher_Async_RPC_POC.Events
{

    public delegate void ExecuteSendAsync<in T>(T data);
    
}
