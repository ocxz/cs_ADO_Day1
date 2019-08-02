using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_登录_数据库完成校验_
{
    interface IVerifier
    {
        string GetKind();
        bool Verify(out string verifyMsg, params string[] param);
    }
}
