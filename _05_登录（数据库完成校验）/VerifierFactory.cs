using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_登录_数据库完成校验_
{
    class VerifierFactory
    {
        /// <summary>
        /// 根据验证器类型，获得验证器
        /// </summary>
        /// <param name="kinds"></param>
        /// <returns></returns>
        public static IVerifier GetVerifier(VerifierKinds kinds)
        {
            switch (kinds)
            {
                case VerifierKinds.LoginVerify:
                    return LoginVerify.GetVerify();
                default:
                    return null;
            }
        }
    }
}
