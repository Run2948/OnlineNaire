/* ==============================================================================
* 命名空间：Quick.Common
* 类 名 称：JsonHelper
* 创 建 者：Qing
* 创建时间：2018-11-30 13:06:59
* CLR 版本：4.0.30319.42000
* 保存的文件名：JsonHelper
* 文件版本：V1.0.0.0
*
* 功能描述：N/A 
*
* 修改历史：
*
*
* ==============================================================================
*         CopyRight @ 班纳工作室 2018. All rights reserved
* ==============================================================================*/


namespace Quick.Common
{
    using Newtonsoft.Json;
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public class JsonHelper
    {
        public static string ConvertJson<T>(T value)
        {
            try
            {
                return JsonConvert.SerializeObject(value);
            }
            catch
            {
                return null;
            }
        }

        public static T ConvertObj<T>(string value)
        {
            try
            {
                return JsonConvert.DeserializeObject<T>(value);
            }
            catch
            {
                return default(T);
            }
        }

        public static T FileCovert<T>(string fileName)
        {
            try
            {
                if (File.Exists(fileName))
                {
                    var text = File.ReadAllText(fileName);
                    return ConvertObj<T>(text);
                }
                return default(T);
            }
            catch (Exception)
            {
                return default(T);
            }
        }

        public static string SaveFile<T>(T list, string fileName)
        {
            try
            {
                if (list != null)
                {
                    string text = ConvertJson(list);
                    File.WriteAllText(fileName, text, Encoding.UTF8);
                }
                return fileName;
            }
            catch (Exception)
            {
                return null;
            }
        }
    }
}

namespace Quick.Common
{
    using System;
    using System.Collections.Generic;
    using System.Data;
    using System.IO;
    using System.Linq;
    using System.Runtime.Serialization.Json;
    using System.Text;
    using System.Threading.Tasks;
    using System.Web.Script.Serialization;

    public class JsonUtils
    {
        /// <summary> 
        /// Json序列化 
        /// </summary> 
        public static string JsonSerializer<T>(T obj)
        {

            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream();
                ser.WriteObject(ms, obj);
                string jsonString = Encoding.UTF8.GetString(ms.ToArray());
                ms.Close();
                return jsonString;
            }
            catch
            {
                return string.Empty;
            }

        }

        /// <summary> 
        /// Json反序列化
        /// </summary> 
        public static T JsonDeserialize<T>(string jsonString)
        {
            try
            {
                DataContractJsonSerializer ser = new DataContractJsonSerializer(typeof(T));
                MemoryStream ms = new MemoryStream(Encoding.UTF8.GetBytes(jsonString));
                T obj = (T)ser.ReadObject(ms);
                return obj;
            }
            catch
            {
                return default(T);
            }
        }

        /// <summary>
        /// 将 DataTable 序列化成 json 字符串
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string DataTableToJson(DataTable dt)
        {
            if (dt == null || dt.Rows.Count == 0)
            {
                return "\"\"";
            }
            JavaScriptSerializer myJson = new JavaScriptSerializer();

            List<Dictionary<string, object>> list = new List<Dictionary<string, object>>();

            foreach (DataRow dr in dt.Rows)
            {
                Dictionary<string, object> result = new Dictionary<string, object>();
                foreach (DataColumn dc in dt.Columns)
                {
                    result.Add(dc.ColumnName, dr[dc].ToString());
                }
                list.Add(result);
            }
            return myJson.Serialize(list);
        }

        /// <summary>
        /// 将对象序列化成 json 字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ObjectToJson(object obj)
        {
            if (obj == null)
            {
                return string.Empty;
            }
            JavaScriptSerializer myJson = new JavaScriptSerializer();

            return myJson.Serialize(obj);
        }

        /// <summary>
        /// 将 json 字符串反序列化成对象
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static object JsonToObject(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return null;
            }
            JavaScriptSerializer myJson = new JavaScriptSerializer();

            return myJson.DeserializeObject(json);
        }

        /// <summary>
        /// 将 json 字符串反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T JsonToObject<T>(string json)
        {
            if (string.IsNullOrEmpty(json))
            {
                return default(T);
            }
            JavaScriptSerializer myJson = new JavaScriptSerializer();

            return myJson.Deserialize<T>(json);
        }
    }
}