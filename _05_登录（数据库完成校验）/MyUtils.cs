using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace _05_登录_数据库完成校验_
{
    public static class MyUtils
    {

        #region 1、string处理，填充字符串
        /// <summary>
        /// 填充字符串
        /// </summary>
        /// <param name="str">要填充的字符串</param>
        /// <param name="param">填充的值</param>
        /// <returns>填充后的字符串</returns>
        public static string FillStr(string str, params object[] param)
        {
            string handerstr = str;
            for (int i = 0; i < param.Length; i++)
            {
                try
                {
                    handerstr = handerstr.Replace("{" + i + "}", param[i].ToString());
                }
                catch
                {

                    return handerstr;
                }
            }
            return handerstr;
        } 
        #endregion


    }
}
