
/* ==============================================================================
* 命名空间：Quick.Common 
* 类 名 称：Config
* 创 建 者：Qing
* 创建时间：2019-03-03 17:35:48
* CLR 版本：4.0.30319.42000
* 保存的文件名：Config
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
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Newtonsoft.Json.Linq;

namespace Quick.Common
{
    /// <summary>
    /// Config.json 的摘要说明
    /// </summary>
    public static class Config
    {
        private static bool noCache = true;

        private static JObject BuildItems()
        {
            var json = File.ReadAllText(HttpContext.Current.Server.MapPath("/App_Data/config.json"));
            return JObject.Parse(json);
        }

        public static JObject Items
        {
            get
            {
                if (noCache || _items == null)
                {
                    _items = BuildItems();
                }
                return _items;
            }
        }

        private static JObject _items;

        public static T GetValue<T>(string key)
        {
            return Items[key].Value<T>();
        }

        public static string[] GetStringArray(string key)
        {
            return Items[key].Select(x => x.Value<string>()).ToArray();
        }

        public static List<string> GetStringList(string key)
        {
            return GetStringArray(key).ToList();
        }

        public static Dictionary<string, string> GetDict(string key)
        {
            var list = GetStringList(key);
            Dictionary<string, string> dict = new Dictionary<string, string>();
            foreach (var kv in list)
            {
                var array = kv.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
                if (!string.IsNullOrEmpty(array[1]))
                    dict.Add(array[1], array[0]);
            }
            return dict;
        }

        public static string GetString(string key)
        {
            return GetValue<string>(key);
        }

        public static int GetInt(string key)
        {
            return GetValue<int>(key);
        }
    }
}
