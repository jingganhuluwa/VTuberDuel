// 文件：ConfigLoader.cs
// 作者：急冻雪柜
// 描述：配置管理器
// 日期：2024/09/20 17:27

using System.Collections.Generic;
using System.IO;
using Newtonsoft.Json;

namespace TinyFramework;

public static class ConfigLoader
{
    private static readonly Dictionary<string, object> _configDict = new Dictionary<string, object>();

    public static T GetOne<T>(int id) where T : BaseConfig
    {
        List<T> all = GetAll<T>();
        T config = all.Find(x => x.Id == id);
        return config;
    }


    public static List<T> GetAll<T>() where T : BaseConfig
    {
        if (!_configDict.TryGetValue(typeof(T).Name, out object configs))
        {
            string fullName = PathDefine.ConfigPath + typeof(T).Name + ".json";
            using (StreamReader sr = new StreamReader(fullName))
            {
                configs = JsonConvert.DeserializeObject<List<T>>(sr.ReadToEnd());
                _configDict.Add(typeof(T).Name, configs);
            }
        }

        return configs as List<T>;
    }
}