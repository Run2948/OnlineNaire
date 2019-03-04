/* ==============================================================================
* 命名空间：Quick.Common.Security
* 类 名 称：Encrypt
* 创 建 者：Qing
* 创建时间：2019-01-06 13:40:58
* CLR 版本：4.0.30319.42000
* 保存的文件名：Encrypt
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2019. All rights reserved
* ==============================================================================*/



using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Quick.Common.Security
{
    /// <summary>
    /// 常用的加密解密算法
    /// </summary>
    public static class Encrypt
    {
        #region MD5加密
        /// <summary>
        /// 用hash方法产生md5值
        /// </summary>
        /// <param name="strText"></param>
        /// <returns></returns>
        public static string ToMD5(this string strText)
        {
            MD5 md5 = new MD5CryptoServiceProvider();
            byte[] buffer = md5.ComputeHash(Encoding.UTF8.GetBytes(strText));
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < buffer.Length; i++)
            {
                result.Append(buffer[i].ToString("X2"));
            }
            return result.ToString();
        }
        #endregion

        #region 产生随机salt 最长8位

        private static string allChar = "1,2,3,4,5,6,7,8,9,A,B,C,D,E,F,G,H,i,J,K,L,M,N,O,P,Q,R,S,T,U,V,W,X,Y,Z，~，!,@,#,$,%,^,&,*,(,),_,+,=,-,<,>,?";

        public static string getRandomSalt(int stringLength)
        {
            var allCharArray = allChar.Split(',');
            string RandomCode = "";
            int temp = -1;
            Random rand = new Random();
            if (stringLength > 8)
                stringLength = 8;
            for (int i = 0; i < stringLength; i++)
            {
                if (temp != -1)
                {
                    rand = new Random(temp * i * ((int)DateTime.Now.Ticks));
                }
                int t = rand.Next(allCharArray.Length - 1);
                while (temp == t)
                {
                    t = rand.Next(allCharArray.Length - 1);
                }
                temp = t;
                RandomCode += allCharArray[t];
            }
            return RandomCode;
        }

        /// <summary>
        /// 先加盐后加密
        /// </summary>
        /// <param name="password">密码</param>
        public static string EncryptionWithSalt(string user_name, string password, string salt_a, string salt_b)
        {
            string tmpMd5 = salt_a + user_name + salt_b + password.ToMD5();
            return tmpMd5.ToMD5();
        }

        /// <summary>
        /// 加密后的加盐
        /// </summary>
        /// <param name="md5_password">MD5密码</param>
        public static string AddSaltForEncryption(string user_name, string md5_password, string salt_a, string salt_b)
        {
            string tmpMd5 = salt_a + user_name + salt_b + md5_password.ToUpper();
            return tmpMd5.ToMD5();
        }

        #endregion
    }
}
