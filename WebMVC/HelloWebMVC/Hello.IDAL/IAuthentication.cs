using Component;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hello.IDAL
{
    public interface IAuthentication
    {
        OperationResult Sign(string username, string password);

        int UpdatePwd(string username, string password);
    }
}
