using System;

namespace _06_字符串填坑
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");

            string str = HanderStr("I {0} {1} {2} you", "love", "fa","fa");
            Console.WriteLine(str);
            Console.ReadKey();
        }


        /// <summary>
        /// 填充字符串
        /// </summary>
        /// <param name="str">要填充的字符串</param>
        /// <param name="param">填充的值</param>
        /// <returns>填充后的字符串</returns>
        public static string HanderStr(string str, params object[] param)
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
    }
}
