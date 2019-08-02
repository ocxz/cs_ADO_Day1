using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_登录_数据库完成校验_
{
    class LoginVerify : IVerifier
    {
        #region 1、单例设计模式获得验证器对象
        private static LoginVerify verify;
        private LoginVerify() { }
        public static LoginVerify GetVerify()
        {
            if (verify == null)
            {
                verify = new LoginVerify();
            }
            return verify;
        }
        #endregion

        #region 2、获得验证器类型字符串
        /// <summary>
        /// 获得该验证器的类型
        /// </summary>
        /// <returns>验证器类型字符串</returns>
        public string GetKind()
        {
            return VerifierKinds.LoginVerify.ToString();
        }
        #endregion

        #region 3、实现登录的验证

        /// <summary>
        /// 实现登录的验证 传1各字符串参数时，验证用户名，传两个时，验证登录
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public bool Verify(out string verifyMsg, params string[] param)
        {
            verifyMsg = "";

            // 传入的参数为空或者大于2，直接验证失败，返回错误信息，返回false
            if (param == null || param.Length > 2)
            {
                verifyMsg = "登录验证参数不正确，验证失败";
                return false;
            }
            else if (param.Length == 1)
            {
                if (string.IsNullOrEmpty(param[0]))
                {
                    verifyMsg = "用户名不能为空";
                    return false;
                }
                else
                {
                    string sql = "select * from UserInfo where UserName='{0}' ";
                    sql = MyUtils.FillStr(sql, param[0]);
                    object result = SqlUtils.ExeQueryCmd(sql);   // 执行 返回查询结果第一行第一列
                    if (result == null)
                    {
                        verifyMsg = "用户名不存在";
                        return false;
                    }
                    else
                    {
                        verifyMsg = "用户名合法";
                        return true;
                    }
                }

            }
            else
            {
                if (string.IsNullOrEmpty(param[0]))
                {
                    verifyMsg = "用户名不能为空";
                    return false;
                }
                if (string.IsNullOrEmpty(param[1]))
                {
                    verifyMsg = "密码不能为空";
                    return false;
                }
                else
                {
                    string sql = "select * from UserInfo where UserName='{0}' ";
                    sql = MyUtils.FillStr(sql, param[0]);
                    sql = MyUtils.FillStr(sql + "and UserPwd='{0}' ", param[1]);
                    Object result = SqlUtils.ExeQueryCmd(sql);
                    if (result == null)
                    {
                        verifyMsg = "用户名或密码不正确";
                        return false;
                    }
                    else
                    {
                        verifyMsg = "验证成功";
                        return true;
                    }
                }
            }
        }

        #endregion
    }
}
